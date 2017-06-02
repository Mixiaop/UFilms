using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFilm.Domain.Spiders;

namespace UFilm.Services.Spiders
{
    /// <summary>
    /// “豆瓣链接服务”记录豆瓣的电影或影人等链接
    /// </summary>
    public interface IDoubanLinkService : U.Application.Services.IApplicationService
    {
        bool Exists(string link);

        List<DoubanMovieLink> GetListByUnUsed(int count = 0);

        int GetCountByUnUsed();

        void Insert(DoubanMovieLink link);

        void Update(DoubanMovieLink link);
    }
}
