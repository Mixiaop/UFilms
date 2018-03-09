using System.Linq;
using UZeroMedia.Client.Services;
using UFilm.Configuration;
using UFilm.Domain.Media;
using UFilm.Domain.Movies;
using UFilm.Domain.Collection;
using UFilm.Domain.Adults;

namespace UFilm.Services.Media
{
    public class ThumbService : IThumbService
    {
        private UFilmSettings _settings;
        private readonly IThumbRepository _thumbRepository;
        private PictureClientService _pictureClientService = new PictureClientService();

        public ThumbService(UFilmSettings settings, IThumbRepository thumbRepository)
        {
            _settings = settings;
            _thumbRepository = thumbRepository;
        }

        #region Movie
        /// <summary>
        /// 格式化“电影封面”的缩略图
        /// </summary>
        /// <param name="movie"></param>
        public void Format(Movie movie)
        {
            movie.Cover = new Picture();
            if (movie != null && movie.Covers != null && movie.CoverId > 0)
            {

                //缩略图
                var thumb = movie.Covers.Where(x => x.Size == _settings.MovieCoverSize && x.Type == ThumbType.MovieCover).FirstOrDefault();
                if (thumb != null && thumb.Url.IsNotNullOrEmpty())
                {
                    movie.Cover.ThumbUrl = thumb.Url;
                    movie.Cover.ThumbSize = thumb.Size;
                }
                else
                {
                    //上传到图片应用 img.mbjuan.com
                    movie.Cover.ThumbUrl = _pictureClientService.GetPictureUrl(movie.CoverId, _settings.MovieCoverSize);
                    movie.Cover.ThumbSize = _settings.MovieCoverSize;
                    Thumb t = new Thumb();
                    t.Type = ThumbType.MovieCover;
                    t.ObjectId = movie.Id;
                    t.Url = movie.Cover.ThumbUrl;
                    t.Size = _settings.MovieCoverSize;
                    _thumbRepository.Insert(t);
                }

                //源图
                var source = movie.Covers.Where(x => x.Size == 0 && x.Type == ThumbType.MovieCover).FirstOrDefault();
                if (source != null && source.Url.IsNotNullOrEmpty())
                {
                    movie.Cover.SourceUrl = source.Url;
                }
                else
                {
                    //上传到图片应用 img.mbjuan.com
                    movie.Cover.SourceUrl = _pictureClientService.GetPictureUrl(movie.CoverId, 0);
                    Thumb t = new Thumb();
                    t.Type = ThumbType.MovieCover;
                    t.ObjectId = movie.Id;
                    t.Url = movie.Cover.SourceUrl;
                    t.Size = 0;
                    _thumbRepository.Insert(t);
                }                
            }
            InsureDefaultPic(movie.Cover);
        }

        /// <summary>
        /// 格式化“电影图片”的缩略图
        /// </summary>
        /// <param name="photo"></param>
        public void Format(MoviePhoto photo)
        {
            if (photo != null && photo.Thumbs != null && photo.PictureId > 0)
            {
                photo.Picture = new Picture();

                //缩略图
                var thumb = photo.Thumbs.Where(x => x.Size == _settings.MoviePhotoThumbSize && x.Type == ThumbType.MoviePhotos).FirstOrDefault();
                if (thumb != null && thumb.Url.IsNotNullOrEmpty())
                {
                    photo.Picture.ThumbUrl = thumb.Url;
                    photo.Picture.ThumbSize = thumb.Size;
                }
                else
                {
                    //上传到图片应用 img.mbjuan.com
                    photo.Picture.ThumbUrl = _pictureClientService.GetPictureUrl(photo.PictureId, _settings.MoviePhotoThumbSize);
                    photo.Picture.ThumbSize = _settings.MoviePhotoThumbSize;
                    Thumb t = new Thumb();
                    t.Type = ThumbType.MoviePhotos;
                    t.ObjectId = photo.Id;
                    t.Url = photo.Picture.ThumbUrl;
                    t.Size = _settings.MoviePhotoThumbSize;
                    _thumbRepository.Insert(t);
                }

                //源图
                var source = photo.Thumbs.Where(x => x.Size == 0 && x.Type == ThumbType.MoviePhotos).FirstOrDefault();
                if (source != null && source.Url.IsNotNullOrEmpty())
                {
                    photo.Picture.SourceUrl = source.Url;
                }
                else
                {
                    //上传到图片应用 img.mbjuan.com
                    photo.Picture.SourceUrl = _pictureClientService.GetPictureUrl(photo.PictureId, 0);
                    Thumb t = new Thumb();
                    t.Type = ThumbType.MoviePhotos;
                    t.ObjectId = photo.Id;
                    t.Url = photo.Picture.SourceUrl;
                    t.Size = 0;
                    _thumbRepository.Insert(t);
                }
            }
            InsureDefaultPic(photo.Picture);
        }
        #endregion

