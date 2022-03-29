using EBookStore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBookStore.API
{
    /// <summary>
    /// Summary description for BookListDataHandler
    /// </summary>
    public class BookListDataHandler : IHttpHandler
    {
        private string _failedResponse = "NULL";
        private BookManager _bookMgr = new BookManager();

        public void ProcessRequest(HttpContext context)
        {
            if (string.Compare("GET", context.Request.HttpMethod, true) == 0)
            {
                var bookList = this._bookMgr.GetBookList();
                if (bookList == null && bookList.Count < 4)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(_failedResponse);
                    return;
                }

                var threeBookList = bookList.Take(3).ToList();
                var resultBookList = this._bookMgr.BuildBookModelList(threeBookList);
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(resultBookList);

                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
                return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}