using EBookStore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore
{
    public partial class MyBookList : System.Web.UI.Page
    {
        private BookManager _bookMgr = new BookManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 取得 keyword 的 querystring ，以及還原至 textbox
                string userID = this.Request.QueryString["ID"];
                ////EBookStore.txtKeyword.Text = keyword;

                var list = this._bookMgr.GetMyBookList(userID);

                if (list.Count == 0)
                {
                    this.h3MyBookListTitle.Visible = false;
                    this.rptList.Visible = false;
                    this.plcEmpty.Visible = true;
                }
                else
                {
                    this.h3MyBookListTitle.Visible = true;
                    this.rptList.Visible = true;
                    this.plcEmpty.Visible = false;

                    this.rptList.DataSource = list;
                    this.rptList.DataBind();
                }
            }
        }
    }
}