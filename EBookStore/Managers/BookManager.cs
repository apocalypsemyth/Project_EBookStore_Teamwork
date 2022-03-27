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

        public void CreateBook(Book model, Guid userID)
        {
            try
            {
                using (ContextModel contextModel = new ContextModel())
                {
                    model.BookID = Guid.NewGuid();
                    model.UserID = userID;
                    model.Date = DateTime.Now;

                    contextModel.Books.Add(model);
                    contextModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("BookManager.CreateBook", ex);
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
    }
}