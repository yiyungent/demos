using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace LuceneNetDemo.Controllers
{
    public class SimpleSearchController : Controller
    {
        string indexPath = HostingEnvironment.MapPath(@"~/LuceneDir");

        // GET: SimpleSearch
        public ActionResult Index(string txtSearch, string btnSearch, string btnCreate)
        {
            IList<SearchResult> list = null;
            if (!string.IsNullOrEmpty(btnSearch))
            {
                // 若单击的是查询按钮
                list = Search(txtSearch);
            }
            else if (!string.IsNullOrEmpty(btnCreate))
            {
                // 若单击的是创建索引按钮
                string msg = CreateIndex();
                ViewData["ShowInfo"] = string.IsNullOrEmpty(msg) ? "创建成功" : msg;
            }

            return View();
        }

        #region 查询
        private List<SearchResult> Search(string kw)
        {
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearch
        }
        #endregion
    }
}