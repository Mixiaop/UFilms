using U.Utilities.Web;
using UFilm.Console.UI;

namespace UFilm.Console.Models
{
    public abstract class PagingModel : ModelBase
    {
        public PagingModel()
        {
            PageIndex = WebHelper.GetInt("page", 1);
            PageSize = 16;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string PagingHTML { get; set; }

        public void Paging(int totalCount)
        {
            var pagiInfo = new PagingInfo();
            pagiInfo.PageIndex = PageIndex;
            pagiInfo.PageSize = PageSize;
            pagiInfo.Url = WebHelper.GetThisPageUrl(true);
            pagiInfo.TotalCount = totalCount;
            PagingHTML = new Paginations(pagiInfo).GetPaging();
        }
    }
}