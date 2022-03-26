using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBookStore.Models
{
    public class MemberAccount
    {
        public Guid UserID { get; set; }

        /// <summary> 帳號 </summary>
        public string Account { get; set; }

        /// <summary> 密碼 </summary>
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool isEnable { get; set; }
        public int UserLevel { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}