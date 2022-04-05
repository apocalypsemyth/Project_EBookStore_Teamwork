using EBookStore.Managers;
using EBookStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore
{
    public partial class MyBookDownload : System.Web.UI.Page
    {
        private BookManager _bookMgr = new BookManager();
        private AccountManager _Amgr = new AccountManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            string bookIDText = this.Request.QueryString["ID"];
            string userid = this._Amgr.GetCurrentUser().UserID.ToString();

            //檢查是否登入
            if (!this._Amgr.IsLogined())
            {
                Response.Redirect("~/Login.aspx");
            }

            //檢查是否真的有購買
            if (_bookMgr.CheckMyBookList(userid, bookIDText).Count() == 0)
            {
                Response.Redirect("~/BookDetail.aspx?ID=" + bookIDText);              
            }

            // 如果沒有帶 id ，跳回列表頁
            if (string.IsNullOrWhiteSpace(bookIDText))
                Response.Redirect("BookList.aspx", true);
            else
            {
                bool isValidbookID = Guid.TryParse(bookIDText, out Guid bookid);

                string fileName = this._bookMgr.GetBookFileURL(bookid);
                string filePath = Server.MapPath(fileName);

                byte[] sourceBytes = File.ReadAllBytes(filePath);
                string newFileName =
                $"{DateTime.Now.ToString("yyyyMMdd_HHmmss")}{Path.GetExtension(filePath)}";

                Response.ContentType = "application/download";
                Response.AddHeader(
                "Content-Disposition",
                $"attachment; filename={newFileName}");

                Response.Clear();
                Response.BinaryWrite(sourceBytes);

                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
    }
}