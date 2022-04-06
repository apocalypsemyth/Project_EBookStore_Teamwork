using EBookStore.EBookStore.ORM;
using EBookStore.Helpers;
using EBookStore.Managers;
using EBookStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore.BackAdmin
{
    public partial class BookDetail : System.Web.UI.Page
    {
        private bool _isEditMode = false;
        private BookContentManager _mgr = new BookContentManager();
        private AccountManager _Amgr = new AccountManager();
        private BookManager _bmgr = new BookManager();

        // 運用 QueryString 來判斷，新增(沒有ID值) or 編輯(有ID值)
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this._Amgr.IsLogined())
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!this.IsPostBack)
            {
                MemberAccount userid = this._Amgr.GetCurrentUser();
                this.ltlUserID.Text = userid.UserID.ToString();
            }
            // 判斷是要 編輯 或 新增
            if (!string.IsNullOrWhiteSpace(this.Request.QueryString["ID"])) // QueryString不是空值 --> 有ID值
                _isEditMode = true;        // 要 編輯
            else
                _isEditMode = false;       // 要 新增

            if (_isEditMode)   // 編輯
            {
                string idText = this.Request.QueryString["ID"];
                Guid id;
                if (!Guid.TryParse(idText, out id))   // 將 ID值 轉型成 Guid
                    this.ltlErrorMsg.Text = "BookID 錯誤";     // 轉型失敗
                else                                 // 轉型成功
                    this.InitEditMode(id);    // 載入編輯用畫面
            }
            else     // 新增
            {
                this.InitCreateMode();        // 載入新增用畫面
            }
        }

        // 載入新增用畫面(新增頁面初始化)
        private void InitCreateMode()
        {
            this.ltlUserID.Visible = false;
            this.ltlBookID.Visible = false;

            this.plcCreateImg.Visible = true;
            this.plcCreateBookContent.Visible = true;

            this.btnUpdate.Visible = false;
        }

        // 載入編輯用畫面(編輯頁面初始化)
        private void InitEditMode(Guid id)
        {
            this.ltlUserID.Visible = false;
            this.ltlBookID.Visible = false;

            this.rptImage.Visible = true;

            this.plcCreateImg.Visible = true;
            this.plcCreateBookContent.Visible = true;

            this.btnSave.Visible = false;

            // 編輯書籍前 去資料庫找出 相對應ID 的書籍資訊 並 回傳
            BookContentModel model = this._mgr.GetBook(id);
            List<BookContentModel> list = new List<BookContentModel>();
            list.Add(model);

            if (model == null) // 沒有找到 對應的書籍
            {
                this.ltlErrorMsg.Text = "查無此書籍代碼";
                return;
            }
            else
            {
                this.ltlBookID.Text = Convert.ToString(model.BookID);
                this.ltlCategory.Text = model.CategoryName;
                this.ltlAuthor.Text = model.AuthorName;
                this.ltlBookName.Text = model.BookName;
                this.ltlDescription.Text = model.Description;
                this.ltlPrice.Text = Convert.ToString(model.Price.ToString("#0"));

                this.rptImage.DataSource = list;
                this.rptImage.DataBind();
            }
        }

        // 欄位檢查 (格式、型別檢查、必選填、上傳)
        private bool CheckInput(out List<string> errorMsgList)
        {
            errorMsgList = new List<string>();

            if (string.IsNullOrWhiteSpace(this.ltlBookID.Text))
            {
                if (_isEditMode == true) // 要 編輯
                    errorMsgList.Add("沒有這筆資料。"); // 沒有與此BookID相對應的資料
            }

            //if (string.IsNullOrWhiteSpace(this.txtUserID.Text))
            //    errorMsgList.Add("管理員編號為必填。");

            if (string.IsNullOrWhiteSpace(this.txtCategory.Text))
                errorMsgList.Add("分類為必填。");

            if (string.IsNullOrWhiteSpace(this.txtAuthor.Text))
                errorMsgList.Add("作者為必填。");

            if (string.IsNullOrWhiteSpace(this.txtBookName.Text))
                errorMsgList.Add("書名為必填。");

            if (string.IsNullOrWhiteSpace(this.txtDescription.Text))
                errorMsgList.Add("內容簡介為必填。");

            if (_isEditMode == true) // 要 編輯
            {
                if (this.fuImage.HasFile) // 換圖片
                {
                    // 檢查檔案上傳是否正確
                    System.Web.UI.WebControls.FileUpload fu = this.fuImage;
                    string ImgUploadErrormsg;
                    List<string> msgList;

                    if (!this.ValidFileUpload(fu, out msgList))
                    {
                        ImgUploadErrormsg = string.Join("<br/>", msgList);
                        errorMsgList.Add(ImgUploadErrormsg);
                    }
                }
            }
            else // 要 新增
            {
                // 檢查檔案上傳是否正確
                System.Web.UI.WebControls.FileUpload fu = this.fuImage;
                string ImgUploadErrormsg;
                List<string> msgList;

                if (!this.ValidFileUpload(fu, out msgList))
                {
                    ImgUploadErrormsg = string.Join("<br/>", msgList);
                    errorMsgList.Add(ImgUploadErrormsg);
                }
            }

            if (_isEditMode == true) // 要 編輯
            {
                if (this.fuBookContent.HasFile) // 換書籍內容
                {
                    // 檢查檔案上傳是否正確
                    System.Web.UI.WebControls.FileUpload fu = this.fuBookContent;
                    string BookContentUploadErrormsg;
                    List<string> msgList;

                    if (!this.ValidBookContentUpload(fu, out msgList))
                    {
                        BookContentUploadErrormsg = string.Join("<br/>", msgList);
                        errorMsgList.Add(BookContentUploadErrormsg);
                    }
                }
            }
            else // 要 新增
            {
                // 檢查檔案上傳是否正確
                System.Web.UI.WebControls.FileUpload fu = this.fuBookContent;
                string BookContentUploadErrormsg;
                List<string> msgList;

                if (!this.ValidBookContentUpload(fu, out msgList))
                {
                    BookContentUploadErrormsg = string.Join("<br/>", msgList);
                    errorMsgList.Add(BookContentUploadErrormsg);
                }
            }

            if (string.IsNullOrWhiteSpace(this.txtPrice.Text))
                errorMsgList.Add("價格為必填。");

            decimal price;
            if (!decimal.TryParse(this.txtPrice.Text.Trim(), out price)) // 轉型失敗
                errorMsgList.Add("價格的格式錯誤。");

            if (errorMsgList.Count > 0)
            {
                errorMsgList.Add("請回上一頁重新 新增 或 編輯。");
                return false;
            }
            else
                return true;
        }

        private bool CheckEditInput(out List<string> errorMsgList)
        {
            errorMsgList = new List<string>();

            if (string.IsNullOrWhiteSpace(this.ltlBookID.Text))
            {
                if (_isEditMode == true) // 要 編輯
                    errorMsgList.Add("沒有這筆資料。"); // 沒有與此BookID相對應的資料
            }

            if (this.fuImage.HasFile) // 換圖片
            {
                // 檢查檔案上傳是否正確
                System.Web.UI.WebControls.FileUpload fu = this.fuImage;
                string ImgUploadErrormsg;
                List<string> msgList;

                if (!this.ValidFileUpload(fu, out msgList))
                {
                    ImgUploadErrormsg = string.Join("<br/>", msgList);
                    errorMsgList.Add(ImgUploadErrormsg);
                }
            }

            if (this.fuBookContent.HasFile) // 換書籍內容
            {
                // 檢查檔案上傳是否正確
                System.Web.UI.WebControls.FileUpload fu = this.fuBookContent;
                string BookContentUploadErrormsg;
                List<string> msgList;

                if (!this.ValidBookContentUpload(fu, out msgList))
                {
                    BookContentUploadErrormsg = string.Join("<br/>", msgList);
                    errorMsgList.Add(BookContentUploadErrormsg);
                }
            }

            //if (string.IsNullOrWhiteSpace(this.txtPrice.Text))
            //    errorMsgList.Add("價格為必填。");

            decimal price;
            if (!decimal.TryParse(this.txtPrice.Text.Trim(), out price)) // 轉型失敗
                errorMsgList.Add("價格的格式錯誤。");

            if (errorMsgList.Count > 0)
            {
                errorMsgList.Add("請回上一頁重新 新增 或 編輯。");
                return false;
            }
            else
                return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> errorMsgList = new List<string>();
            if (!this.CheckInput(out errorMsgList))
            {
                this.ltlErrorMsg.Text = string.Join("<br/>", errorMsgList);
                return; // 要修改
            }

            BookContentModel model = new BookContentModel()
            {
                UserID = Guid.Parse(this.ltlUserID.Text),

                CategoryName = getcategory(),
                AuthorName = getauthor(),
                BookName = getbookname(),
                Description = getdescription(),
                Price = getprice(),
                IsEnable = true,
                Date = DateTime.Now,
                EndDate = DateTime.Now.AddDays(+365)
            };

            if (_isEditMode == true) // 要 編輯
            {
                model.BookID = Guid.Parse(this.ltlBookID.Text);
            }

            if (_isEditMode == true) // 要 編輯
            {
                if (this.fuImage.HasFile)  // 儲存檔案，並將路徑寫至 model ，以供保存
                {
                    System.Threading.Thread.Sleep(3);
                    Random random = new Random((int)DateTime.Now.Ticks);
                    string folderPath = "/FileDownload/Book";
                    string fileName =
                        DateTime.Now.ToString("yyyyMMdd_HHmmss_FFFFFF") +
                        "_" + random.Next(10000).ToString("0000") +
                        "_" + Path.GetExtension(this.fuImage.FileName);

                    folderPath = System.Web.Hosting.HostingEnvironment.MapPath(folderPath);

                    if (!Directory.Exists(folderPath))  // 假如資料夾不存在，先建立
                        Directory.CreateDirectory(folderPath);

                    string newFilePath = Path.Combine(folderPath, fileName);
                    this.fuImage.SaveAs(newFilePath);

                    model.Image = "/FileDownload/Book/" + fileName;
                }
                else
                {
                    string idText = this.Request.QueryString["ID"];
                    Guid id = Guid.Parse(idText);   // 將 ID值 轉型成 Guid
                    BookContentModel imgmodel = this._mgr.GetBook(id);
                    model.Image = imgmodel.Image;
                }

                if (this.fuBookContent.HasFile)  // 儲存檔案，並將路徑寫至 model ，以供保存
                {
                    System.Threading.Thread.Sleep(3);
                    Random random = new Random((int)DateTime.Now.Ticks);
                    string folderPath = "/FileDownload/BookContent";
                    string fileName =
                        DateTime.Now.ToString("yyyyMMdd_HHmmss_FFFFFF") +
                        "_" + random.Next(10000).ToString("0000") +
                        "_" + Path.GetExtension(this.fuBookContent.FileName);

                    folderPath = System.Web.Hosting.HostingEnvironment.MapPath(folderPath);

                    if (!Directory.Exists(folderPath))  // 假如資料夾不存在，先建立
                        Directory.CreateDirectory(folderPath);

                    string newFilePath = Path.Combine(folderPath, fileName);
                    this.fuBookContent.SaveAs(newFilePath);

                    model.BookContent = "/FileDownload/BookContent/" + fileName;
                }
                else
                {
                    string idText = this.Request.QueryString["ID"];
                    Guid id = Guid.Parse(idText);   // 將 ID值 轉型成 Guid
                    BookContentModel BookContentmodel = this._mgr.GetBook(id);
                    model.BookContent = BookContentmodel.BookContent;
                }

            }
            else // 要 新增
            {
                if (this.fuImage.HasFile)  // 儲存檔案，並將路徑寫至 model ，以供保存
                {
                    System.Threading.Thread.Sleep(3);
                    Random random = new Random((int)DateTime.Now.Ticks);
                    string folderPath = "/FileDownload/Book";
                    string fileName =
                        DateTime.Now.ToString("yyyyMMdd_HHmmss_FFFFFF") +
                        "_" + random.Next(10000).ToString("0000") +
                        "_" + Path.GetExtension(this.fuImage.FileName);

                    folderPath = System.Web.Hosting.HostingEnvironment.MapPath(folderPath);

                    if (!Directory.Exists(folderPath))  // 假如資料夾不存在，先建立
                        Directory.CreateDirectory(folderPath);

                    string newFilePath = Path.Combine(folderPath, fileName);
                    this.fuImage.SaveAs(newFilePath);

                    model.Image = "/FileDownload/Book/" + fileName;
                }

                if (this.fuBookContent.HasFile)  // 儲存檔案，並將路徑寫至 model ，以供保存
                {
                    System.Threading.Thread.Sleep(3);
                    Random random = new Random((int)DateTime.Now.Ticks);
                    string folderPath = "/FileDownload/BookContent";
                    string fileName =
                        DateTime.Now.ToString("yyyyMMdd_HHmmss_FFFFFF") +
                        "_" + random.Next(10000).ToString("0000") +
                        "_" + Path.GetExtension(this.fuBookContent.FileName);

                    folderPath = System.Web.Hosting.HostingEnvironment.MapPath(folderPath);

                    if (!Directory.Exists(folderPath))  // 假如資料夾不存在，先建立
                        Directory.CreateDirectory(folderPath);

                    string newFilePath = Path.Combine(folderPath, fileName);
                    this.fuBookContent.SaveAs(newFilePath);

                    model.BookContent = "/FileDownload/BookContent/" + fileName;
                }
            }

            // 儲存
            if (_isEditMode == true) // 要 編輯
            {
                this._mgr.UpdateBookContent(model);
                string idText = this.Request.QueryString["ID"];
                Guid id = Guid.Parse(idText);   // 將 ID值 轉型成 Guid
                InitEditMode(id);
            }
            else // 要 新增
            {
                this._mgr.CreateBookContent(model);
                Response.Redirect("BookList.aspx");
            }
        }

        //取消 新增 or 編輯，跳回 清單頁面 BookList.aspx
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookList.aspx");
        }

        // 檢查檔案上傳是否正確 (容量 / 副檔名)
        private bool ValidFileUpload(System.Web.UI.WebControls.FileUpload fileUpload, out List<string> errorMsgList)
        {
            List<string> msgList = new List<string>();

            // 檢查是否有上傳檔案
            if (!fileUpload.HasFile)
                msgList.Add("需上傳封面圖");

            string fileName = fileUpload.FileName;
            // 檢查檔案副檔名是否符合規範
            if (!FileHelper.ValidImageExtension(fileName))
            {
                string fileExts = string.Join(", ", FileHelper.ImageFileExtArr);
                msgList.Add("檔案格式必須為 " + fileExts + " 檔");
            }

            // 檢查檔案容量是否符合規範
            byte[] fileContent = fileUpload.FileBytes;
            if (!FileHelper.ValidFileLength(fileContent))
            {
                msgList.Add("檔案容量必須在 " + FileHelper.UploadMB + "MB 以內");
            }

            errorMsgList = msgList;
            if (errorMsgList.Count > 0)
                return false;
            else
                return true;
        }

        private bool ValidBookContentUpload(System.Web.UI.WebControls.FileUpload fuBookContent, out List<string> errorMsgList)
        {
            List<string> msgList = new List<string>();

            // 檢查是否有上傳檔案
            if (!fuBookContent.HasFile)
                msgList.Add("需上傳書籍內容");

            string fileName = fuBookContent.FileName;
            // 檢查檔案副檔名是否符合規範
            if (!BookContentFileHelper.ValidBookContentExtension(fileName))
            {
                string fileExts = string.Join(", ", BookContentFileHelper.BookContentFileExtArr);
                msgList.Add("檔案格式必須為 " + fileExts + " 檔");
            }

            // 檢查檔案容量是否符合規範
            byte[] fileContent = fuBookContent.FileBytes;
            if (!BookContentFileHelper.ValidFileLength(fileContent))
            {
                msgList.Add("檔案容量必須在 " + BookContentFileHelper.UploadMB + "MB 以內");
            }

            errorMsgList = msgList;
            if (errorMsgList.Count > 0)
                return false;
            else
                return true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            List<string> errorMsgList = new List<string>();
            if (!this.CheckEditInput(out errorMsgList))
            {
                this.ltlErrorMsg.Text = string.Join("<br/>", errorMsgList);
                return; // 要修改
            }

            string bookid = this.Request.QueryString["ID"];
            string category = getcategory();
            string author = getauthor();
            string bookname = getbookname();
            string description = getdescription();
            string image = saveImage(bookid);
            string bookcontent = saveBookContent(bookid);
            decimal price = getprice();

            this._bmgr.UpdateBook(bookid, category, author, bookname, description, image, bookcontent, price);

            Response.Redirect("BookList.aspx");
        }

        private decimal getprice()
        {
            if (!string.IsNullOrWhiteSpace(this.txtPrice.Text))
            {
                decimal price = Convert.ToDecimal(this.txtPrice.Text.Trim());
                return price;
            }
            else
            {
                decimal price = Convert.ToDecimal(this.ltlPrice.Text.Trim());
                return price;
            }
        }

        private string getauthor()
        {
            if (!string.IsNullOrWhiteSpace(this.txtAuthor.Text))
            {
                string category = this.txtAuthor.Text.Trim();
                return category;
            }
            else
            {
                string category = this.ltlAuthor.Text.Trim();
                return category;
            }
        }

        private string getbookname()
        {
            if (!string.IsNullOrWhiteSpace(this.txtBookName.Text))
            {
                string bookname = this.txtBookName.Text.Trim();
                return bookname;
            }
            else
            {
                string bookname = this.ltlBookName.Text.Trim();
                return bookname;
            }
        }

        private string getcategory()
        {
            if (!string.IsNullOrWhiteSpace(this.txtCategory.Text))
            {
                string category = this.txtCategory.Text.Trim();
                return category;
            }
            else
            {
                string category = this.ltlCategory.Text.Trim();
                return category;
            }
        }

        private string getdescription()
        {
            if (!string.IsNullOrWhiteSpace(this.txtDescription.Text))
            {
                string description = this.txtDescription.Text.Trim();
                return description;
            }
            else
            {
                string description = this.ltlDescription.Text.Trim();
                return description;
            }
        }

        private string saveImage(string bookid)
        {
            if (fuImage.HasFile)
            {
                String fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + fuImage.FileName;
                String savePath = Server.MapPath("/FileDownload/Book/");
                String saveResult = savePath + fileName;
                fuImage.SaveAs(saveResult);

                return "/FileDownload/Book/" + fileName;
            }
            else
            {
                string imageURL = _bmgr.getimageURL(bookid);
                return imageURL;
            }
        }

        private string saveBookContent(string bookid)
        {
            if (fuBookContent.HasFile)
            {
                String fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + fuBookContent.FileName;
                String savePath = Server.MapPath("/FileDownload/BookContent/");
                String saveResult = savePath + fileName;
                fuBookContent.SaveAs(saveResult);

                return "/FileDownload/BookContent/" + fileName;
            }
            else
            {
                string bookcontentURL = _bmgr.getbookcontentURL(bookid);
                return bookcontentURL;
            }
        }
    }
}