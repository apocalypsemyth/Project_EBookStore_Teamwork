using EBookStore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore.BackAdmin
{
    public partial class PaymentList : System.Web.UI.Page
    {
        private AccountManager _accountMgr = new AccountManager();
        private PaymentManager _paymentMgr = new PaymentManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this._accountMgr.IsLogined())
                this.Response.Redirect("~/Login.aspx");

            if (!this.IsPostBack)
            {
                var paymentList = this._paymentMgr.GetPaymentList();

                if (paymentList.Count == 0)
                {
                    this.gvPaymentList.Visible = false;
                    this.plcEmpty.Visible = true;
                }
                else
                {
                    this.gvPaymentList.Visible = true;
                    this.plcEmpty.Visible = false;

                    this.gvPaymentList.DataSource = paymentList;
                    this.gvPaymentList.DataBind();
                }
            }
        }

        protected void btnCreatePayment_Click(object sender, EventArgs e)
        {
            Guid paymentID = Guid.NewGuid();
            this.Response.Redirect("PaymentDetail.aspx?ID=" + paymentID);
        }

        protected void btnDeletePayment_Click(object sender, EventArgs e)
        {
            List<Guid> paymentIDList = new List<Guid>();

            foreach (GridViewRow gvPaymentRow in this.gvPaymentList.Rows)  
            {
                CheckBox ckbDel = gvPaymentRow.FindControl("ckbDel") as CheckBox;     
                HiddenField hfID = gvPaymentRow.FindControl("hfID") as HiddenField;   

                if (ckbDel != null && hfID != null)    
                {
                    if (ckbDel.Checked)    
                    {
                        if (Guid.TryParse(hfID.Value, out Guid paymentID))
                            paymentIDList.Add(paymentID);
                    }

                }
            }

            if (paymentIDList.Count > 0)  
            {                                                            
                this._paymentMgr.BatchDeletePayment(paymentIDList);
                this.Response.Redirect(this.Request.RawUrl);
            }
        }
    }
}