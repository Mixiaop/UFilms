using System.Linq;
using U.Application.Services;
using UFilm.Configuration;
using UFilm.Domain.Movies;
using UFilm.Services.Media;

namespace UFilm.Services.Movies
{
    /// <summary>
    /// 当前模块的实体“格式化服务”（UI显示)
    /// </summary>
    public class DisplayFormatService : IApplicationService
    {
        protected readonly UFilmSettings _settings;
        protected readonly IThumbService _thumbService;
        public DisplayFormatService(UFilmSettings settings, IThumbService thumbService)
        {
            _settings = settings;
            _thumbService = thumbService;
        }

        /// <summary>
        /// 格式化电影属性友好显示（配置）
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public Movie Format(Movie movie)
        {
            if (movie != null)
            {
                if (movie.Area.IsNotNullOrEmpty())
                    movie.Area = movie.Area.Replace(",", " / ");

                if (movie.OtherEnName.IsNotNullOrEmpty())
                    movie.OtherEnName = movie.OtherEnName.Replace(",", " / ");

                if (movie.MovieType.IsNotNullOrEmpty())
                    movie.MovieType = movie.MovieType.Replace(",", " / ");

                if (movie.Actor.IsNotNullOrEmpty())
                {
                    movie.Actor = movie.Actor.Replace(",", " / ");
                    movie.Actor = movie.Actor.SubString2(50).Trim().TrimEnd('/');
                }

                if (movie.Director.IsNotNullOrEmpty())
                    movie.Director = movie.Director.Replace(",", " / ");

                if (movie.OtherPostYear.IsNotNullOrEmpty())
                    movie.OtherPostYear = movie.OtherPostYear.Replace(",", " / ");

                _thumbService.Format(movie);

                //老的使用MovieCover表
                ////封面
                //if (movie != null && movie.Covers != null) { 
                //    //原图
                //    var source = movie.Covers.Where(x => x.Size == 0).FirstOrDefault();
                //    if (source != null)
                //        movie.FormatCoverSource = source.Url;

                //}
            }
            return movie;
        }

        protected FilmMan Format(FilmMan filmMan)
        {
            if (filmMan != null)
            {
                if (filmMan.PlaceOfBirth.IsNotNullOrEmpty())
                    filmMan.PlaceOfBirth = filmMan.PlaceOfBirth.Replace(",", " / ");

                if (filmMan.Job.IsNotNullOrEmpty())
                    filmMan.Job = filmMan.Job.Replace(",", " / ");

                if (filmMan.MoreCnName.IsNotNullOrEmpty())
                    filmMan.MoreCnName = filmMan.MoreCnName.Replace(",", " / ");

                if (filmMan.MoreEnName.IsNotNullOrEmpty())
                    filmMan.MoreEnName = filmMan.MoreEnName.Replace(",", " / ");

                if (filmMan.FamilyInfo.IsNotNullOrEmpty())
                    filmMan.FamilyInfo = filmMan.FamilyInfo.Replace(",", " / ");

                filmMan.FormatGender = filmMan.Gender == 1 ? "男" : (filmMan.Gender == 0 ? "女" : "");

                _thumbService.Format(filmMan);
            }
            return filmMan;
        }
    }
}
