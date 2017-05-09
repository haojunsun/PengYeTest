using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PengYe.Project.Log;
using PengYe.Project.Model;
using PengYe.Project.Services;

namespace PengYe.Project.MiniProgram.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ILogService _log;

        public HomeController(IArticleService articleService,ILogService logService)
        {
            _articleService = articleService;
            _log = logService;
        }

        public ActionResult Index()
        {
            var model = _articleService.List().ToList();
            var article=new Article()
            {
                ArticleId=0,
                Body="body",
                CatchLine="cl",
                ChannelTags="ct"
            };
            model.Add(article);
          
            return View(model);
        }


        public ActionResult Test()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}