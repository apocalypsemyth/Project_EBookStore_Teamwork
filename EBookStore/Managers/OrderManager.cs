using EBookStore.EBookStore.ORM;
using EBookStore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBookStore.Managers
{
    public class OrderManager
    {
        /** Order Part */
        public Order GetOnlyOneUnfinishOrder(Guid userID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var findUnfinishOrder = contextModel.Orders
                        .Where(item => item.UserID == userID && item.OrderStatus == 0);

                    int isOnlyOneUnfinishOrder = findUnfinishOrder.Count();
                    if (isOnlyOneUnfinishOrder != 1)
                        return null;

                    var unfinishOrder = findUnfinishOrder.FirstOrDefault();
                    return unfinishOrder;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("OrderManager.GetOnlyOneUnfinishOrder", ex);
                throw;
            }
        }

        public void CreateOrder(Guid userID, Guid paymentID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    Order newOrder = new Order()
                    {
                        OrderID = Guid.NewGuid(),
                        UserID = userID,
                        PaymentID = paymentID,
                        OrderDate = DateTime.Now,
                        OrderStatus = 0,
                    };

                    contextModel.Orders.Add(newOrder);
                    contextModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("OrderManager.CreateOrder", ex);
                throw;
            }
        }

        public void UpdateOrder(Guid userID, Guid paymentID, int orderStatus)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var unfinishOrder = this.GetOnlyOneUnfinishOrder(userID);

                    unfinishOrder.PaymentID = paymentID;
                    unfinishOrder.OrderStatus = orderStatus;

                    contextModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("OrderManager.UpdateOrder", ex);
                throw;
            }
        }


        /** OrderBook Part */
        public List<OrderBook> GetOnlyOneUnfinishOrderItsOrderBookList(Guid userID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var unfinishOrder = this.GetOnlyOneUnfinishOrder(userID);

                    if (unfinishOrder == null)
                        return new List<OrderBook>();

                    var orderBookList = contextModel.OrderBooks
                        .Where(item => item.OrderID == unfinishOrder.OrderID)
                        .ToList();

                    return orderBookList;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("OrderManager.GetOrderBookList", ex);
                throw;
            }
        }

        public void CreateOrderBook(Guid orderID, Guid bookID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    OrderBook newOrderBook = new OrderBook()
                    {
                        OrderBookID = Guid.NewGuid(),
                        OrderID = orderID,
                        BookID = bookID,
                    };

                    contextModel.OrderBooks.Add(newOrderBook);
                    contextModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("OrderManager.CreateOrderBook", ex);
                throw;
            }
        }

        public List<OrderBook> BatchDeleteOrderBook(Guid orderID, Guid[] bookIDArray)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var findToDeleteOrderBookList = bookIDArray
                        .Select(bookID => contextModel.OrderBooks
                        .Where(orderBook => orderBook.OrderID == orderID && orderBook.BookID == bookID)
                        .FirstOrDefault());

                    contextModel.OrderBooks.RemoveRange(findToDeleteOrderBookList);
                    contextModel.SaveChanges();

                    var remainedOrderBookList = contextModel.OrderBooks
                        .Where(orderBook => orderBook.OrderID == orderID)
                        .ToList();

                    return remainedOrderBookList;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("OrderManager.BatchDeleteOrderBook", ex);
                throw;
            }
        }
    }
}