using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PengYe.Project.Model
{
    public class Article
    {
        public int ArticleId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 副标题--备用
        /// </summary>
        [MaxLength(30)]
        public string CatchLine { get; set; }

        /// <summary>
        /// 文章大图
        /// </summary>
        public string TitleImageUrl { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [MaxLength]
        public string Body { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedUtc { get; set; }

        /// <summary>
        /// 分类标签（频道）
        /// </summary>
        public string ChannelTags { get; set; }

        /// <summary>
        /// 分类标签（栏目）
        /// </summary>
        public string ColumnTags { get; set; }

        /// <summary>
        /// 草稿--默认1是 发布后自动变0
        /// </summary>
        public int IsDraft { get; set; }

        /// <summary>
        /// 发布状态--默认0 发布1
        /// </summary>
        public int IsRelease { get; set; }

        /// <summary>
        /// 其他图--预留
        /// </summary>
        public string OtherImageUrl { get; set; }

        /// <summary>
        /// 其他图--预留
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 维护的管理员
        /// </summary>
        public string ManagerName { get; set; }

        /// <summary>
        /// 视频
        /// </summary>
        public string MediaUrl { get; set; }
    }
}
