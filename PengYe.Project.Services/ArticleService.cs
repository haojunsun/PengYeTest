using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PengYe.Project.Infrastructure;
using PengYe.Project.Log;
using PengYe.Project.Model;
using PengYe.Project.Repository;

namespace PengYe.Project.Services
{
    public interface IArticleService : IDependency
    {
        IEnumerable<Article> List();
        void Add(Article article);
        void Update(Article article);
        void Delete(int id);
        Article Get(int id);

        IEnumerable<Article> GetByChannelTag(string tagName, int pageIndex, int pageSize, ref int totalCount);
        IEnumerable<Article> GetByColumnTag(string tagName, int pageIndex, int pageSize, ref int totalCount);

        IEnumerable<Article> GetColumn(int exid, string tagName, string channel, int pageIndex, int pageSize, ref int totalCount);
        Article GetFirst(string tagName, string channel);
    }

    public class ArticleService : IArticleService
    {
        private readonly MyDbContext _appDbContext;
        private readonly ILogService _log;
        public ArticleService(MyDbContext appDbContext, ILogService logService)
        {
            _appDbContext = appDbContext;
            _log = logService;
        }

        public IEnumerable<Article> List()
        {
            _log.Debug("记录一下");
            return _appDbContext.Articles.OrderByDescending(x => x.CreatedUtc).ToList();
        }

        public void Add(Article article)
        {
            _appDbContext.Articles.Add(article);
            _appDbContext.SaveChanges();
        }

        public void Update(Article article)
        {
            //_appDbContext.Articles.Attach(article);
            _appDbContext.Entry(article).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var article = _appDbContext.Articles.Find(id);

            if (article == null)
            {
                return;
            }

            _appDbContext.Articles.Remove(article);
            _appDbContext.SaveChanges();
        }

        public Article Get(int id)
        {
            return _appDbContext.Articles.Find(id);
        }

        public IEnumerable<Article> GetByChannelTag(string tagName, int pageIndex, int pageSize, ref int totalCount)
        {
            var list = (from p in _appDbContext.Articles
                where p.ChannelTags.Contains(tagName)
                orderby p.CreatedUtc descending
                select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = _appDbContext.Articles.Count(x => x.ChannelTags.Contains(tagName));
            return list.ToList();
        }

        public IEnumerable<Article> GetByColumnTag(string tagName, int pageIndex, int pageSize, ref int totalCount)
        {
            var list = (from p in _appDbContext.Articles
                where p.ColumnTags == tagName
                orderby p.CreatedUtc descending
                select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = _appDbContext.Articles.Count(x => x.ColumnTags == tagName);
            return list.ToList();
        }

        public IEnumerable<Article> GetColumn(int exid, string tagName, string channel, int pageIndex, int pageSize, ref int totalCount)
        {
            var list = (from p in _appDbContext.Articles
                where p.ColumnTags.Contains(tagName) && p.IsDraft == 0 && p.IsRelease == 1 && p.ArticleId != exid && p.ChannelTags.Contains(channel)
                orderby p.CreatedUtc descending
                select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = _appDbContext.Articles.Count(x => x.ColumnTags.Contains(tagName) && x.IsDraft == 0 && x.IsRelease == 1 && x.ArticleId != exid && x.ChannelTags.Contains(channel));
            return list.ToList();
        }

        public Article GetFirst(string tagName, string channel)
        {
            return _appDbContext.Articles.Where(x => x.ColumnTags.Contains(tagName) && x.IsDraft == 0 && x.IsRelease == 1 && x.ChannelTags.Contains(channel)).OrderByDescending(x => x.CreatedUtc).FirstOrDefault();
        }
    }
}
