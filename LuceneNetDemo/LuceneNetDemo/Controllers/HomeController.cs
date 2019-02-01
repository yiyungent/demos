using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using System.IO;
using System.Text;
using Lucene.Net.Analysis.PanGu;

namespace LuceneNetDemo.Controllers
{
    public class HomeController : Controller
    {
        #region 一元分词
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string txtBody)
        {
            string strResult = AnalyzerResult(txtBody, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29));

            return Content(strResult);
        }
        #endregion

        #region 公共分词方法
        private string AnalyzerResult(string txtBody, Analyzer analyzer)
        {
            TokenStream tokenStream = analyzer.TokenStream("", new StringReader(txtBody));
            Lucene.Net.Analysis.Token token = null;
            StringBuilder sb = new StringBuilder();
            // 新版本 3+ .Next() 已经被废弃
            while ((token = tokenStream.Next()) != null)
            {
                sb.Append(token.TermText() + "\r\n");
            }
            return sb.ToString();
        }
        #endregion

        #region 二元分词
        public ActionResult TwoAnalyzer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TwoAnalyzer(string txtBody)
        {
            string strResult = AnalyzerResult(txtBody, new CJKAnalyzer());

            return Content(strResult);
        }
        #endregion

        #region 盘古分词
        public ActionResult PanGuAnalyzer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PanGuAnalyzer(string txtBody)
        {
            string strResult = AnalyzerResult(txtBody, new PanGuAnalyzer());

            return Content(strResult);
        }
        #endregion
    }
}