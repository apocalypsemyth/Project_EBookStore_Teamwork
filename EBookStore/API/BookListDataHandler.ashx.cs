using EBookStore.Managers;
using EBookStore.Models;
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
        private BookManager _bookMgr = new BookManager();

        public void ProcessRequest(HttpContext context)
        {
            if (string.Compare("GET", context.Request.HttpMethod, true) == 0 && !string.IsNullOrWhiteSpace(context.Request.QueryString["Name"]))
            {
                string name = context.Request.QueryString["Name"];
                var obj = this._bookMgr.GetBook(name);
                var obj2 = this._bookMgr.BuildBookModel(obj);
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(obj2);

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