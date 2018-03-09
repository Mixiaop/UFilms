using System.Text;
using System.Text.RegularExpressions;

namespace UFilm.Web
{
    /// <summary>
    /// 数据分页操作类
    /// </summary>
    public class Paginations
    {
        StringBuilder sb;
        PagingInfo info;
        public Paginations(PagingInfo info)
        {
            this.info = info;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <returns></returns>
        public string GetPaging()
        {
            #region 简单分页
            sb = new StringBuilder();
            //判断如果当前页为负数或者零时，NowPage=1 
            if (info.PageIndex <= 0)
                info.PageIndex = 1;

            //判断如果当前页大于总页数时，PageIndex=PageCount 
            if (info.PageIndex >= info.PageCount)
            {
                info.PageIndex = info.PageCount;
            }

            if (info.PageCount <= 1)
            {
                return "";
            }

            //上一页if (pageIndex > 1)
            if (info.PageIndex != 1)
            {
                sb.Append("<li class=\"next\"><a href=\"" + ReplaceUrlParam(info.Url, info.Param, 1, info.IsRewriter) + "\" title=\"首页\">首页</a></li>");
                sb.Append("<li class=\"next\"><a href=\"" + ReplaceUrlParam(info.Url, info.Param, (info.PageIndex - 1), info.IsRewriter) + "\" title=\"前页\">上一页</a></li>");
            }

            //如果分页总页数小于页码数（PageNumber）时，不显示省略号(...) 
            if (info.PageCount <= info.PageNumber)
            {
                for (int i = 1; i <= info.PageCount; i++)
                    GetPageNumberStr(i);
            }
            else
            {
                if (info.PageNumber % 2 != 0)
                {
                    info.LeftPageNumberOne = (info.PageNumber / 2) + 1;
                    info.LeftPageNumberTwo = (info.PageNumber / 2);
                    info.RightPageNumber = (info.PageNumber / 2);
                }
                else
                {
                    info.LeftPageNumberOne = (info.PageNumber / 2) + 1;
                    info.LeftPageNumberTwo = (info.PageNumber / 2);
                    info.RightPageNumber = (info.PageNumber / 2) - 1;
                }

                //总页数大于页码数（PageNumber）时（简单处理--算法） 
                if (info.PageIndex > info.LeftPageNumberOne && info.PageIndex < info.PageCount - info.RightPageNumber)
                {
                    sb.Append("<li><a >...</a></li>");
                    for (int i = info.PageIndex - info.LeftPageNumberTwo; i <= info.PageIndex + info.RightPageNumber; i++)
                        GetPageNumberStr(i);
                    sb.Append("<li><a >...</a></li>");
                }
                else if (info.PageIndex >= info.PageCount - info.RightPageNumber)
                {
                    sb.Append("<li><a >...</a></li>");
                    for (int i = (info.PageCount - info.PageNumber) + 1; i <= info.PageCount; i++)
                        GetPageNumberStr(i);
                }
                else
                {
                    for (int i = 1; i <= info.PageNumber; i++)
                        GetPageNumberStr(i);
                    sb.Append("<li><a >...</a></li>");
                }
            }

            //下一页和末页 
            if (info.PageIndex != info.PageCount)
            {
                sb.Append("<li class=\"next\"><a href=\"" + ReplaceUrlParam(info.Url, info.Param, (info.PageIndex + 1), info.IsRewriter) + "\" title=\"后页\">下一页</a></li>");

                sb.Append("<li class=\"next\"><a href=\"" + ReplaceUrlParam(info.Url, info.Param, (info.PageCount), info.IsRewriter) + "\" title=\"尾页\">尾页</a></li>");
            }
            sb.Append(string.Format("<li class=\"totalcount\"><a style=\"color:#999\">（共 {0} 条记录）</a></li>", info.TotalCount));
            return sb.ToString();
            #endregion
        }

        /// <summary>
        /// 获取AJAX分页
        /// </summary>
        /// <returns></returns>
        public string GetAjaxPaging()
        {
            #region Ajax分页
            sb = new StringBuilder();
            //判断如果当前页为负数或者零时，NowPage=1 
            if (info.PageIndex <= 0)
                info.PageIndex = 1;

            //判断如果当前页大于总页数时，PageIndex=PageCount 
            if (info.PageIndex >= info.PageCount)
            {
                info.PageIndex = info.PageCount;
            }

            if (info.PageCount <= 1)
            {
                return "";
            }

            //上一页if (pageIndex > 1)
            if (info.PageIndex != 1)
            {
                sb.Append("<li class='prev'><a href='javascript:;' onclick='pageList(this, \\\"" + ReplaceUrlParam(info.Url, info.Param, (info.PageIndex - 1), false) + "\\\", \\\"" + info.AjaxTarget + "\\\");' title='前页'><</a></li>");
            }

            //如果分页总页数小于页码数（PageNumber）时，不显示省略号(...) 
            if (info.PageCount <= info.PageNumber)
            {
                for (int i = 1; i <= info.PageCount; i++)
                    GetAjaxPageNumberStr(i);
            }
            else
            {
                if (info.PageNumber % 2 != 0)
                {
                    info.LeftPageNumberOne = (info.PageNumber / 2) + 1;
                    info.LeftPageNumberTwo = (info.PageNumber / 2);
                    info.RightPageNumber = (info.PageNumber / 2);
                }
                else
                {
                    info.LeftPageNumberOne = (info.PageNumber / 2) + 1;
                    info.LeftPageNumberTwo = (info.PageNumber / 2);
                    info.RightPageNumber = (info.PageNumber / 2) - 1;
                }

                //总页数大于页码数（PageNumber）时（简单处理--算法） 
                if (info.PageIndex > info.LeftPageNumberOne && info.PageIndex < info.PageCount - info.RightPageNumber)
                {
                    sb.Append("<li><a >...</a></li>");
                    for (int i = info.PageIndex - info.LeftPageNumberTwo; i <= info.PageIndex + info.RightPageNumber; i++)
                        GetAjaxPageNumberStr(i);
                    sb.Append("<li><a >...</a></li>");
                }
                else if (info.PageIndex >= info.PageCount - info.RightPageNumber)
                {
                    sb.Append("<li><a >...</a></li>");
                    for (int i = (info.PageCount - info.PageNumber) + 1; i <= info.PageCount; i++)
                        GetAjaxPageNumberStr(i);
                }
                else
                {
                    for (int i = 1; i <= info.PageNumber; i++)
                        GetAjaxPageNumberStr(i);
                    sb.Append("<li><a >...</a></li>");
                }
            }

            //下一页和末页 
            if (info.PageIndex != info.PageCount)
            {
                sb.Append("<li class='next'><a href='javascript:;' onclick='pageList(this, \\\"" + ReplaceUrlParam(info.Url, info.Param, (info.PageIndex + 1), false) + "\\\", \\\"" + info.AjaxTarget + "\\\");' title='后页'>></a></li>");
            }
            return sb.ToString();
            #endregion
        }


        #region 替换page参数
        string ReplaceUrlParam(string url, string param, int value, bool isRewriter)
        {
            if (isRewriter)
            {
                return url.Replace("{*}", value.ToString());
            }
            else
            {
                if (Regex.IsMatch(url, "[?&]" + param + "=", RegexOptions.IgnoreCase))
                {
                    return Regex.Replace(url, param + "=[\\d]*", param + "=" + value, RegexOptions.IgnoreCase);
                }
                else
                {
                    return url.IndexOf("?") == -1 ? url + "?page=" + value : url + "&page=" + value;
                }
            }
        }
        #endregion

        #region 拼接页码方法
        void GetPageNumberStr(int i)
        {
            if (info.PageIndex == i)
            {
                sb.Append("<li class=\"active\"><a>" + i + "</a></li>");
            }
            else
            {
                sb.Append("<li><a href=\"" + ReplaceUrlParam(info.Url, info.Param, i, info.IsRewriter) + "\">" + i + "</a></li>");
            }
        }
        #endregion

        #region 拼接AJAX页码方法
        void GetAjaxPageNumberStr(int i)
        {
            if (info.PageIndex == i)
            {
                sb.Append("<li class=\'active\'><a>" + i + "</a></li>");
            }
            else
            {
                sb.Append("<li><a href='javascript:;' onclick='pageList(this, \\\"" + ReplaceUrlParam(info.Url, info.Param, i, false) + "\\\", \\\"" + info.AjaxTarget + "\\\");'>" + i + "</a></li>");
            }
        }
        #endregion

    }
    #region 分页信息描述
    /// <summary>
    /// 分页实体类
    /// </summary>
    public class PagingInfo
    {
        int _pageNumber = 10;

