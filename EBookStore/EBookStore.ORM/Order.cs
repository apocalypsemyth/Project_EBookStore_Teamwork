namespace EBookStore.EBookStore.ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderBooks = new HashSet<OrderBook>();
        }

        public Guid OrderID { get; set; }

        public Guid UserID { get; set; }

        public Guid PaymentID { get; set; }

        public DateTime OrderDate { get; set; }

        public int OrderStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderBook> OrderBooks { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual User User { get; set; }
    }
}
