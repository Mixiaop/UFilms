
namespace UFilm.EntityFramework
{
    public class DbConsts
    {
        public class DbTableName
        {
            public const string Media_Thumbs = "Mbj_Thumbs";
            public const string Tags_Tags = "Mbj_Tags";

            public const string Movies_Movies = "Mbj_Movies";
            public const string Movies_MovieCovers = "Mbj_MovieCovers";
            public const string Movies_MoviePhotos = "Mbj_MoviePhotos";
            public const string Movies_MovieSeries = "Mbj_MovieSeries";
            public const string Movies_MovieParticipants = "Mbj_MovieParticipants";
            public const string Movies_MovieTorrents = "Mbj_MovieTorrents";
            public const string Movies_FilmMans = "Mbj_FilmMans";
            public const string Moveis_FilmManPhotos = "Mbj_FilmManPhotos";

            public const string Collection_MovieCollections = "Mbj_MovieCollections";
            public const string Collection_MovieCollectionItems = "Mbj_MovieCollectionItems";


            public const string Spiders_Tasks = "Mbj_Spiders_Tasks";
            public const string Spiders_TaskLogs = "Mbj_Spiders_TaskLogs";
            public const string Spiders_DoubanMovieLinks = "Mbj_Spiders_DoubanMovieLinks";

            #region Adults
            public const string Adults_Actors = "Mbj_Adults_Actors";
            public const string Adults_ActorPhotos = "Mbj_Adults_ActorPhotos";
            public const string Adults_Movies = "Mbj_Adults_Movies";
            public const string Adults_MovieActors = "Mbj_Adults_MovieActors";
            public const string Adults_SpiderLinks = "Mbj_Adults_SpiderLinks";
            #endregion

        }
    }
}
