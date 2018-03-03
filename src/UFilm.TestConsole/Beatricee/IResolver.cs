using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFilm.TestConsole.Beatricee
{
    public interface IResolver
    {
        MovieInfo Resolve(string txtFilePath);

    }
}
