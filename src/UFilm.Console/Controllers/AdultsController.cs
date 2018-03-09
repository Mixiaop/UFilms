using System;
using System.Collections.Generic;
using U;
using U.FakeMvc.Controllers;
using UFilm.Services.Adults;
using UFilm.Services.Tags;
using UFilm.Console.Models.Adults;

namespace UFilm.Console.Controllers
{
    public class AdultsController : ControllerBase
    {
        public IMovieService MovieService = UPrimeEngine.Instance.Resolve<IMovieService>();
        public IActorService ActorService = UPrimeEngine.Instance.Resolve<IActorService>();
        public IActorPhotoService ActorPhotoService = UPrimeEngine.Instance.Resolve<IActorPhotoService>();
        public ITagService TagService = UPrimeEngine.Instance.Resolve<ITagService>();

        #region Movies
        [HttpGet]
        public MoviesListModel MoviesListView(string keywords = "", string movieType = "")
        {
            MoviesListModel Model = new MoviesListModel();
            Model.GetKeywords = keywords;
            Model.GetMovieType = movieType;
            Model.PageSize = 20;
            var types = new List<string>();
            if (movieType.IsNotNullOrEmpty())
                types.Add(movieType);

            Model.Results = MovieService.Search(keywords, types, Model.PageIndex, Model.PageSize);
            Model.Paging(Model.Results.TotalCount);

            Model.MovieTypes = TagService.GetAllTags(TagType.LMovieType);

            return Model;
        }

        [HttpGet]
        public MoviesAddModel MoviesAddView()
        {
            MoviesAddModel Model = new MoviesAddModel();

            return Model;
        }

        [HttpGet]
        public MoviesEditModel MoviesEditView(int movieId)
        {
            MoviesEditModel Model = new MoviesEditModel();
            Model.Movie = MovieService.Get(movieId);
            return Model;
        }
        #endregion

        #region Actors
        [HttpGet]
        public ActorsListModel ActorsListView(string keywords = "")
        {
            ActorsListModel Model = new ActorsListModel();
            Model.GetKeywords = keywords;
            Model.PageSize = 20;
            Model.Results = ActorService.Search(keywords, Model.PageIndex, Model.PageSize);
            Model.Paging(Model.Results.TotalCount);

            return Model;
        }

        [HttpGet]
        public ActorsAddModel ActorsAddView()
        {
            ActorsAddModel Model = new ActorsAddModel();

            return Model;
        }

        [HttpGet]
        public ActorsEditModel ActorsEditView(int actorId)
        {
            ActorsEditModel Model = new ActorsEditModel();
            Model.Actor = ActorService.Get(actorId);
            return Model;
        }

        #endregion

        #region ActorPhotos
        [HttpGet]
        public ActorPhotosListModel ActorPhotosListView(int actorId, int isX = 0)
        {
            ActorPhotosListModel Model = new ActorPhotosListModel();
            Model.GetActorId = actorId;
            Model.GetIsX = isX;
            Model.PageSize = 16;
            bool? x = null;
            if (Model.GetIsX == 1)
                x = false;
            else if (Model.GetIsX == 2)
                x = true;
            Model.Actor = ActorService.Get(Model.GetActorId);
            Model.Photos = ActorPhotoService.QueryPhotos(Model.GetActorId, x, Model.PageIndex, Model.PageSize);
            Model.Paging(Model.Photos.TotalCount);

            return Model;
        }
        #endregion
    }
}