        #region FilmMan
        /// <summary>
        /// 格式化“影人头像”的缩略图
        /// </summary>
        /// <param name="filmMan"></param>
        public void Format(FilmMan filmMan)
        {
            filmMan.Avatar = new Picture();
            if (filmMan != null && filmMan.Avatars != null && filmMan.AvatarId > 0)
            {
                //缩略图
                var thumb = filmMan.Avatars.Where(x => x.Size == _settings.FilmManAvatarSize && x.Type == ThumbType.FilmManAvatar).FirstOrDefault();
                if (thumb != null && thumb.Url.IsNotNullOrEmpty())
                {
                    filmMan.Avatar.ThumbUrl = thumb.Url;
                    filmMan.Avatar.ThumbSize = thumb.Size;
                }
                else
                {
                    //上传到图片应用 img.mbjuan.com
                    filmMan.Avatar.ThumbUrl = _pictureClientService.GetPictureUrl(filmMan.AvatarId, _settings.FilmManAvatarSize);
                    filmMan.Avatar.ThumbSize = _settings.FilmManAvatarSize;
                    Thumb t = new Thumb();
                    t.Type = ThumbType.FilmManAvatar;
                    t.ObjectId = filmMan.Id;
                    t.Url = filmMan.Avatar.ThumbUrl;
                    t.Size = _settings.FilmManAvatarSize;
                    _thumbRepository.Insert(t);
                }

                //源图
                var source = filmMan.Avatars.Where(x => x.Size == 0 && x.Type == ThumbType.FilmManAvatar).FirstOrDefault();
                if (source != null && source.Url.IsNotNullOrEmpty())
                {
                    filmMan.Avatar.SourceUrl = source.Url;
                }
                else
                {
                    //上传到图片应用 img.mbjuan.com
                    filmMan.Avatar.SourceUrl = _pictureClientService.GetPictureUrl(filmMan.AvatarId, 0);
                    Thumb t = new Thumb();
                    t.Type = ThumbType.FilmManAvatar;
                    t.ObjectId = filmMan.Id;
                    t.Url = filmMan.Avatar.SourceUrl;
                    t.Size = 0;
                    _thumbRepository.Insert(t);
                }
            }
            InsureDefaultPic(filmMan.Avatar);
        }
        /// <summary>
        /// 格式化“影人图片”的缩略图
        /// </summary>
        /// <param name="photo"></param>
        public void Format(FilmManPhoto photo)
        {
            if (photo != null && photo.Thumbs != null && photo.PictureId > 0)
            {
                photo.Picture = new Picture();

                //缩略图
                var thumb = photo.Thumbs.Where(x => x.Size == _settings.FilmManPhotoThumbSize && x.Type == ThumbType.FilmManPhotos).FirstOrDefault();
                if (thumb != null && thumb.Url.IsNotNullOrEmpty())
                {
                    photo.Picture.ThumbUrl = thumb.Url;
                    photo.Picture.ThumbSize = thumb.Size;
                }
                else
                {
                    //上传到图片应用 img.mbjuan.com
                    photo.Picture.ThumbUrl = _pictureClientService.GetPictureUrl(photo.PictureId, _settings.FilmManPhotoThumbSize);
                    photo.Picture.ThumbSize = _settings.FilmManPhotoThumbSize;
                    Thumb t = new Thumb();
                    t.Type = ThumbType.FilmManPhotos;
                    t.ObjectId = photo.Id;
                    t.Url = photo.Picture.ThumbUrl;
                    t.Size = _settings.FilmManPhotoThumbSize;
                    _thumbRepository.Insert(t);
                }

                //源图
                var source = photo.Thumbs.Where(x => x.Size == 0 && x.Type == ThumbType.FilmManPhotos).FirstOrDefault();
                if (source != null && source.Url.IsNotNullOrEmpty())
                {
                    photo.Picture.SourceUrl = source.Url;
                }
                else
                {
                    //上传到图片应用 img.mbjuan.com
                    photo.Picture.SourceUrl = _pictureClientService.GetPictureUrl(photo.PictureId, 0);
                    Thumb t = new Thumb();
                    t.Type = ThumbType.FilmManPhotos;
                    t.ObjectId = photo.Id;
                    t.Url = photo.Picture.SourceUrl;
                    t.Size = 0;
                    _thumbRepository.Insert(t);
                }
            }
            InsureDefaultPic(photo.Picture);
        }
        #endregion

