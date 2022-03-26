using EBookStore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace EBookStore.API
{
    /// <summary>
    /// Summary description for OrderDetailDataHandler
    /// </summary>
    public class OrderDetailDataHandler : IHttpHandler, IRequiresSessionState
    {
        private string _failedResponse = "NULL";
        private AccountManager _accountMgr = new AccountManager();
        private BookManager _bookMgr = new BookManager();
        private OrderManager _orderMgr = new OrderManager();
        private PaymentManager _paymentMgr = new PaymentManager();

        public void ProcessRequest(HttpContext context)
        {
            if (string.Compare("GET", context.Request.HttpMethod, true) == 0)
            {
                var currentUser = this._accountMgr.GetCurrentUser();
                if (currentUser == null)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(_failedResponse);
                    return;
                }

                Guid userID = currentUser.UserID;
                var orderBookList = this._orderMgr.GetOnlyOneUnfinishOrderItsOrderBookList(userID);
                string orderBookAmount = orderBookList.Count().ToString();

                context.Response.ContentType = "text/plain";
                context.Response.Write(orderBookAmount);
                return;
            }

            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 && string.Compare("CREATE", context.Request.QueryString["Action"], true) == 0)
            {
                string bookIDStr = context.Request.Form["bookID"];
                bool isValidBookID = Guid.TryParse(bookIDStr, out Guid bookID);

                if (!isValidBookID)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(_failedResponse);
                    return;
                }

                var currentUser = this._accountMgr.GetCurrentUser();
                if (currentUser == null)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(_failedResponse);
                    return;
                }

                Guid userID = currentUser.UserID;
                var payment = this._paymentMgr.GetPayment();
                Guid paymentID = payment.PaymentID;

                string orderBookAmount = "";
                var hasOrder = this._orderMgr.GetOnlyOneUnfinishOrder(userID);

                if (hasOrder == null)
                {
                    this._orderMgr.CreateOrder(userID, paymentID);
                    var order = this._orderMgr.GetOnlyOneUnfinishOrder(userID);
                    this._orderMgr.CreateOrderBook(order.OrderID, bookID);
                    var orderBookList = this._orderMgr.GetOnlyOneUnfinishOrderItsOrderBookList(userID);
                    orderBookAmount = orderBookList.Count().ToString();
                }
                else
                {
                    var orderBook = this._orderMgr.GetOnlyOneUnfinishOrderItsOrderBookList(userID);
                    bool isCurrentBookInOrder = orderBook.Where(item => item.BookID == bookID).Any();

                    if (isCurrentBookInOrder)
                    {
                        context.Response.ContentType = "text/plain";
                        context.Response.Write(_failedResponse);
                        return;
                    }

                    this._orderMgr.CreateOrderBook(hasOrder.OrderID, bookID);
                    var orderBookList = this._orderMgr.GetOnlyOneUnfinishOrderItsOrderBookList(userID);
                    orderBookAmount = orderBookList.Count().ToString();
                }

                context.Response.ContentType = "text/plain";
                context.Response.Write(orderBookAmount);
                return;
            }

            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 && string.Compare("DELETE", context.Request.QueryString["Action"], true) == 0)
            {
                string checkedBookID = context.Request.Form["checkedBookID"];
                if (string.IsNullOrEmpty(checkedBookID))
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(_failedResponse);
                    return;
                }

                string[] checkedBookIDStrArr = checkedBookID.Split(',');
                Guid[] checkedBookIDGuidArr = checkedBookIDStrArr.Select(item => Guid.Parse(item)).ToArray();

                var currentUser = this._accountMgr.GetCurrentUser();
                if (currentUser == null)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(_failedResponse);
                    return;
                }

                Guid userID = currentUser.UserID;
                var finishingOrder = this._orderMgr.GetOnlyOneUnfinishOrder(userID);
                var orderBookList = this._orderMgr.BatchDeleteOrderBook(finishingOrder.OrderID, checkedBookIDGuidArr);
                var bookList = this._bookMgr.GetBookList();
                var filteredBookList = orderBookList
                        .Select(orderBook => bookList
                        .Where(book => book.BookID == orderBook.BookID)
                        .FirstOrDefault())
                        .ToList();
                var resultBookList = this._bookMgr.BuildBookModelList(filteredBookList);
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(resultBookList);

                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
                return;
            }

            if (string.Compare("POST", context.Request.HttpMethod, true) == 0 && string.Compare("UPDATE", context.Request.QueryString["Action"], true) == 0)
            {
                string selectedPaymentID = context.Request.Form["selectedPaymentID"];
                bool isValidPaymentID = Guid.TryParse(selectedPaymentID, out Guid paymentID);
                string orderStatusStr = context.Request.Form["orderStatus"];
                bool isValidOrderStatus = int.TryParse(orderStatusStr, out int orderStatus);

                if (!isValidOrderStatus || !isValidPaymentID)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(_failedResponse);
                    return;
                }

                var currentUser = this._accountMgr.GetCurrentUser();
                if (currentUser == null)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(_failedResponse);
                    return;
                }

                Guid userID = currentUser.UserID;
                var orderList = this._orderMgr.GetOnlyOneUnfinishOrderItsOrderBookList(userID);
                var bookList = this._bookMgr.GetBookList();
                var filteredBookList = orderList
                        .Select(order => bookList
                        .Where(book => book.BookID == order.BookID)
                        .FirstOrDefault())
                        .ToList();
                var resultBookList = this._bookMgr.BuildBookModelList(filteredBookList);
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(resultBookList);

                this._orderMgr.UpdateOrder(userID, paymentID, orderStatus);

                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
                return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}