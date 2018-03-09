using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFilm.Services.Awards
{
    public class OscarAwardService : IOscarAwardService
    {
        /// <summary>
        /// 获取奖项名称列表 [中文名：英文名]
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAwardNameList() {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("最佳影片", "Best Motion Picture of the Year");
            dict.Add("最佳影片(艺术类)", "Best Motion Picture of the Year, Unique and Artistic");
            dict.Add("最佳导演", "Best Achievement in Directing");
            dict.Add("最佳导演(喜剧类)", "Best Achievement in Directing, Comedy Picture");
            dict.Add("最佳导演(剧情类)", "Best Achievement in Directing, Dramatic Picture");
            dict.Add("最佳男主角", "Best Performance by an Actor in a Leading Role");
            dict.Add("最佳女主角", "Best Performance by an Actress in a Leading Role");
            dict.Add("最佳男配角", "Best Performance by an Actor in a Supporting Role");
            dict.Add("最佳女配角", "Best Performance by an Actress in a Supporting Role");
            dict.Add("最佳动画长片", "Best Animated Feature Film of the Year");
            dict.Add("最佳编剧", "Best Writing, Achievement");
            dict.Add("最佳原创剧本", "Best Writing, Original Screenplay");
            dict.Add("最佳改编剧本", "Best Writing, Adapted Screenplay ");
            dict.Add("最佳艺术指导", "Best Production Design");
            dict.Add("最佳美术指导", "Best Art Direction");
            dict.Add("最佳外语片", "Best Foreign Language Film of the Year");
            dict.Add("最佳摄影", "Best Cinematography");
            dict.Add("最佳剪辑", "Best Achievement in Film Editing");
            dict.Add("最佳音效剪辑", "Best Sound Editing");
            dict.Add("最佳音响效果", "Best Achievement in Sound Mixing");
            dict.Add("最佳混音效果", "Best Sound Mixing");
            dict.Add("最佳视觉效果", "Best Visual Effects");
            dict.Add("最佳化妆", "Best Makeup and Hairstyling");
            dict.Add("最佳服装设计", "Best Achievement in Costume Design");
            dict.Add("最佳配乐", "Best Achievement in Music Written for Motion Pictures, Original Score");
            dict.Add("最佳配乐(剧情类)", "");
            dict.Add("最佳配乐(音乐、喜剧类)", "");
            dict.Add("最佳歌曲", "Best Achievement in Music Written for Motion Pictures, Original Song");
            dict.Add("最佳作曲", "Best Music, Original Song Score and Its Adaptation or Best Adaptation Score");
            dict.Add("最佳纪录长片", "Best Documentary, Features");
            dict.Add("最佳动画短片", "Best Short Film, Animated");
            dict.Add("最佳真人短片", "Best Short Film, Live Action");
            dict.Add("最佳纪录短片", "Best Documentary, Short Subjects");
            dict.Add("终身成就奖", "Honorary Award");
            dict.Add("特别成就奖 ", "Special Achievement Award");
            dict.Add("埃尔文·G·撒尔伯格纪念奖", "Irving G. Thalberg Memorial Award");
            dict.Add("吉恩·赫肖尔特人道主义奖", "Jean Hersholt Humanitarian Award");
            dict.Add("科技成就奖", "Technical Achievement Award");
            dict.Add("奥斯卡功绩奖", "Academy Award of Merit");
            dict.Add("戈登·E·索耶奖", "Gordon E. Sawyer Award");
            dict.Add("科学与工程奖", "Scientific and Engineering Award");
            return dict;
        }
    }
}
