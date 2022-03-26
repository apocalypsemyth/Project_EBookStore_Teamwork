using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBookStore.Models
{
    public class BookContentModel
    {
        public Guid BookID { get; set; }
        public Guid UserID { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public string BookName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public bool IsEnable { get; set; }       //  新建立
        public DateTime Date { get; set; }
        public DateTime? EndDate { get; set; }   // 修改成 nullable

        #region 管理用欄位
        public DateTime CreateDate { get; set; }
        public Guid CreateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateUser { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Guid? DeleteUser { get; set; }
        #endregion

        public string Content
        {
            get { return this.Description; }
            set { this.Description = value; }
        }
    }
}