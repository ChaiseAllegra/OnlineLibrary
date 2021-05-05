using System.Collections.Generic;
using Library.Models;
namespace ProcessUploadData.Services
{
    public interface IFileRepository
    {
        List<Book> GetAllBooks();
        bool AddBook(Book newBook);
        List<Book> SearchBook(Book book);
        bool CheckOutBook(Book currBook, int newCount);
        bool CheckInBook(Book currBook, int newCount);
        bool InsertCheckOutTable(string userName,Book currBook);
        bool CheckInCheckOutTable(string userName, Book currBook);
        bool CheckOutCheckOutTable(string userName, Book currBook);
        bool DeleteEmptyCheckOutTable();
        List<AccountInfo> GetAllUserBooks(string userName);
        bool AddUser(User newUser);
        bool UpdateUser(User currUser,int status);
    }

    public class UploadForm
    {
    }
}
