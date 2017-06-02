using AutoMapper;
using UFilm.Domain.Movies;
using UFilm.Domain.Media;
using UFilm.Domain.Spiders;
using UFilm.Domain.Collection;
using UFilm.Domain.Adults;
using UFilm.Services.Movies.Dto;
using UFilm.Services.Media.Dto;
using UFilm.Services.Spiders.Dto;
using UFilm.Services.Collection.Dto;
using UFilm.Services.Adults.Dto;

namespace UFilm.Services.Mapping
{
    internal static class CustomDtoMapper
    {
        private static volatile bool _mappedBefore;
        private static readonly object SyncObj = new object();

        public static void CreateMappings()
        {
            lock (SyncObj)
            {
                if (_mappedBefore)
                {
                    return;
                }

                CreateMappingsInternal();

                _mappedBefore = true;
            }
        }

        private static void CreateMappingsInternal()
        {
            Mapper.CreateMap<Picture, PictureDto>().ReverseMap();

            Mapper.CreateMap<Movie, MovieDto>().ReverseMap();
            Mapper.CreateMap<Movie, MovieBriefDto>().ReverseMap();

            Mapper.CreateMap<SpiderTask, SpiderTaskDto>().ReverseMap();
            Mapper.CreateMap<SpiderTaskLog, SpiderTaskLogDto>().ReverseMap();

            Mapper.CreateMap<MovieCollectionItem, MovieCollectionItemDto>().ReverseMap();

            #region Adults
            Mapper.CreateMap<LMovie, LMovieDto>().ReverseMap();
            Mapper.CreateMap<LMovie, LMovieBriefDto>().ReverseMap();

            Mapper.CreateMap<Actor, ActorDto>().ReverseMap();
            Mapper.CreateMap<Actor, ActorBriefDto>().ReverseMap();
            #endregion
        }
    }
}
