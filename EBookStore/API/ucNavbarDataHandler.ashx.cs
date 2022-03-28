using EBookStore.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace EBookStore.API
{
    /// <summary>
    /// Summary description for ucNavbarDataHandler
    /// </summary>
    public class ucNavbarDataHandler : IHttpHandler, IRequiresSessionState
    {
        private string _failedResponse = "NULL";
        private AccountManager _accountMgr = new AccountManager();

        public void ProcessRequest(HttpContext context)
        {
            var currentUser = this._accountMgr.GetCurrentUser();
            if (currentUser == null)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(_failedResponse);
                return;
            }

            Guid userID = currentUser.UserID;

            context.Response.ContentType = "text/plain";
            context.Response.Write(userID);
            return;
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