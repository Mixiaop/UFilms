using UFilm.Domain.Movies;
using UFilm.Domain.Collection;
using UFilm.Domain.Adults;

namespace UFilm.Services.Media
{
    public interface IThumbService : U.Application.Services.IApplicationService
    {
        
        /// <summary>
        /// 格式化“电影封面”的缩略图
        /// </summary>
        /// <param name="movie"></param>
        void Format(Movie movie);

        /// <summary>
        /// 格式化“电影图片”的缩略图
        /// </summary>
        /// <param name="photo"></param>
        void Format(MoviePhoto photo);

        /// <summary>
        /// 格式化“影人头像”的缩略图
        /// </summary>
        /// <param name="filmMan"></param>
        void Format(FilmMan filmMan);

        /// <summary>
        /// 格式化“影人图片”的缩略图
        /// </summary>
        /// <param name="photo"></param>
        void Format(FilmManPhoto photo);

        /// <summary>
        /// 格式化影集封面
        /// </summary>
        /// <param name="collection"></param>
        void Format(MovieCollection collection);

        void Format(LMovie movie);

        void Format(Actor actor);

        void Format(ActorPhoto photo);

        /// <summary>
        /// 清除缩略图
        /// </summary>
        /// <param name="objectId"></param>
        void Clear(int objectId);
    }
}
