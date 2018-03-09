using U.Settings;

namespace UFilm.Configuration
{
    [USettingsPathArribute("UFilmSettings.json", "/Config/UFilm/")]
    public class UFilmSettings : USettings<UFilmSettings>
    {

        /// <summary>
        /// 当前站点域名
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 电影默认封图大小（如：260）
        /// </summary>
        public int MovieCoverSize { get; set; }
        
        /// <summary>
        /// 电影图片默认缩略图大小
        /// </summary>
        public int MoviePhotoThumbSize { get; set; }

        /// <summary>
        /// 影人头像剪切大小
        /// </summary>
        public int FilmManAvatarSize { get; set; }

        /// <summary>
        /// 电影人缩略图默认剪切大小
        /// </summary>
        public int FilmManPhotoThumbSize { get; set; }
    }
}
