using U.Application.Services;
using U.Application.Services.Dto;
using UFilm.Domain.Movies;

namespace UFilm.Services.Movies
{
    public interface IFilmManPhotoService : IApplicationService
    {
        /// <summary>
        /// 分页获取影人图片列表
        /// </summary>
        /// <param name="filmManId">影人Id</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页数</param>
        /// <returns></returns>
        PagedResultDto<FilmManPhoto> Search(int filmManId, int pageIndex = 1, int pageSize = 16);

        /// <summary>
        /// 插入一张影人图片
        /// </summary>
        /// <param name="photo"></param>
        void Insert(FilmManPhoto photo);
    }
}
