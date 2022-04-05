using EBookStore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore
{
    public partial class OrderDetail : System.Web.UI.Page
    {
        private AccountManager _accountMgr = new AccountManager();
        private PaymentManager _paymentMgr = new PaymentManager();
        private OrderManager _orderMgr = new OrderManager();
        private BookManager _bookMgr = new BookManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            var currentUser = _accountMgr.GetCurrentUser();
            if (currentUser == null)
                this.Response.Redirect("~/Login.aspx");

            Guid userID = currentUser.UserID;

            if (!this.IsPostBack)
            {
                var paymentList = this._paymentMgr.GetPaymentList();
                var orderBookList = this._orderMgr.GetOnlyOneUnfinishOrderItsOrderBookList(userID);
                var bookList = this._bookMgr.GetBookList();
                var resultBookList = this._bookMgr.FilterBookListByOrderBookList(orderBookList, bookList);

                if (resultBookList.Count == 0)
                {
                    this.ddlPaymentList.Visible = false;
                    this.rptOrderBookList.Visible = false;
                    this.plcOrderBookEmpty.Visible = true;
                }
                else
                {
                    this.ddlPaymentList.Visible = true;
                    this.ddlPaymentList.DataTextField = "PaymentName";
                    this.ddlPaymentList.DataValueField = "PaymentID";
                    this.ddlPaymentList.DataSource = paymentList;
                    this.ddlPaymentList.DataBind();

                    this.rptOrderBookList.Visible = true;
                    this.plcOrderBookEmpty.Visible = false;
                    this.rptOrderBookList.DataSource = resultBookList;
                    this.rptOrderBookList.DataBind();
                }
            }
        }
    }
}