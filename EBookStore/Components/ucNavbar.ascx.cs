using EBookStore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore.Components
{
    public partial class ucNavbar : System.Web.UI.UserControl
    {
        private AccountManager _accountMgr = new AccountManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this._accountMgr.IsLogined())
            {
                var currentUser = this._accountMgr.GetCurrentUser();
                Guid userID = currentUser.UserID;

                this.btn_Login.Visible = false;
                this.btn_Logout.Visible = true;
                this.aLinkMyBookList.HRef = "~/MyBookList.aspx?ID=" + userID;
                this.aLinkShoppingCart.HRef = "~/OrderDetail.aspx?ID=" + userID;
            }
            else
            {
                this.btn_Logout.Visible = false;
                this.btn_Login.Visible = true;
                this.aLinkMyBookList.HRef = "~/Login.aspx";
                this.aLinkShoppingCart.HRef = "~/Login.aspx";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = this.txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
                Response.Write("<script>alert('請輸入關鍵字')</script>");
            else
                Response.Redirect("SearchedBookList.aspx?keyword=" + keyword);
        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Login.aspx");
        }

        protected void btn_Logout_Click(object sender, EventArgs e)
        {
            this._accountMgr.Logout();
            this.Response.Redirect("~/BookList.aspx");
        }
    }
}