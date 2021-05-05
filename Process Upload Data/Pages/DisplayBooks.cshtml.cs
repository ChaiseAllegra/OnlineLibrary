using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using ProcessUploadData.Services;
using RestSharp;
using Library.Models;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace Process_Upload_Data.Pages
{
    public class DisplayFilterDataModel : PageModel
    {
        private IFileRepository fileRepo;
        public List<Book> BookList { get; set; }
        [BindProperty(SupportsGet = true)]
        public string BookNameSearch { get; set; }
        [BindProperty(SupportsGet = true)]
        public string BookDescSearch { get; set; }
        [BindProperty(SupportsGet = true)]
        public string AuthorSearch { get; set; }
        [BindProperty(SupportsGet = true)]
        public string GenreSearch { get; set; }
        [BindProperty]
        public string BookName { get; set; }
        [BindProperty]
        public string Count { get; set; }
        Book book { get; set; }
        public DisplayFilterDataModel(IFileRepository fileRepo)
        {
            this.fileRepo = fileRepo;
            book = new Book();
        }
        public IActionResult OnGet()
        {
            var cookieValueFromReq = Request.Cookies["User"];
            if (!String.IsNullOrEmpty(cookieValueFromReq))
            {
                if (isInput())
                {
                    book.instantiateBookForSearch(BookNameSearch, BookDescSearch, AuthorSearch, GenreSearch);
                    BookList = fileRepo.SearchBook(book);
                }
                else BookList = fileRepo.GetAllBooks();
                return this.Page();
            }
            return RedirectToPage("Index");
        }
        public IActionResult OnPost()//Check out book
        {
            Book currBook = new Book();
            int checkOutCount = 0;
            if (!string.IsNullOrEmpty(Count) && int.TryParse(Count, out checkOutCount)) currBook.count = checkOutCount;//Handle if the user enters anything that is not an int
            else return RedirectToPage("Error");
            if (!String.IsNullOrEmpty(BookName)) currBook.bookName = BookName;
            else return RedirectToPage("Error");
            if (fileRepo.CheckOutBook(currBook, checkOutCount))
            {
                string userName = Request.Cookies["User"];
                if (fileRepo.CheckInCheckOutTable(userName, currBook)) return RedirectToPage("DisplayBooks");
                else if (fileRepo.InsertCheckOutTable(userName, currBook)) return RedirectToPage("DisplayBooks");
            }
            return RedirectToPage("Error", new { message = "Custom Message" });
        }
        public bool isInput()
        {
            return !String.IsNullOrEmpty(BookNameSearch) || !String.IsNullOrEmpty(BookDescSearch) || !String.IsNullOrEmpty(AuthorSearch) || !String.IsNullOrEmpty(GenreSearch);
        }
    }
}
