using System;
using System.Threading.Tasks;
using UFilm.Services.Spiders.Events;

namespace UFilm.Services.Spiders
{
    public interface ISpider
    {
        event EventHandler<OnStartEventArgs> OnStart;

        event EventHandler<OnCompletedEventArgs> OnCompleted;

        event EventHandler<OnErrorEventArgs> OnError;

        void Start(Uri uri, Script script = null, Operation operation = null);
    }
}
