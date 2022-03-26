using EBookStore.Helpers;
using EBookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EBookStore.Managers
{
    public class UserManager
    {
        public bool TryLogin(string account, string password)
        {
            bool isAccountRight = false;
            bool isPasswordRight = false;
            bool isQualify = false;

            UserModel member = this.GetAccount(account);

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
                HttpContext.Current.Session["AccountModel"] = new UserModel()
                {
                    Account = account,
                    Password = password
                };
            }

            return result;
        }

        public bool TryLogin2(string account, string password)
        {
            bool isAccountRight = false;
            bool isPasswordRight = false;
            bool isQualify = false;

            UserModel member = this.GetAccount(account);

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
                HttpContext.Current.Session["AccountModel"] = new UserModel()
                {
                    Account = account,
                    Password = password
                };
            }

            return result;
        }

        public bool IsLogined()
        {
            UserModel account = GetCurrentUser();
            return (account != null);
        }

        public UserModel GetCurrentUser()
        {
            UserModel account = HttpContext.Current.Session["AccountModel"] as UserModel;

            return account;
        }

        public void Logout()
        {
            HttpContext.Current.Session.Remove("AccountModel");
        }

        #region "增刪修查"
        public List<UserModel> GetAccountList(string keyword)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Accounts ";

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
                        List<UserModel> list = new List<UserModel>();

                        while (reader.Read())
                        {
                            UserModel member = new UserModel()
                            {
                                ID = (Guid)reader["ID"],
                                Account = reader["Account"] as string
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
        private UserModel BuildAccountModel(SqlDataReader reader)
        {
            UserModel model = new UserModel()
            {
                ID = (Guid)reader["ID"],
                Account = reader["Account"] as string,
                Password = reader["PWD"] as string,
                UserLevel = (int)reader["UserLevel"]
            };
            return model;
        }
        public UserModel GetAccount(string account)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM [Accounts]
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
                            UserModel member = new UserModel()
                            {
                                ID = (Guid)reader["ID"],
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

        public UserModel GetAccount(Guid id)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Accounts
                    WHERE ID = @id ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            UserModel member = new UserModel()
                            {
                                ID = (Guid)reader["ID"],
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

        public void CreateAccount(UserModel member)
        {
            // 1. 判斷資料庫是否有相同的 Account
            if (this.GetAccount(member.Account) != null)
                throw new Exception("已存在相同的帳號");

            // 2. 新增資料
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  INSERT INTO Accounts
                        (ID, Account, PWD)
                    VALUES
                        (@id, @account, @pwd,@userlevel)";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        member.ID = Guid.NewGuid();

                        command.Parameters.AddWithValue("@id", member.ID);
                        command.Parameters.AddWithValue("@account", member.Account);
                        command.Parameters.AddWithValue("@pwd", member.Password);
                        command.Parameters.AddWithValue("@userlevel", member.UserLevel);

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

        public void UpdateAccount(UserModel member)
        {
            // 1. 判斷資料庫是否有相同的 Account
            if (this.GetAccount(member.Account) == null)
                throw new Exception("帳號不存在：" + member.Account);

            // 2. 編輯資料
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  UPDATE Accounts
                    SET 
                        PWD = @pwd
                    WHERE
                        ID = @id ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@id", member.ID);
                        command.Parameters.AddWithValue("@pwd", member.Password);

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
                param.Add("@id" + i);
            }
            string inSql = string.Join(", ", param);    // @id1, @id2, @id3, etc...

            // 2. 刪除資料
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" DELETE Accounts
                    WHERE ID IN ({inSql}) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        for (var i = 0; i < ids.Count; i++)
                        {
                            command.Parameters.AddWithValue("@id" + i, ids[i]);
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