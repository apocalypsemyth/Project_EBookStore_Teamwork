using EBookStore.EBookStore.ORM;
using EBookStore.Helpers;
using EBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBookStore.Managers
{
    public class BookManager
    {
        public List<Book> GetBookList()
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var bookList = contextModel.Books
                        .Where(item => item.IsEnable)
                        .ToList();

                    return bookList;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.GetBookList", ex);
                throw;
            }
        }

        public List<Book> GetBookList(string searchText)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var bookList = this.GetBookList();
                    var searchedBookList = bookList
                        .Where(item => item.BookName == searchText)
                        .ToList();

                    return searchedBookList;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.GetBookList", ex);
                throw;
            }
        }

        public Book GetBook(Guid bookID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var book = contextModel.Books
                        .Where(item => item.BookID == bookID)
                        .FirstOrDefault();

                    return book;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.GetBook", ex);
                throw;
            }
        }

        public Book GetBook(string searchText)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var searchedBook = contextModel.Books
                        .Where(item => item.BookName == searchText)
                        .FirstOrDefault();

                    return searchedBook;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.GetBook", ex);
                throw;
            }
        }

        public List<Book> FilterBookListByOrderBookList(List<OrderBook> orderBookList, List<Book> bookList)
        {
            try
            {
                var filteredBookList = orderBookList
                    .Select(orderBook => bookList
                    .Where(book => book.BookID == orderBook.BookID)
                    .FirstOrDefault())
                    .ToList();

                return filteredBookList;
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.FilterBookListByOrderBookList", ex);
                throw;
            }
        }

        public List<MyBookListModel> GetMyBookList(string userID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var query =
                        (from order in contextModel.Orders
                         where order.UserID.ToString() == userID
                            && order.OrderStatus == 1 //已結帳=1
                         join orderbook in contextModel.OrderBooks
                             on order.OrderID equals orderbook.OrderID
                         join book in contextModel.Books
                             on orderbook.BookID equals book.BookID
                         orderby order.OrderDate
                         select new MyBookListModel
                         {
                             CategoryName = book.CategoryName,
                             BookName = book.BookName,
                             AuthorName = book.AuthorName,
                             BookID = book.BookID
                         }).ToList();

                    return query;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.GetMyBookListModel", ex);
                throw;
            }
        }

        public List<Book> GetSearchResult(string keyword)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    // 組合 IQueryable
                    var query =
                        from item in contextModel.Books
                        where item.IsEnable == true
                        where item.BookName.Contains(keyword)
                            || item.AuthorName.Contains(keyword)
                            || item.CategoryName.Contains(keyword)
                        select item;

                    // 執行並取回結果
                    var list = query.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.GetSearchResult", ex);
                throw;
            }
        }

        public string GetBookFileURL(Guid bookID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var query = (from bookfile in contextModel.Books
                                 where bookfile.BookID == bookID
                                 select bookfile.Image).FirstOrDefault().ToString();
                    return query;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.GetBookFileURL", ex);
                throw;
            }
        }

        public BookModel BuildBookModel(Book book)
        {
            try
            {
                return new BookModel()
                {
                    BookID = book.BookID,
                    UserID = book.UserID,
                    CategoryName = book.CategoryName,
                    AuthorName = book.AuthorName,
                    BookName = book.BookName,
                    Description = book.Description,
                    Price = book.Price,
                    Image = book.Image,
                    IsEnable = book.IsEnable,
                    Date = book.Date,
                    EndDate = book.EndDate,
                };
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.BuildBookModel", ex);
                throw;
            }
        }

        public List<BookModel> BuildBookModelList(List<Book> bookList)
        {
            try
            {
                List<BookModel> bookModelList = new List<BookModel>();
                foreach (var book in bookList)
                {
                    BookModel retBook = this.BuildBookModel(book);
                    bookModelList.Add(retBook);
                }

                return bookModelList;
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.BuildBookModelList", ex);
                throw;
            }
        }

        public void UpdateBook(string bookid, string category, string author, string bookname, string description, string image, string bookcontent, decimal price)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    // 組查詢條件
                    var query = from item in contextModel.Books
                                where item.BookID.ToString() == bookid
                                select item;
                    



                    // 取得資料
                    var cate = query.FirstOrDefault();

                    // 檢查是否存在並修改資料
                    if (cate != null)
                    {
                        cate.CategoryName = category;
                        cate.AuthorName = author;
                        cate.BookName = bookname;
                        cate.Description = description;
                        cate.Image = image;
                        cate.BookContent = bookcontent;
                        cate.Price = price;
                    }

                    // 確定存檔
                    contextModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.UpdateBook", ex);
                throw;
            }
        }        

        public string getimageURL(string bookID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var query = (from bookfile in contextModel.Books
                                 where bookfile.BookID.ToString() == bookID
                                 select bookfile.Image).FirstOrDefault().ToString();
                    return query;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.GetBookFileURL", ex);
                throw;
            }
        }

        public string getbookcontentURL(string bookID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    var query = (from bookfile in contextModel.Books
                                 where bookfile.BookID.ToString() == bookID
                                 select bookfile.BookContent).FirstOrDefault().ToString();
                    return query;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.GetBookFileURL", ex);
                throw;
            }
        }
    }
}