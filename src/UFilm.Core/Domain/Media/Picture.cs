using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFilm.Domain.Media
{
    /// <summary>
    /// 代表一张“图片”包括源图、缩略片、小方块
    /// </summary>
    public class Picture
    {
        public Picture() {
            SourceUrl = "";
            ThumbUrl = "";
            ThumbSize = 0;
            SquareUrl = "";
            SquareSize = 0;
        }

        public string SourceUrl { get; set; }

        public string ThumbUrl { get; set; }

        public int ThumbSize { get; set; }

        public string SquareUrl { get; set; }

        public int SquareSize { get; set; }
    }
}