        /// <summary> 
        /// 当前页 
        /// </summary> 
        public int PageIndex { get; set; }

        /// <summary> 
        /// 每页显示的记录条数 
        /// </summary> 
        public int PageSize { get; set; }

        /// <summary> 
        /// 总条数 
        /// </summary> 
        public int TotalCount { get; set; }

        /// <summary>
        /// 计算总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (PageSize == 0)
                    PageSize = 20;//每页显示的记录条数为"0",则默认为"20" 
                if (TotalCount % PageSize == 0)
                    return (TotalCount / PageSize);
                else
                    return (TotalCount / PageSize) + 1;
            }
        }
        /// <summary> 
        /// 页码（意思是前台分页控件显示的页数） 
        /// </summary> 
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = value;
            }
        }

        /// <summary> 
        /// 当前页面路径
        /// </summary> 
        public string Url { get; set; }

        /// <summary>
        /// 是否URL重写
        /// </summary>
        public bool IsRewriter { get; set; }

        /// <summary>
        /// page占位符
        /// </summary>
        public string Param
        {
            get { return "page"; }
        }

        /// <summary>
        /// Ajax分页目标容器
        /// </summary>
        public string AjaxTarget { get; set; }

        /// <summary> 
        /// 左页码属性一 
        /// </summary> 
        public int LeftPageNumberOne { get; set; }
        /// <summary> 
        /// 左页码属性二 
        /// </summary> 
        public int LeftPageNumberTwo { get; set; }
        /// <summary> 
        /// 右页码 
        /// </summary> 
        public int RightPageNumber { get; set; }
    }
    #endregion
}
