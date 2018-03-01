using System;

namespace UFilm.Services.Spiders.Events
{
    
    public class OnStartEventArgs
    {
        public Uri Uri { get; set; }

        public OnStartEventArgs(Uri uri)
        {
            this.Uri = uri;
        }
    }
}
