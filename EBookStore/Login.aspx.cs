using EBookStore.Managers;
using EBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore
{
    public partial class Login : System.Web.UI.Page
    {
        private AccountManager _mgr = new AccountManager();
                
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {                
                if (this._mgr.IsLogined())
                {
                    this.plcUserInfo.Visible = true;
                    this.plcLogin.Visible = false;
                    MemberAccount account = this._mgr.GetCurrentUser();
                    this.ltlAccount.Text = account.Account;
                }
                else
                {
                    this.plcLogin.Visible = true;
                    this.plcUserInfo.Visible = false;
                }
            }            
        }

        /// <summary>
        /// 確認管理者登入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string account = this.txtAccount.Text.Trim();
            string pwd = this.txtPassword.Text.Trim();
            if (this._mgr.TryLogin(account, pwd)) //沒寫true or false =為true的省略寫法
            {
                //Response.Redirect(previousURL);
                //Response.Redirect(ViewState["prevUrl"].ToString());
                Response.Redirect("~/BackAdmin/AdminOnlyMaster.aspx");
                //Response.Redirect(Request.RawUrl);
                //Response.Write("<script language=javascript>history.go(-2);</script>");                                
            }
            if (this._mgr.GuestLogin(account, pwd)) //沒寫true or false =為true的省略寫法 
            {
                Response.Redirect("~/BookList.aspx");
                //Response.Redirect(Request.RawUrl);
            }
            else
            {
                this.ltlMessage.Text = "登入失敗";
            }
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("BackAdmin/EditWithMain.aspx");
        }
    }
}