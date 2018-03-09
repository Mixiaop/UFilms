using U;
using U.FakeMvc.Controllers;
using U.Utilities.Web;
using UFilm.Services.Tags;
using UFilm.Services.Movies;
using UFilm.Services.Collection;
using UFilm.Web.Models.Collection;

namespace UFilm.Web.Controllers
{
    public class CollectionController : ControllerBase
    {
        ITagService _tagService = UPrimeEngine.Instance.Resolve<ITagService>();
        IMovieQueryService _movieQueryService = UPrimeEngine.Instance.Resolve<IMovieQueryService>();
        IFilmManService _filmManService = UPrimeEngine.Instance.Resolve<IFilmManService>();
        IMovieCollectionService _collectionService = UPrimeEngine.Instance.Resolve<IMovieCollectionService>();

        [HttpGet]
        public SubjectModel SubjectView(int collectionId)
        {
            SubjectModel Model = new SubjectModel();
            Model.Collection = _collectionService.Get(collectionId);
            if (Model.Collection == null)
                Invoke404();

            Model.Items = _collectionService.GetAllItems(collectionId);

            return Model;
        }

        [HttpGet]
        public SearchModel SearchModel() {
            SearchModel Model = new SearchModel();
            Model.PageSize = 30;

            Model.Results = _collectionService.Search("", Model.PageIndex, Model.PageSize);
            Model.Paging(Model.Results.TotalCount);

            return Model;
        }
    }
}