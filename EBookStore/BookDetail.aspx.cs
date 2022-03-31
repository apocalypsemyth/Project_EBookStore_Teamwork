using EBookStore.EBookStore.ORM;
using EBookStore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore
{
    public partial class BookDetail : System.Web.UI.Page
    {
        private BookManager _bookMgr = new BookManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            string bookIDText = this.Request.QueryString["ID"];

            // 如果沒有帶 id ，跳回列表頁
            if (string.IsNullOrWhiteSpace(bookIDText))
                this.BackToListPage();

            Guid bookID;
            if (!Guid.TryParse(bookIDText, out bookID))
                this.BackToListPage();

            // 查資料
            Book model = this._bookMgr.GetBook(bookID);
            if (model == null)
                this.BackToListPage();

            // 不開放前台顯示
            if (!model.IsEnable)
                this.BackToListPage();

            // 顯示資料
            this.ShowDetail(model);

            if (!this.IsPostBack)
            {
                var list = this._bookMgr.GetBookList();
                var excludeSelfList = list.Where(item => item.BookID != bookID).ToList();

                if (excludeSelfList.Count == 0)
                    this.rptMayInterestBookList.Visible = false;
                else
                {
                    this.rptMayInterestBookList.DataSource = excludeSelfList;
                    this.rptMayInterestBookList.DataBind();
                }
            }
        }

        private void ShowDetail(Book model)
        {
            this.imgImage.Src = model.Image;
            this.ltlCategoryName.Text = model.CategoryName;
            this.ltlBookName.Text = model.BookName;
            this.ltlAuthorName.Text = model.AuthorName;
            this.ltlDescription.Text = model.Description;
            this.ltlPrice.Text = model.Price.ToString("0") + "元";
            this.ltlDate.Text = model.Date.ToString("yyyy年MM月dd日");

            DateTime? endDate = model.EndDate;
            if (endDate != null)
            {
                this.paraEndDate.Style.Add(HtmlTextWriterStyle.Display, "initial");
                this.ltlEndDate.Text = "結束期： " + endDate?.ToString("yyyy年MM月dd日");
            }
            else
                this.paraEndDate.Style.Add(HtmlTextWriterStyle.Display, "none");
        }

        private void BackToListPage()
        {
            this.Response.Redirect("BookList.aspx", true);
        }
    }
}