        #region Collection
        /// <summary>
        /// 格式化影集封面
        /// </summary>
        /// <param name="collection"></param>
        public void Format(MovieCollection collection) {
            collection.Cover = new Picture();
            if (collection != null && collection.Covers != null && collection.CoverId > 0)
            {
                //缩略图
                var thumb = collection.Covers.Where(x => x.Size == _settings.MovieCoverSize && x.Type == ThumbType.MovieCollectionCover).FirstOrDefault();
                if (thumb != null && thumb.Url.IsNotNullOrEmpty())
                {
                    collection.Cover.ThumbUrl = thumb.Url;
                    collection.Cover.ThumbSize = thumb.Size;
                }
                else
                {
                    //上传到图片应用 img.mbjuan.com
                    collection.Cover.ThumbUrl = _pictureClientService.GetPictureUrl(collection.CoverId, _settings.MovieCoverSize);
                    collection.Cover.ThumbSize = _settings.MovieCoverSize;
                    Thumb t = new Thumb();
                    t.Type = ThumbType.MovieCollectionCover;
                    t.ObjectId = collection.Id;
                    t.Url = collection.Cover.ThumbUrl;
                    t.Size = _settings.MovieCoverSize;
                    _thumbRepository.Insert(t);
                }

                //源图
                var source = collection.Covers.Where(x => x.Size == 0 && x.Type == ThumbType.MovieCollectionCover).FirstOrDefault();
                if (source != null && source.Url.IsNotNullOrEmpty())
                {
                    collection.Cover.SourceUrl = source.Url;
                }
                else
                {
                    //上传到图片应用 img.mbjuan.com
                    collection.Cover.SourceUrl = _pictureClientService.GetPictureUrl(collection.CoverId, 0);
                    Thumb t = new Thumb();
                    t.Type = ThumbType.MovieCollectionCover;
                    t.ObjectId = collection.Id;
                    t.Url = collection.Cover.SourceUrl;
                    t.Size = 0;
                    _thumbRepository.Insert(t);
                }
            }
            InsureDefaultPic(collection.Cover);
        }
        #endregion

        #region Adults
        public void Format(LMovie movie) {
            movie.Cover = new Picture();
            if (movie != null && movie.Covers != null && movie.CoverId > 0)
            {

                //缩略图
                var thumb = movie.Covers.Where(x => x.Size == _settings.MovieCoverSize && x.Type == ThumbType.AdultMovieCover).FirstOrDefault();
                if (thumb != null && thumb.Url.IsNotNullOrEmpty())
                {
                    movie.Cover.ThumbUrl = thumb.Url;
                    movie.Cover.ThumbSize = thumb.Size;
                }
                else
                {
                    //上传到图片应用 img.mbjuan.com
                    movie.Cover.ThumbUrl = _pictureClientService.GetPictureUrl(movie.CoverId, _settings.MovieCoverSize);
                    movie.Cover.ThumbSize = _settings.MovieCoverSize;
                    Thumb t = new Thumb();
                    t.Type = ThumbType.AdultMovieCover;
                    t.ObjectId = movie.Id;
                    t.Url = movie.Cover.ThumbUrl;
                    t.Size = _settings.MovieCoverSize;
                    _thumbRepository.Insert(t);
                }

                //源图
                var source = movie.Covers.Where(x => x.Size == 0 && x.Type == ThumbType.AdultMovieCover).FirstOrDefault();
                if (source != null && source.Url.IsNotNullOrEmpty())
                {
                    movie.Cover.SourceUrl = source.Url;
                }
                else
                {
                    //上传到图片应用 img.mbjuan.com
                    movie.Cover.SourceUrl = _pictureClientService.GetPictureUrl(movie.CoverId, 0);
                    Thumb t = new Thumb();
                    t.Type = ThumbType.AdultMovieCover;
                    t.ObjectId = movie.Id;
                    t.Url = movie.Cover.SourceUrl;
                    t.Size = 0;
                    _thumbRepository.Insert(t);
                }
            }
            InsureDefaultPic(movie.Cover);
        }

