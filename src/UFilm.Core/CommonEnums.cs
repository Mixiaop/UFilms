using U.CodeAnnotations;

namespace UFilm
{
    /// <summary>
    /// 电影职业类型
    /// </summary>
    public enum MovieJobType : int
    {
        /// <summary>
        /// 导演 
        /// </summary>
        [EnumAlias("导演")]
        Director = 0,
        /// <summary>
        /// 编剧
        /// </summary>
        [EnumAlias("编剧")]
        ScreenWriter = 1,
        /// <summary>
        /// 演员
        /// </summary>
        [EnumAlias("演员")]
        Actor = 2
    }

    /// <summary>
    /// 影片图片类型
    /// </summary>
    public enum MoviePhotoType : int
    {
        /// <summary>
        /// 剧照
        /// </summary>
         [EnumAlias("剧照")]
        Stage = 1,
        /// <summary>
        /// 海报
        /// </summary>
         [EnumAlias("海报")]
        Poster = 2,
        None = -1
    }

    /// <summary>
    /// 所属业务的标签分类
    /// </summary>
    public enum TagType
    {
        /// <summary>
        /// None
        /// </summary>
        [EnumAlias("None")]
        None = 0,
        /// <summary>
        /// TF玩具，如：重涂、限定、MP比例
        /// </summary>
        [EnumAlias("TF玩具")]
        TFToys = 1,
        /// <summary>
        /// 电影类型
        /// </summary>
        [EnumAlias("电影类型")]
        MovieType = 2,
        /// <summary>
        /// 电影语言
        /// </summary>
        [EnumAlias("电影语言")]
        MovieLanguage = 3,
        /// <summary>
        /// 电影地区
        /// </summary>
        [EnumAlias("电影地区")]
        MovieArea = 4,
        /// <summary>
        /// 影人星座
        /// </summary>
        [EnumAlias("影人星座")]
        FilmManConstellation = 5,
        /// <summary>
        /// 电影精选集
        /// </summary>
        [EnumAlias("电影精选集")]
        MovieCollection = 10,
        /// <summary>
        /// 电影类型
        /// </summary>
        [EnumAlias("L电影类型")]
        LMovieType = 20
    }

    /// <summary>
    /// 缩略图类型
    /// </summary>
    public enum ThumbType { 
        /// <summary>
        /// 电影封面
        /// </summary>
        MovieCover = 4,
        /// <summary>
        /// 电影图片
        /// </summary>
        MoviePhotos = 1,
        /// <summary>
        /// 影人图片
        /// </summary>
        FilmManPhotos = 2,
        /// <summary>
        /// 影人头像
        /// </summary>
        FilmManAvatar = 3,
        /// <summary>
        /// 影集封面
        /// </summary>
        MovieCollectionCover = 5,
        AdultMovieCover = 30,
        AdultActorAvatar = 31,
        AdultActorPhotos = 32
    }

    /// <summary>
    /// 奖项类型
    /// </summary>
    public enum AwardType
    {
        /// <summary>
        /// 奥斯卡金像奖
        /// </summary>
        Oscar = 0
    }

    /// <summary>
    /// 通用排序
    /// </summary>
    public enum OrderBy  { 
        CreationTimeDesc,
        CreationTimeAsc
    }

    /// <summary>
    /// Adults电影搜索排序
    /// </summary>
    public enum LMovieSearchOrderBy
    {
        Hits,
        CreationTimeDesc,
        CreationTimeAsc
    }
}
