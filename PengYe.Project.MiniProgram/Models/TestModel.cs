using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PengYe.Project.MiniProgram.Models
{
    public class TestModel
    {
        [Display(Name = "标题")]
        [Required]
        public string Title
        {
            get;
            set;
        }
        [Display(Name = "内容")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Content
        {
            get;
            set;
        }
        public string AttachmentPath
        {
            get;
            set;
        }
    }
}