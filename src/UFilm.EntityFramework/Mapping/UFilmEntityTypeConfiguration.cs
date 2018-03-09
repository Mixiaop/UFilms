using System;
using System.Data.Entity.ModelConfiguration;


namespace UFilm.EntityFramework.Mapping
{
    public abstract class UFilmEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        protected UFilmEntityTypeConfiguration()
        {
            PostInitialize();
        }

        protected virtual void PostInitialize()
        {

        }
    }
}
