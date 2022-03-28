using EBookStore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore
{
    public partial class BookList : System.Web.UI.Page
    {
        private BookManager _bookMgr = new BookManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                var list = this._bookMgr.GetBookList();
                string keyword = this.Request.QueryString["keyword"];
                var searchList = this._bookMgr.GetSearchResult(keyword);

                if (list.Count == 0)
                {
                    this.rptList.Visible = false;
                    this.plcEmpty.Visible = true;
                }
                else if (searchList.Count > 0)
                {
                    this.rptList.DataSource = searchList;
                    this.rptList.DataBind();

                    this.plcEmpty.Visible = false;
                    this.rptList.Visible = true;
                }
                else
                {
                    this.rptList.Visible = true;
                    this.plcEmpty.Visible = false;

                    this.rptList.DataSource = list;
                    this.rptList.DataBind();
                }
            }
        }
    }
}