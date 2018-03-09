using System;
using AjaxPro;
using U;
using U.Web.Models;
using U.AutoMapper;
using UFilm.Services.Collection;
using UFilm.Services.Collection.Dto;

namespace UFilm.Console.AjaxServices
{
    [AjaxNamespace("CollectionService")]
    public partial class CollectionService : System.Web.UI.Page
    {
        IMovieCollectionService _collectoinService = UPrimeEngine.Instance.Resolve<IMovieCollectionService>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [AjaxMethod]
        public AjaxResponse Delete(int collectionId)
        {
            AjaxResponse res = new AjaxResponse();
            _collectoinService.Delete(collectionId);

            return res;
        }

        [AjaxMethod]
        public AjaxResponse DeleteItem(int collectionItemId)
        {
            AjaxResponse res = new AjaxResponse();
            var collection = _collectoinService.GetItem(collectionItemId);
            _collectoinService.DeleteItem(collection);

            return res;
        }

        [AjaxMethod]
        public AjaxResponse AddItem(int collectionId, int movieId, string title)
        {
            AjaxResponse res = new AjaxResponse();
            _collectoinService.AddItem(collectionId, movieId, title);

            return res;
        }

        [AjaxMethod]
        public AjaxResponse<MovieCollectionItemDto> GetItem(int itemId) {
            AjaxResponse<MovieCollectionItemDto> res = new AjaxResponse<MovieCollectionItemDto>();
            var item = _collectoinService.GetItem(itemId);
            res.Result = item.MapTo<MovieCollectionItemDto>();
            return res;
        }

        [AjaxMethod]
        public AjaxResponse UpdateItem(int itemId, string title, int order)
        {
            AjaxResponse res = new AjaxResponse();
            var item = _collectoinService.GetItem(itemId);
            item.Title = title;
            item.Order = order;
            _collectoinService.UpdateItem(item);
            return res;
        }
    }
}