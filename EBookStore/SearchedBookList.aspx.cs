using EBookStore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore
{
    public partial class SearchedBookList : System.Web.UI.Page
    {
        private BookManager _bookMgr = new BookManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string keyword = this.Request.QueryString["keyword"];
                var searchList = this._bookMgr.GetSearchResult(keyword);

                if (searchList.Count == 0)
                {
                    this.rptSearchedBookList.Visible = false;
                    this.plcEmpty.Visible = true;
                }
                else
                {
                    this.rptSearchedBookList.Visible = true;
                    this.plcEmpty.Visible = false;

                    this.rptSearchedBookList.DataSource = searchList;
                    this.rptSearchedBookList.DataBind();
                }
            }
        }
    }
}