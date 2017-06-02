using U;
using U.Utilities.Web;
using U.FakeMvc;
using U.FakeMvc.Controllers;
using UFilm.Domain.Movies;
using UFilm.Domain.Collection;
using UFilm.Services.Movies;
using UFilm.Services.Collection;
using UFilm.Services.Tags;
using UFilm.Console.Models.Collection;

namespace UFilm.Console.Controllers
{
    public class CollectionController : ControllerBase
    {
        public IMovieService _movieService = UPrimeEngine.Instance.Resolve<IMovieService>();
        public IMoviePhotoService _photoService = UPrimeEngine.Instance.Resolve<IMoviePhotoService>();
        public IMovieTorrentService _torrentService = UPrimeEngine.Instance.Resolve<IMovieTorrentService>();
        public IMovieCollectionService _collectionService = UPrimeEngine.Instance.Resolve<IMovieCollectionService>();
        public ITagService _tagService = UPrimeEngine.Instance.Resolve<ITagService>();
        [HttpGet]
        public CollectionAddModel CollectionAddView(string keywords)
        {
            CollectionAddModel Model = new CollectionAddModel();
            Model.Tags = _tagService.GetAllTags(TagType.MovieCollection);
            return Model;
        }

        [HttpGet]
        public CollectionEditModel CollectionEditView(int collectionId)
        {
            CollectionEditModel Model = new CollectionEditModel();
            Model.Tags = _tagService.GetAllTags(TagType.MovieCollection);
            Model.Collection = _collectionService.Get(collectionId);

            return Model;
        }

        [HttpGet]
        public CollectionListModel CollectionListView(string keywords)
        {
            CollectionListModel Model = new CollectionListModel();
            Model.GetKeywords = keywords;
            Model.PageSize = 40;
            Model.Results = _collectionService.Search(keywords, Model.PageIndex, Model.PageSize);

            Model.Paging(Model.Results.TotalCount);

            return Model;
        }

        [HttpGet]
        public CollectionItemsListModel CollectionItemListView(int collectionId)
        {
            CollectionItemsListModel Model = new CollectionItemsListModel();
            Model.Collection = _collectionService.Get(collectionId);
            Model.PageSize = 40;
            Model.Results = _collectionService.SearchItems(collectionId, Model.PageIndex, Model.PageSize);
            Model.Paging(Model.Results.TotalCount);

            return Model;
        }

        #region Action
        public void AddCollection(MovieCollection collection)
        {
            _collectionService.Insert(collection);
        }

        public void UpdateCollection(CollectionEditModel model)
        {
            _collectionService.Update(model.Collection, model.OldTags);
        }
        #endregion
    }
}