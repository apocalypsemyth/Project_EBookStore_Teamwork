using EBookStore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore
{
    public partial class Main : System.Web.UI.MasterPage
    {
        private BookManager _bookMgr = new BookManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ucNavbar.BtnSearchClick += SearchBook;
        }

        protected void SearchBook(object sender, string searchText)
        {
            var ctphr = this.ContentPlaceHolder1;
            var rptList = (Repeater)ctphr.FindControl("rptList");
            var plcEmpty = (PlaceHolder)ctphr.FindControl("plcEmpty");

            if (rptList != null && plcEmpty != null)
            {
                var list = this._bookMgr.GetBookList(searchText);

                if (list.Count == 0)
                {
                    rptList.Visible = false;
                    plcEmpty.Visible = true;
                }
                else
                {
                    rptList.Visible = true;
                    plcEmpty.Visible = false;

                    rptList.DataSource = list;
                    rptList.DataBind();
                }
            }
        }
    }
}