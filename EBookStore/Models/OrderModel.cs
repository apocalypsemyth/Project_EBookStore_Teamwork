using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBookStore.Models
{
    public class OrderModel
    {
        public Guid OrderID { get; set; }
        public Guid UserID { get; set; }
        public Guid PaymentID { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }
        public Guid OrderBookID { get; set; }
        public Guid BookID { get; set; }
    }
}