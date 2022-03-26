using EBookStore.Managers;
using EBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBookStore.BackAdmin
{
    public partial class BookListSearchIEFalse : System.Web.UI.Page
    {
        private BookContentManager _mgr = new BookContentManager();
        private const int _pageSize = 10;

        // 從資料庫叫出全部的清單資料 OR 輸入書名搜尋關鍵字查詢
        // 將 搜尋關鍵字值 變成 QueryString，再次載入 BookList 頁面，顯示查詢結果
        protected void Page_Load(object sender, EventArgs e)
        {
            string pageIndexText = this.Request.QueryString["Index"];
            int pageIndex =
                (string.IsNullOrWhiteSpace(pageIndexText))
                    ? 1
                    : Convert.ToInt32(pageIndexText);

            if (!this.IsPostBack)
            {
                // 取得keyword的querystring，以及還原至textbox
                string keyword = this.Request.QueryString["keyword"];
                if (!string.IsNullOrWhiteSpace(keyword))
                    this.txtKeyword.Text = keyword;

                // 用 keyword 來判斷，是要 從資料庫叫出全部的清單資料 OR 查詢輸入關鍵字的資料，
                // 並切換 顯示分類資料的按鈕 區塊
                if (string.IsNullOrWhiteSpace(keyword)) // 從資料庫叫出全部的清單資料 // 顯示全部清單資料的上、下架商品的分類按鈕
                {
                    //this.btnIsEnableTrue.Visible = true;
                    //this.btnIsEnableTrue.BorderColor = System.Drawing.Color.AliceBlue;
                    //this.btnIsEnableTrue.BackColor = System.Drawing.Color.AliceBlue;
                    //this.btnIsEnableFalse.Visible = true;
                    //this.btnSearchIETrue.Visible = false;
                    //this.btnSearchIEFalse.Visible = false;
                }
                else // 查詢輸入關鍵字的資料 // 顯示查詢資料的上、下架商品的分類按鈕
                {
                    //this.btnIsEnableTrue.Visible = false;
                    //this.btnIsEnableFalse.Visible = false;
                    this.btnSearchIETrue.Visible = true;
                    this.btnSearchIEFalse.Visible = true;
                    this.btnSearchIEFalse.BorderColor = System.Drawing.Color.AliceBlue;
                    this.btnSearchIEFalse.BackColor = System.Drawing.Color.AliceBlue;
                }

                int totalRows = 0;
                List<BookContentModel> listIEFalse = this._mgr.GetBookListIsEnableFalse(keyword, _pageSize, pageIndex, out totalRows);
                //List<BookContentModel> listIETrue = this._mgr.GetAdminBookListIsEnableTrue(keyword); // 從資料庫叫出IsEnableTrue的全部清單資料

                this.ucPager.TotalRows = totalRows;
                this.ucPager.PageIndex = pageIndex;
                this.ucPager.Bind("keyword", keyword);

                if (listIEFalse.Count > 0) // 有資料
                {
                    this.gvList.DataSource = listIEFalse;  // GridView 做 資料繫結
                    this.gvList.DataBind();         // 顯示於頁面上

                    this.plcEmpty.Visible = false;  // 隱藏 未有資料 的區塊
                    this.gvList.Visible = true;
                }
                else    // 沒有資料
                {
                    this.plcEmpty.Visible = true;   // 顯示 未有資料 的區塊
                    this.gvList.Visible = false;
                }
            }

        }

        // 點激 新增 按鈕，跳至 BookDetail.aspx 內頁，做新增
        // PS.點激 畫面清單資料 中 管理欄位 的 編輯 按鈕，並將 ID 變成 QueryString 帶入 URL，
        // 跳至 BookDetail.aspx?ID=XX 內頁，判斷新增or編輯後，做編輯
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookDetail.aspx");
        }

        // 做成軟性刪除 --> IsEnable值 改為 false
        // 勾選 畫面清單中的checkbox，建立 checkbox被選取 的 (軟性)刪除id清單
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<Guid> idList = new List<Guid>();
            foreach (GridViewRow gRow in this.gvList.Rows)  // 將 畫面清單GridView 的內容放入變數中，一個一個查找
            {
                CheckBox ckbDel = gRow.FindControl("ckbDel") as CheckBox;     // 找到checkbox
                HiddenField hfID = gRow.FindControl("hfID") as HiddenField;   // 找到hiddenfield(裡面放資料ID)

                if (ckbDel != null && hfID != null)    // checkbox有值 且 hiddenfield有ID
                {
                    if (ckbDel.Checked)    // checkbox有被選取
                    {
                        Guid id;
                        if (Guid.TryParse(hfID.Value, out id))
                            idList.Add(id);
                    }

                }
            }
            // 有 被勾選的 要(軟性)刪除的資料  
            if (idList.Count > 0)  // 用 idList 裡的 BookID資料，去資料庫裡抓出要做(軟性)刪除的那幾筆資料後，跑SQL的(修改)刪除指令
            {                                                             // 資料庫裡放的是圖片的存檔路徑
                // 用 idList 裡的 BookID資料，去資料庫裡抓出那些要做刪除的資料的 圖片存檔路徑(圖片真正存放的地方)
                // 去圖片真正存檔的 資料夾 中，做 圖檔的刪除
                //List<BookContentModel> pickedList = this._mgr.GetBookList(idList); // 去資料庫抓圖片的存檔路徑
                //foreach (BookContentModel model in pickedList)   // 從List中，把存檔路徑一個一個跑出來
                //{
                //    this.DeleteImage(model.Image);
                //}

                this._mgr.DeleteBookContent(idList);
                this.Response.Redirect(this.Request.RawUrl); // 刪完後回到清單畫面
            }
        }

        // 去圖片真正存檔的 資料夾 中，做 圖檔的刪除
        private void DeleteImage(string imagePath)
        {
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(imagePath); // 查找完整的圖片存檔路徑

            if (System.IO.File.Exists(filePath))  // 有相對應的 圖片檔
                System.IO.File.Delete(filePath);  // 刪除圖片
        }

        // 輸入書名搜尋關鍵字查詢
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = this.txtKeyword.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))     // 沒輸入搜尋關鍵字就按了查詢按鈕
                Response.Redirect("BookList.aspx");
            else
                // 將 搜尋關鍵字值 變成 QueryString 丟給URL，再次載入 BookList 頁面，顯示查詢結果
                Response.Redirect("BookList.aspx?keyword=" + keyword);
        }

        //// 從資料庫叫出IsEnable True 的全部清單資料，顯示於畫面上
        //protected void btnIsEnableTrue_Click(object sender, EventArgs e)
        //{
        //    string keyword = this.Request.QueryString["keyword"];
        //    Response.Redirect("BookListIsEnable.aspx?keyword=" + keyword);
        //}

        //// 從資料庫叫出IsEnable False 的全部清單資料，顯示於畫面上
        //protected void btnIsEnableFalse_Click(object sender, EventArgs e)
        //{
        //    string keyword = this.Request.QueryString["keyword"];
        //    Response.Redirect("BookListIsEnableFalse.aspx?keyword=" + keyword);
        //}

        // 查詢輸入關鍵字的IsEnable True 的資料，顯示於畫面上
        protected void btnSearchIETrue_Click(object sender, EventArgs e)
        {
            string keyword = this.Request.QueryString["keyword"];
            Response.Redirect("BookListSearchIETrue.aspx?keyword=" + keyword);
        }

        // 查詢輸入關鍵字的IsEnable False 的資料，顯示於畫面上
        protected void btnSearchIEFalse_Click(object sender, EventArgs e)
        {
            string keyword = this.Request.QueryString["keyword"];
            Response.Redirect("BookListSearchIEFalse.aspx?keyword=" + keyword);
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            string keyword = this.Request.QueryString["keyword"];
            Response.Redirect("BookList.aspx?keyword=" + keyword);
        }
    }
}