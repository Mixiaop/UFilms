using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFilm.Domain.Seo
{
    /// <summary>
    /// 当前站点的URL
    /// 电影、影人、影集等页面的URL，记录主动向百度接口推送URL
    /// 接口调用地址： http://data.zz.baidu.com/urls?site=www.mbjuan.com&token=fwbdEoJY79JlckET
    /// http://www.example.com/1.html
    /// http://www.example.com/2.html
    /// 成功返回{
    ///"remain":4999998,
    ///"success":2,
    ///"not_same_site":[],
    ///"not_valid":[]
    ///}
    /// </summary>
    public class SiteUrl
    {
    }
}
