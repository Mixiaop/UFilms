﻿using System;

namespace UFilm.Services.Spiders.Events
{
    public class OnErrorEventArgs
    {
        public Uri Uri { get; set; }

        public Exception Exception { get; set; }

        public OnErrorEventArgs(Uri uri,Exception exception) {
            this.Uri = uri;
            this.Exception = exception;
        }
    }
}
