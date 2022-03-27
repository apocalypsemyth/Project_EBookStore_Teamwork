using EBookStore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore.BackAdmin
{
    public partial class PaymentDetail : System.Web.UI.Page
    {
        private PaymentManager _paymentMgr = new PaymentManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            Guid paymentID = this.GetPaymentID();
            var payment = this._paymentMgr.GetPayment(paymentID);

            if (!this.IsPostBack)
            {
                if (payment == null)
                {
                    this.btnCreatePayment.Visible = true;
                    this.btnUpdatePayment.Visible = false;

                    this.ltlPaymentID.Text = "系統自動生成";
                    this.ltlPaymentDate.Text = "系統自動生成";
                }
                else
                {
                    this.btnCreatePayment.Visible = false;
                    this.btnUpdatePayment.Visible = true;

                    this.ltlPaymentID.Text = payment.PaymentID.ToString();
                    this.ltlPaymentName.Text = payment.PaymentName;
                    this.ltlPaymentDate.Text = payment.PaymentDate.ToString("yyyy年MM月dd日");
                }
            }
        }

        protected void btnCreatePayment_Click(object sender, EventArgs e)
        {
            Guid paymentID = this.GetPaymentID();
            string paymentName = this.txtPaymentName.Text.Trim();

            this._paymentMgr.CreatePayment(paymentID, paymentName);
            this.Response.Redirect("PaymentList.aspx");
        }

        protected void btnUpdatePayment_Click(object sender, EventArgs e)
        {
            Guid paymentID = this.GetPaymentID();
            string paymentName = this.txtPaymentName.Text.Trim();

            this._paymentMgr.UpdatePayment(paymentID, paymentName);
            this.Response.Redirect("PaymentList.aspx");
        }

        protected Guid GetPaymentID()
        {
            string paymentIDStr = this.Request.QueryString["ID"];

            bool isValidPaymentID = Guid.TryParse(paymentIDStr, out Guid paymentID);
            if (!isValidPaymentID)
                this.Response.Redirect("PaymentList.aspx");

            return paymentID;
        }
    }
}