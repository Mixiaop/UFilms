using System.Collections.Generic;
using UFilm.Domain.Adults;

namespace UFilm.AlibWeb.Models.Movies
{
    public class SubjectModel : ModelBase
    {
        /// <summary>
        /// 当前电影
        /// </summary>
        public LMovie Movie { get; set; }

        /// <summary>
        /// 电影人员列表
        /// </summary>
        public IList<LMovieActor> Actors { get; set; }

        ///// <summary>
        ///// 分享的资源
        ///// </summary>
        //public IList<MovieTorrent> Torrents { get; set; }

        ///// <summary>
        ///// 关联的系列电影
        ///// </summary>
        //public IList<Movie> SeriesMovies { get; set; }
    }
}