using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UFilm.TestConsole.Beatricee
{
    public class Resolver: IResolver
    {
        public MovieInfo Resolve(string txtFilePath) {
            MovieInfo info = null;
            if (File.Exists(txtFilePath))
            {
                info = new MovieInfo();
                var html = File.ReadAllText(txtFilePath);


            }

            return info;
        }
    }
}
