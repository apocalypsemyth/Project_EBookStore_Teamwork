using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBookStore.Models
{
    public class MyBookListModel
    {
        public Guid OrderID { get; set; }
        public Guid UserID { get; set; }
        public Guid PaymentID { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }
        public Guid OrderBookID { get; set; }
        public Guid BookID { get; set; }
        public string CategoryName { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public bool IsEnable { get; set; }
        public DateTime Date { get; set; }
        public DateTime? EndDate { get; set; }
    }
}