        public void Format(Actor actor) {
            actor.Avatar = new Picture();
            if (actor != null && actor.Avatars != null && actor.AvatarId > 0)
            {
                //缩略图
                var thumb = actor.Avatars.Where(x => x.Size == _settings.FilmManAvatarSize && x.Type == ThumbType.AdultActorAvatar).FirstOrDefault();
                if (thumb != null && thumb.Url.IsNotNullOrEmpty())
                {
                    actor.Avatar.ThumbUrl = thumb.Url;
                    actor.Avatar.ThumbSize = thumb.Size;
                }
                else
                {
                    //上传到图片应用 img.mbjuan.com
                    actor.Avatar.ThumbUrl = _pictureClientService.GetPictureUrl(actor.AvatarId, _settings.FilmManAvatarSize);
                    actor.Avatar.ThumbSize = _settings.FilmManAvatarSize;
                    Thumb t = new Thumb();
                    t.Type = ThumbType.AdultActorAvatar;
                    t.ObjectId = actor.Id;
                    t.Url = actor.Avatar.ThumbUrl;
                    t.Size = _settings.FilmManAvatarSize;
                    _thumbRepository.Insert(t);
                }

                //源图
                var source = actor.Avatars.Where(x => x.Size == 0 && x.Type == ThumbType.AdultActorAvatar).FirstOrDefault();
                if (source != null && source.Url.IsNotNullOrEmpty())
                {
                    actor.Avatar.SourceUrl = source.Url;
                }
                else
                {
                    //上传到图片应用 img.mbjuan.com
                    actor.Avatar.SourceUrl = _pictureClientService.GetPictureUrl(actor.AvatarId, 0);
                    Thumb t = new Thumb();
                    t.Type = ThumbType.AdultActorAvatar;
                    t.ObjectId = actor.Id;
                    t.Url = actor.Avatar.SourceUrl;
                    t.Size = 0;
                    _thumbRepository.Insert(t);
                }
            }
            InsureDefaultPic(actor.Avatar);
        }

        public void Format(ActorPhoto photo) {
            if (photo != null && photo.Thumbs != null && photo.PictureId > 0)
            {
                photo.Picture = new Picture();

                //缩略图
                var thumb = photo.Thumbs.Where(x => x.Size == _settings.FilmManPhotoThumbSize && x.Type == ThumbType.AdultActorPhotos).FirstOrDefault();
                if (thumb != null && thumb.Url.IsNotNullOrEmpty())
                {
                    photo.Picture.ThumbUrl = thumb.Url;
                    photo.Picture.ThumbSize = thumb.Size;
                }
                else
                {
                    //上传到图片应用 img.mbjuan.com
                    photo.Picture.ThumbUrl = _pictureClientService.GetPictureUrl(photo.PictureId, _settings.FilmManPhotoThumbSize);
                    photo.Picture.ThumbSize = _settings.FilmManPhotoThumbSize;
                    Thumb t = new Thumb();
                    t.Type = ThumbType.AdultActorPhotos;
                    t.ObjectId = photo.Id;
                    t.Url = photo.Picture.ThumbUrl;
                    t.Size = _settings.FilmManPhotoThumbSize;
                    _thumbRepository.Insert(t);
                }

                //源图
                var source = photo.Thumbs.Where(x => x.Size == 0 && x.Type == ThumbType.AdultActorPhotos).FirstOrDefault();
                if (source != null && source.Url.IsNotNullOrEmpty())
                {
                    photo.Picture.SourceUrl = source.Url;
                }
                else
                {
                    //上传到图片应用 img.mbjuan.com
                    photo.Picture.SourceUrl = _pictureClientService.GetPictureUrl(photo.PictureId, 0);
                    Thumb t = new Thumb();
                    t.Type = ThumbType.AdultActorPhotos;
                    t.ObjectId = photo.Id;
                    t.Url = photo.Picture.SourceUrl;
                    t.Size = 0;
                    _thumbRepository.Insert(t);
                }
            }
            InsureDefaultPic(photo.Picture);
        }
        #endregion

        #region Utilities
        public void Clear(int objectId) {
            _thumbRepository.Delete(x => x.ObjectId == objectId);
        }
          
        public void InsureDefaultPic(Picture pic) {
            if (pic == null) pic = new Picture();
            if (pic != null)
            {
                if (pic.ThumbUrl.IsNullOrEmpty())
                {
                    pic.SourceUrl = "/img/NoPic210x315.jpg";
                    pic.ThumbUrl = "/img/NoPic210x315.jpg";
                }
            }
        }
        #endregion
    }
}
