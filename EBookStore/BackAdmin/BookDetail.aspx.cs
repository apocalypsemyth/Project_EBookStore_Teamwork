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

        // 運用 QueryString 來判斷，新增(沒有ID值) or 編輯(有ID值)
        protected void Page_Load(object sender, EventArgs e)
        {
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
            this.ltlBookID.Text = "系統自動新增";
            this.plcCreateImg.Visible = true;
            this.rptImage.Visible = false;
            this.btnImgChange.Visible = false;
            this.plcEditImg.Visible = false;
            this.btnEditImgCancel.Visible = false;

            this.ltlUserID.Visible = false;
            this.ltlCategory.Visible = false;
            this.ltlAuthor.Visible = false;
            this.ltlBookName.Visible = false;
            this.ltlDescription.Visible = false;
            this.ltlPrice.Visible = false;
            this.ltlDate.Visible = false;
            this.ltlEndDate.Visible = false;
            this.ltlIsEnable.Visible = false;
        }

        // 載入編輯用畫面(編輯頁面初始化)
        private void InitEditMode(Guid id)
        {
            this.plcCreateImg.Visible = false;
            this.rptImage.Visible = true;
            this.btnImgChange.Visible = true;
            this.plcEditImg.Visible = false;
            this.btnEditImgCancel.Visible = false;

            // 編輯書籍前 去資料庫找出 相對應ID 的書籍資訊 並 回傳
            BookContentModel model = this._mgr.GetBook(id);
            List<BookContentModel> list = new List<BookContentModel>();
            list.Add(model);

            if (model == null) // 沒有找到 對應的書籍
            {
                this.ltlErrorMsg.Text = "查無此書籍代碼";
                return;
            }

            this.ltlBookID.Text = Convert.ToString(model.BookID); // 顯示書籍代碼(不允許更改書籍代碼)
            this.ltlUserID.Text = Convert.ToString(model.UserID);
            this.ltlCategory.Text = model.CategoryName;
            this.ltlAuthor.Text = model.AuthorName;
            this.ltlBookName.Text = model.BookName;
            this.ltlDescription.Text = model.Description;
            this.ltlPrice.Text = Convert.ToString(model.Price) + "元";
            this.ltlDate.Text = Convert.ToString(model.Date);
            this.ltlEndDate.Text = Convert.ToString(model.EndDate);

            this.rptImage.DataSource = list;
            this.rptImage.DataBind();

            if (model.IsEnable == true)
                this.ltlIsEnable.Text = "True";
            if (model.IsEnable == false)
                this.ltlIsEnable.Text = "False";
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

            if (string.IsNullOrWhiteSpace(this.txtUserID.Text))
                errorMsgList.Add("管理員編號為必填。");

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
                if (this.fuEditImage.HasFile) // 換圖片
                {
                    // 檢查檔案上傳是否正確
                    System.Web.UI.WebControls.FileUpload fu = this.fuEditImage;
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

            if (string.IsNullOrWhiteSpace(this.txtPrice.Text))
                errorMsgList.Add("價格為必填。");

            if (string.IsNullOrWhiteSpace(this.rbtnList.SelectedValue))
                errorMsgList.Add("商品是否上架為必選。");

            if (string.IsNullOrWhiteSpace(this.txtDate.Text))
                errorMsgList.Add("上架日期為必填。");

            if (string.IsNullOrWhiteSpace(this.txtEndDate.Text))
                errorMsgList.Add("下架日期為必填。");


            Guid userid;
            if (!Guid.TryParse(this.txtUserID.Text.Trim(), out userid)) // 轉型失敗
                errorMsgList.Add("管理員編號的格式錯誤。");
            List<MemberAccount> compareuserid = this._mgr.GetUserID(userid);
            if (compareuserid.Count == 0)
                errorMsgList.Add("管理員編號不存在。");

            decimal price;
            if (!decimal.TryParse(this.txtPrice.Text.Trim(), out price)) // 轉型失敗
                errorMsgList.Add("價格的格式錯誤。");

            DateTime date;
            if (!DateTime.TryParse(this.txtDate.Text.Trim(), out date)) // 轉型失敗
                errorMsgList.Add("上架日期的格式錯誤。");

            DateTime endDate;
            if (!DateTime.TryParse(this.txtEndDate.Text.Trim(), out endDate)) // 轉型失敗
                errorMsgList.Add("下架日期的格式錯誤。");

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
                UserID = Guid.Parse(this.txtUserID.Text.Trim()),

                CategoryName = this.txtCategory.Text.Trim(),
                AuthorName = this.txtAuthor.Text.Trim(),
                BookName = this.txtBookName.Text.Trim(),
                Description = this.txtDescription.Text.Trim(),
                Price = Convert.ToDecimal(this.txtPrice.Text.Trim()),
                Date = Convert.ToDateTime(this.txtDate.Text.Trim()),
                EndDate = Convert.ToDateTime(this.txtEndDate.Text.Trim()),

            };

            if (_isEditMode == true) // 要 編輯
            {
                model.BookID = Guid.Parse(this.ltlBookID.Text.Trim());
            }

            if (_isEditMode == true) // 要 編輯
            {
                if (this.fuEditImage.HasFile)  // 儲存檔案，並將路徑寫至 model ，以供保存
                {
                    System.Threading.Thread.Sleep(3);
                    Random random = new Random((int)DateTime.Now.Ticks);
                    string folderPath = "~/FileDownload/Book";
                    string fileName =
                        DateTime.Now.ToString("yyyyMMdd_HHmmss_FFFFFF") +
                        "_" + random.Next(10000).ToString("0000") +
                        "_" + Path.GetExtension(this.fuEditImage.FileName);

                    folderPath = System.Web.Hosting.HostingEnvironment.MapPath(folderPath);

                    if (!Directory.Exists(folderPath))  // 假如資料夾不存在，先建立
                        Directory.CreateDirectory(folderPath);

                    string newFilePath = Path.Combine(folderPath, fileName);
                    this.fuEditImage.SaveAs(newFilePath);

                    model.Image = "~/FileDownload/Book/" + fileName;
                }
                else
                {
                    string idText = this.Request.QueryString["ID"];
                    Guid id = Guid.Parse(idText);   // 將 ID值 轉型成 Guid
                    BookContentModel imgmodel = this._mgr.GetBook(id);
                    model.Image = imgmodel.Image;
                }

            }
            else // 要 新增
            {
                if (this.fuImage.HasFile)  // 儲存檔案，並將路徑寫至 model ，以供保存
                {
                    System.Threading.Thread.Sleep(3);
                    Random random = new Random((int)DateTime.Now.Ticks);
                    string folderPath = "~/FileDownload/Book";
                    string fileName =
                        DateTime.Now.ToString("yyyyMMdd_HHmmss_FFFFFF") +
                        "_" + random.Next(10000).ToString("0000") +
                        "_" + Path.GetExtension(this.fuImage.FileName);

                    folderPath = System.Web.Hosting.HostingEnvironment.MapPath(folderPath);

                    if (!Directory.Exists(folderPath))  // 假如資料夾不存在，先建立
                        Directory.CreateDirectory(folderPath);

                    string newFilePath = Path.Combine(folderPath, fileName);
                    this.fuImage.SaveAs(newFilePath);

                    model.Image = "~/FileDownload/Book/" + fileName;
                }
            }


            if (!string.IsNullOrWhiteSpace(this.rbtnList.SelectedValue))
            {
                if (this.rbtnList.Items.FindByText("True").Selected)
                {
                    model.IsEnable = true;
                }
                else if (this.rbtnList.Items.FindByText("False").Selected)
                {
                    model.IsEnable = false;
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

        protected void btnImgChange_Click(object sender, EventArgs e)
        {
            this.rptImage.Visible = false;
            this.btnImgChange.Visible = false;
            this.plcEditImg.Visible = true;
            this.btnEditImgCancel.Visible = true;
        }

        protected void btnEditImgCancel_Click(object sender, EventArgs e)
        {
            this.rptImage.Visible = true;
            this.btnImgChange.Visible = true;
            this.plcEditImg.Visible = false;
            this.btnEditImgCancel.Visible = false;
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

    }
}