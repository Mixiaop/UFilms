using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U.Application.Services.Dto;
using UFilm.Domain.Collection;
using UFilm.Services.Movies;
using UFilm.Services.Media;
using UFilm.Services.Tags;

namespace UFilm.Services.Collection
{
    public class MovieCollectionService : IMovieCollectionService
    {
        private readonly IMovieCollectionRepository _collectionRepository;
        private readonly IMovieCollectionItemRepository _itemRepository;
        private readonly DisplayFormatService _formatService;
        private readonly IThumbService _thumbService;
        private readonly ITagService _tagService;
        public MovieCollectionService(IMovieCollectionRepository collectionRepository, IMovieCollectionItemRepository itemRepository, DisplayFormatService formatService, IThumbService thumbService, ITagService tagService)
        {
            _collectionRepository = collectionRepository;
            _itemRepository = itemRepository;
            _formatService = formatService;
            _thumbService = thumbService;
            _tagService = tagService;
        }

        public PagedResultDto<MovieCollection> Search(string keywords = "", int pageIndex = 1, int pageSize = 16)
        {
            var query = _collectionRepository.GetAll();

            if (keywords.IsNotNullOrEmpty())
                query = query.Where(x => x.Name.Contains(keywords));

            int count = query.Count();

            query = query.OrderByDescending(x => x.CreationTime)
                         .Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize);

            var list = query.ToList();

            foreach (var info in list)
                _thumbService.Format(info);


            return new PagedResultDto<MovieCollection>(count, list);
        }

        public MovieCollection Get(int collectionId)
        {
            var collection = _collectionRepository.Get(collectionId);
            _thumbService.Format(collection);

            return collection;
        }

        public void Delete(int collectionId) {
            var collect = Get(collectionId);
            Delete(collect);
        }

        public void Delete(MovieCollection collection) {
            _collectionRepository.Delete(collection);
        }

        public int Insert(MovieCollection collection)
        {
            //标签
            if (collection.Tags.IsNotNullOrEmpty())
            {
                string[] list = collection.Tags.Split(',');

                List<string> tags = new List<string>();
                if (list != null)
                {
                    foreach (var s in list)
                        tags.Add(s);
                }

                _tagService.Update(TagType.MovieCollection, tags);
            }
            return _collectionRepository.InsertAndGetId(collection);
        }

        public void Update(MovieCollection collection, string oldTags = "", bool isReplaceCover = false)
        {
            _collectionRepository.Update(collection);

            List<string> newTagsList = new List<string>();
            List<string> oldTagsList = new List<string>();
            if (collection.Tags.IsNotNullOrEmpty())
            {
                newTagsList = collection.Tags.ToList(',');
            }
            if (oldTags.IsNotNullOrEmpty()) {
                oldTagsList = oldTags.ToList(',');
            }

            _tagService.Update(TagType.MovieCollection, newTagsList, oldTagsList);

            if (isReplaceCover)
                _thumbService.Clear(collection.Id);
        }

        #region Items
        public void AddItem(int collectionId, int movieId, string title)
        {
            MovieCollectionItem item = new MovieCollectionItem();
            item.CollectionId = collectionId;
            var collection = Get(item.CollectionId);
            if (collection != null)
            {
                item.MovieId = movieId;
                item.Title = title;
                //排序
                var orderQuery = _itemRepository.GetAll().Where(x => x.CollectionId == collectionId);
                if (orderQuery.Count() > 0)
                {
                    var order = orderQuery.Max(x => x.Order);
                    item.Order = order + 1;
                }
                else
                {
                    item.Order = 1;
                }

                _itemRepository.InsertAndGetId(item);

                collection.Count += 1;
                Update(collection);
            }
        }

        public void DeleteItem(MovieCollectionItem item)
        {
            var collection = Get(item.CollectionId);
            if (collection != null)
            {
                if (collection.Count > 0)
                {
                    collection.Count -= 1;
                    Update(collection);
                }

                _itemRepository.Delete(item);
            }
        }

        public void UpdateItem(MovieCollectionItem item) {
            _itemRepository.Update(item);
        }

        public MovieCollectionItem GetItem(int itemId)
        {
            return _itemRepository.Get(itemId);
        }

        public IList<MovieCollectionItem> GetAllItems(int collectionId)
        {
            var query = _itemRepository.GetAll()
                        .Where(x => x.CollectionId == collectionId)
                        .OrderByDescending(x => x.Order);

            var list = query.ToList();
            foreach (var c in list)
            {
                _formatService.Format(c.Movie);
            }
            return list;
        }

        public PagedResultDto<MovieCollectionItem> SearchItems(int collectionId, int pageIndex = 1, int pageSize = 16)
        {
            var query = _itemRepository.GetAll()
                        .Where(x => x.CollectionId == collectionId)
                        .OrderByDescending(x => x.Order);

            var count = query.Count();

            var list = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PagedResultDto<MovieCollectionItem>(count, list);
        }
        #endregion
    }
}
