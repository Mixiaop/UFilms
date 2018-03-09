using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFilm.Domain.Adults;

namespace UFilm.Services.Adults
{
    /// <summary>
    /// 采集服务 
    /// </summary>
    public interface ISpiderService : U.Application.Services.IApplicationService
    {
        #region SpiderLink

        bool Exists(string link);

        SpiderLink Get(int linkId);

        List<SpiderLink> GetListByUnUsed(SpiderLinkSource source = SpiderLinkSource.WwwNH87cn, int count = 0, OrderBy orderBy = OrderBy.CreationTimeDesc);

        int GetCountByUnUsed(SpiderLinkSource source = SpiderLinkSource.WwwNH87cn);

        void InsertLink(SpiderLink link, SpiderLinkSource source = SpiderLinkSource.WwwNH87cn);

        void Update(SpiderLink link);
        #endregion
    }

    public enum SpiderLinkSource { 
        WwwNH87cn
    }
}
