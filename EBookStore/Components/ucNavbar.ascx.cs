using EBookStore.Managers;
using EBookStore.Models;
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
        public delegate void BtnSearch(object sender, string SearchText);
        public event BtnSearch BtnSearchClick = null;
        private AccountManager _mgr = new AccountManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this._mgr.IsLogined())
            {
                this.btn_Login.Visible = false;
                this.btn_Logout.Visible = true;
            }

            else
            {
                this.btn_Logout.Visible = false;
                this.btn_Login.Visible = true;

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = this.txtSearch.Text;

            if (!string.IsNullOrWhiteSpace(searchText) && this.BtnSearchClick != null)
                this.BtnSearchClick(this, searchText);

            this.txtSearch.Text = "";
        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void btn_Logout_Click(object sender, EventArgs e)
        {
            this._mgr.Logout();
            Response.Redirect("~/BookList.aspx");
        }
    }
}