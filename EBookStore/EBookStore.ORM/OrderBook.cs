namespace EBookStore.EBookStore.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderBook
    {
        public Guid OrderBookID { get; set; }

        public Guid OrderID { get; set; }

        public Guid BookID { get; set; }

        public virtual Book Book { get; set; }

        public virtual Order Order { get; set; }
    }
}
