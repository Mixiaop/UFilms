using System;
using System.Collections.Generic;
using U;
using U.FakeMvc.Controllers;
using U.Utilities.Web;
using UFilm.Services.Tags;
using UFilm.Services.Movies;
using UFilm.Web.Models;

namespace UFilm.Web.Controllers
{
    public class CommonController : ControllerBase
    {
        ITagService _tagService = UPrimeEngine.Instance.Resolve<ITagService>();
        IMovieQueryService _movieQueryService = UPrimeEngine.Instance.Resolve<IMovieQueryService>();
        IFilmManService _filmManService = UPrimeEngine.Instance.Resolve<IFilmManService>();

        #region Index
        [HttpGet]
        public IndexModel IndexView(string a, string t)
        {
            IndexModel Model = new IndexModel();
            Model.GetMovieArea = a;
            Model.GetMovieType = t;

            Model.MovieAreas = _tagService.GetAllTags(TagType.MovieArea, 22);
            Model.MovieTypes = _tagService.GetAllTags(TagType.MovieType, 28);

            #region 已选中的 地区/类型
            if (!string.IsNullOrEmpty(Model.GetMovieArea))
            {
                string[] aList = Model.GetMovieArea.Trim().Split('_');
                if (aList.Length > 0)
                {
                    foreach (var info in aList)
                    {
                        string tags = string.Empty;
                        foreach (var info2 in aList)
                        {
                            if (info2 != info)
                            {
                                tags += info2 + "_";
                            }
                        }
                        tags = tags.TrimEnd('_');
                        if (info.IsNotNullOrEmpty())
                            Model.SelectedTagsHTML += string.Format("<a href=\"/?t={1}&a={0}\">{2} <i>×</i></a>", tags, Model.GetMovieType, info);
                    }
                }
            }

            if (!string.IsNullOrEmpty(Model.GetMovieType))
            {
                string[] tList = Model.GetMovieType.Trim().Split('_');
                if (tList.Length > 0)
                {
                    foreach (var info in tList)
                    {
                        string tags = string.Empty;
                        foreach (var info2 in tList)
                        {
                            if (info2 != info)
                            {
                                tags += info2 + "_";
                            }
                        }
                        tags = tags.TrimEnd('_');
                        if (info.IsNotNullOrEmpty())
                            Model.SelectedTagsHTML += string.Format("<a href=\"/?t={0}&a={1}\">{2} <i>×</i></a>", tags, Model.GetMovieArea, info);
                    }
                }

            }
            #endregion
            return Model;
        }
        #endregion

        #region Search
        [HttpGet]
        public SearchModel SearchView(string wd, int page = 1) {
            SearchModel Model = new SearchModel();
            Model.GetKeywords = wd.Trim();
            Model.GetPage = page;

            if (!string.IsNullOrEmpty(Model.GetKeywords))
            {
                var filmManResult = _filmManService.Search(1, 1, Model.GetKeywords.Trim());
                if (filmManResult != null && filmManResult.Items.Count > 0)
                {
                    Model.SearchFilmMan = filmManResult.Items[0];
                    Model.SearchFilmManMovies = _filmManService.QueryParticipantMovies(Model.SearchFilmMan.Id, 1, 3);
                }

                Model.Movies = _movieQueryService.Search(page, 16, Model.GetKeywords);

                PagingInfo pageInfo = new PagingInfo();
                pageInfo.PageIndex = Model.GetPage;
                pageInfo.PageSize = 16;

                pageInfo.Url = WebHelper.GetUrl();

                pageInfo.TotalCount = Model.Movies.TotalCount;
                Model.PagiHTML = new Paginations(pageInfo).GetPaging();


            }
            return Model;
        }
        #endregion

        [HttpGet]
        public DiscoverModel DiscoverView(string a, string t)
        {
            DiscoverModel Model = new DiscoverModel();
            Model.PageSize = 30;
            Model.GetMovieArea = a;
            Model.GetMovieType = t;

            List<string> types = new List<string>();
            List<string> areas = new List<string>();

            foreach (var infot in Model.GetMovieTypes)
            {
                if (infot.IsNotNullOrEmpty())
                    types.Add(infot.Trim());
            }

            foreach (var infoa in Model.GetMovieAreas)
            {
                if (infoa.IsNotNullOrEmpty())
                    areas.Add(infoa.Trim());
            }

            Model.Movies = _movieQueryService.Search(Model.PageIndex, Model.PageSize, "", types, areas, Services.Movies.Dto.MovieQueryOrder.Other);
            Model.Paging(Model.Movies.TotalCount);

            Model.MovieAreas = _tagService.GetAllTags(TagType.MovieArea, 22);
            Model.MovieTypes = _tagService.GetAllTags(TagType.MovieType, 28);
            return Model;
        }
    }
}