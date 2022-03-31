using EBookStore.Helpers;
using EBookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EBookStore.Managers
{
    public class AccountManager
    {
        public bool GuestLogin(string account, string password)
        {
            bool isAccountRight = false;
            bool isPasswordRight = false;
            bool isQualify = false;

            MemberAccount member = this.GetAccount(account);

            if (member == null) // 找不到就代表登入失敗
                return false;

            if (string.Compare(member.Account, account, true) == 0)
                isAccountRight = true;

            if (member.Password == password)
                isPasswordRight = true;
            if (member.UserLevel == 2)
                isQualify = true;


            // 檢查帳號密碼是否正確
            bool result = (isAccountRight && isPasswordRight && isQualify);

            // 帳密正確：把值寫入 Session
            if (result)
            {
                HttpContext.Current.Session["MemberAccount"] = new MemberAccount()
                {
                    UserID = member.UserID,
                    Account = account,
                    Password = password
                };
            }

            return result;
        }

        public bool TryLogin(string account, string password)
        {
            bool isAccountRight = false;
            bool isPasswordRight = false;
            bool isQualify = false;

            MemberAccount member = this.GetAccount(account);

            if (member == null) // 找不到就代表登入失敗
                return false;

            if (string.Compare(member.Account, account, true) == 0)
                isAccountRight = true;

            if (member.Password == password)
                isPasswordRight = true;
            if (member.UserLevel == 1)
                isQualify = true;


            // 檢查帳號密碼是否正確
            bool result = (isAccountRight && isPasswordRight && isQualify);

            // 帳密正確：把值寫入 Session
            if (result)
            {
                HttpContext.Current.Session["MemberAccount"] = new MemberAccount()
                {
                    Account = account,
                    Password = password,
                    UserID = member.UserID
                };
            }

            return result;
        }

        public bool IsLogined()
        {
            MemberAccount account = GetCurrentUser();
            return (account != null);
        }

        public MemberAccount GetCurrentUser()
        {
            MemberAccount account = HttpContext.Current.Session["MemberAccount"] as MemberAccount;

            return account;
        }        

        public void Logout()
        {
            HttpContext.Current.Session.Remove("MemberAccount");
        }

        #region "增刪修查"
        public List<MemberAccount> GetAccountList(string keyword)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Users ";

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                commandText += " WHERE Account LIKE '%'+@keyword+'%'";
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                        {
                            command.Parameters.AddWithValue("@keyword", keyword);
                        }

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<MemberAccount> list = new List<MemberAccount>();

                        while (reader.Read())
                        {
                            MemberAccount member = new MemberAccount()
                            {
                                UserID = (Guid)reader["UserID"],
                                Account = reader["Account"] as string,
                                Phone = reader["Phone"] as string,
                                Email = reader["Email"] as string,
                            };

                            list.Add(member);
                        }

                        return list;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MapContentManager.GetMapList", ex);
                throw;
            }
        }

        private MemberAccount BuildAccountModel(SqlDataReader reader)
        {
            MemberAccount model = new MemberAccount()
            {
                UserID = (Guid)reader["UserID"],
                Account = reader["Account"] as string,
                Password = reader["PWD"] as string,
                UserLevel = (int)reader["UserLevel"]
            };
            return model;
        }

        public MemberAccount GetAccount(string account)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM [Users]
                    WHERE [Account] = @account ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@account", account);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            MemberAccount member = new MemberAccount()
                            {
                                UserID = (Guid)reader["UserID"],
                                Account = reader["Account"] as string,
                                Password = reader["PWD"] as string,
                                UserLevel = (int)reader["UserLevel"],
                            };

                            return member;
                        }

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MapContentManager.GetMapList", ex);
                throw;
            }
        }

        public MemberAccount GetAccount(Guid id)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Users
                    WHERE UserID = @UserID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@UserID", id);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            MemberAccount member = new MemberAccount()
                            {
                                UserID = (Guid)reader["UserID"],
                                Account = reader["Account"] as string,
                                Password = reader["PWD"] as string,
                                UserLevel = (int)reader["UserLevel"]
                            };

                            return member;
                        }

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MapContentManager.GetMapList", ex);
                throw;
            }
        }

        public void CreateAccount(MemberAccount member)//out參數,發生時
        {
            // 1. 判斷資料庫是否有相同的 Account
            if (this.GetAccount(member.Account) != null)
                throw new Exception("已存在相同的帳號");

            // 2. 新增資料
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO Users
                        ( UserID,Account, PWD,Email,Phone,isEnable,UserLevel)
                    VALUES
                        ( @UserID,@account, @pwd,@Email,@Phone,@isEnable,@userlevel)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        member.isEnable = true;
                        member.UserID = Guid.NewGuid();
                        command.Parameters.AddWithValue("@UserID", member.UserID);
                        command.Parameters.AddWithValue("@account", member.Account);
                        command.Parameters.AddWithValue("@pwd", member.Password);
                        command.Parameters.AddWithValue("@Email", member.Email);
                        command.Parameters.AddWithValue("@Phone", member.Phone);
                        command.Parameters.AddWithValue("@isEnable", member.isEnable);
                        command.Parameters.AddWithValue("@userlevel", (int)member.UserLevel);

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Create", ex);
                throw;
            }
        }

        public void UpdateAccount(MemberAccount member)
        {
            // 1. 判斷資料庫是否有相同的 Account
            if (this.GetAccount(member.Account) == null)
                throw new Exception("帳號不存在：" + member.Account);

            // 2. 編輯資料
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  UPDATE Users
                    SET 
                        PWD = @pwd,
                        Email=@Email,
                        Phone=@Phone,                                  
                        UserLevel =@userlevel
                    WHERE
                        UserID = @UserID ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@UserID", member.UserID);
                        command.Parameters.AddWithValue("@pwd", member.Password);
                        command.Parameters.AddWithValue("@Email", member.Email);
                        command.Parameters.AddWithValue("@Phone", member.Phone);
                        command.Parameters.AddWithValue("@userlevel", (int)member.UserLevel);
                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MapContentManager.GetMapList", ex);
                throw;
            }
        }

        public void DeleteAccounts(List<Guid> ids)
        {
            // 1. 判斷是否有傳入 id
            if (ids == null || ids.Count == 0)
                throw new Exception("需指定 id");

            List<string> param = new List<string>();
            for (var i = 0; i < ids.Count; i++)
            {
                param.Add("@UserID" + i);
            }
            string inSql = string.Join(", ", param);    // @id1, @id2, @id3, etc...

            // 2. 刪除資料
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" DELETE Accounts
                    WHERE UserID IN ({inSql}) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        for (var i = 0; i < ids.Count; i++)
                        {
                            command.Parameters.AddWithValue("@UserID" + i, ids[i]);
                        }

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MapContentManager.GetMapList", ex);
                throw;
            }
        }
        #endregion
    }
}