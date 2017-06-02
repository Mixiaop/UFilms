using System.Collections.Generic;
using U.Application.Services.Dto;
using UFilm.Domain.Movies;

namespace UFilm.Web.Models.Movies
{
    /// <summary>
    /// 电影单页模型
    /// </summary>
    public class SubjectModel : ModelBase
    {
        /// <summary>
        /// 当前电影
        /// </summary>
        public Movie Movie { get; set; }

        /// <summary>
        /// 电影剧照
        /// </summary>
        public PagedResultDto<MoviePhoto> Photos { get; set; }

        /// <summary>
        /// 电影人员列表
        /// </summary>
        public IList<MovieParticipant> Participarts { get; set; }

        /// <summary>
        /// 分享的资源
        /// </summary>
        public IList<MovieTorrent> Torrents { get; set; }

        /// <summary>
        /// 关联的系列电影
        /// </summary>
        public IList<Movie> SeriesMovies { get; set; }
    }
}