using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U.Application.Services;
using U.Application.Services.Dto;

namespace UFilm.Services.Awards
{
    public interface IOscarAwardService : IApplicationService
    {
        /// <summary>
        /// 获取奖项名称列表 [中文名：英文名]
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetAwardNameList();
    }
}
