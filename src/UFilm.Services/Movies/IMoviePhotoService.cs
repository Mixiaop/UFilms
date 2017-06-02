using U.Application.Services;
using U.Application.Services.Dto;
using UFilm.Domain.Movies;

namespace UFilm.Services.Movies
{
    /// <summary>
    /// 电影“影片的图片”服务，包含电影的海报及剧照
    /// </summary>
    public interface IMoviePhotoService : IApplicationService
    {
        /// <summary>
        /// 分页搜索影片的图片信息
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页数</param>
        /// <param name="movieId">电影Id</param>
        /// <param name="keywords">搜索关键字</param>
        /// <param name="photoType"></param>
        /// <returns></returns>
        PagedResultDto<MoviePhoto> Search(int pageIndex = 1, int pageSize = 16, int movieId = 0, string keywords = "", MoviePhotoType photoType = MoviePhotoType.None);

        MoviePhoto Get(int photoId);

        void Insert(MoviePhoto photo);

        void Update(MoviePhoto photo);

        void Delete(MoviePhoto photo);
    }
}
