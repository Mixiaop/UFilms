using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U.Application.Services.Dto;
using UFilm.Domain.Collection;

namespace UFilm.Services.Collection
{
    public interface IMovieCollectionService : U.Application.Services.IApplicationService
    {
        PagedResultDto<MovieCollection> Search(string keywords = "", int pageIndex = 1, int pageSize = 16);

        MovieCollection Get(int collectionId);

        void Delete(int collectionId);

        void Delete(MovieCollection collection);

        int Insert(MovieCollection collection);

        void Update(MovieCollection collection, string oldTags = "", bool isReplaceCover = false);

        #region Items
        void AddItem(int collectionId, int movieId, string title);

        void DeleteItem(MovieCollectionItem item);

        void UpdateItem(MovieCollectionItem item);

        MovieCollectionItem GetItem(int itemId);

        IList<MovieCollectionItem> GetAllItems(int collectionId);

        PagedResultDto<MovieCollectionItem> SearchItems(int collectionId, int pageIndex = 1, int pageSize = 16);
        #endregion
    }
}
