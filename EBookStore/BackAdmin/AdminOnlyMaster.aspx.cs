using EBookStore.Models;
using EBookStore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project.BackAdmin

{
    public partial class AdminOnlyMaster : System.Web.UI.Page
    {

        private AccountManager _mgr = new AccountManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this._mgr.IsLogined())
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!this.IsPostBack)
            {
                MemberAccount account = this._mgr.GetCurrentUser();
                if(account==null)
                {
                    Response.Redirect("~/Login.aspx");
                }

                this.ltlAccount.Text = account.Account;

                string keyword = this.Request.QueryString["keyword"];
                this.txtKeyword.Text = keyword;

                List<MemberAccount> list = this._mgr.GetAccountList(keyword);
                if (list.Count > 0)
                {
                    this.gvList.DataSource = list;
                    this.gvList.DataBind();

                    this.plcEmpty.Visible = false;
                    this.gvList.Visible = true;
                }
                else
                {
                    this.plcEmpty.Visible = true;
                    this.gvList.Visible = false;
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            this._mgr.Logout();
            Response.Redirect("~/Login.aspx");
        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<Guid> idList = new List<Guid>();
            foreach (GridViewRow gRow in this.gvList.Rows)
            {
                CheckBox ckbDel = gRow.FindControl("ckbDel") as CheckBox;
                HiddenField hfID = gRow.FindControl("hfID") as HiddenField;

                if (ckbDel != null && hfID != null)
                {
                    if (ckbDel.Checked)
                    {
                        Guid id;
                        if (Guid.TryParse(hfID.Value, out id))
                            idList.Add(id);
                    }
                }
            }
            if (idList.Count > 0)
            {
                this._mgr.DeleteAccounts(idList);
                this.Response.Redirect(this.Request.RawUrl);
            }
        }
        protected void btnWrite_Click(object sender, EventArgs e)
        {



        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminOnlyMaster.aspx");
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditWithMain.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = this.txtKeyword.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
                Response.Redirect("AdminOnlyMaster.aspx");
            else
                Response.Redirect("AdminOnlyMaster.aspx?keyword=" + keyword);
        }

        protected void BtnLogout_Click1(object sender, EventArgs e)
        {
            this._mgr.Logout();
            Response.Redirect("~/Login.aspx");
        }

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    string Keyword = this.txtKeyword.Text;
        //    List<MemberAccount> KeywordSendback = _mgr.GetAccountList(Keyword);
        //    this.GridView1.DataSource = KeywordSendback;
        //    this.GridView1.DataBind();






        //}
    }
}