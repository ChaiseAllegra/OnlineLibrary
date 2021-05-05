using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProcessUploadData.Services;

namespace Process_Upload_Data.Pages
{
    public class AddNewBookModel : PageModel
    {
        private IFileRepository fileRepo;
        public List<Book> BookList { get; set; }
        [BindProperty]
        public string BookName { get; set; }
        [BindProperty]
        public string BookDesc { get; set; }
      
        [BindProperty]
        public string Count { get; set; }
        [BindProperty]
        public string Genre { get; set; }
        [BindProperty]
        public string Author { get; set; }
        public AddNewBookModel(IFileRepository fileRepo)
        {
            this.fileRepo = fileRepo;
        }
        public IActionResult OnGet()
        {
            var cookieValueFromReq = Request.Cookies["User"];
            if (!String.IsNullOrEmpty(cookieValueFromReq)) return this.Page();
            return RedirectToPage("Index");
        }
        public IActionResult OnPost()//Check In book
        {
            Book currBook = new Book();
            int tmp = 0;
            if (!string.IsNullOrEmpty(BookName)) currBook.bookName = BookName;
            else return RedirectToPage("Error");
            if (!string.IsNullOrEmpty(BookDesc)) currBook.bookDesc = BookDesc;
            else return RedirectToPage("Error");
            if (!string.IsNullOrEmpty(Author)) currBook.author = Author;
            else return RedirectToPage("Error");
            if (!string.IsNullOrEmpty(Genre)) currBook.genre = Genre;
            else return RedirectToPage("Error");
            if (!string.IsNullOrEmpty(Count)&& int.TryParse(Count, out tmp))   currBook.count = tmp;
            else return RedirectToPage("Error");
           
            if(fileRepo.CheckInBook(currBook,tmp)) return RedirectToPage("DisplayBooks");
            else if (fileRepo.AddBook(currBook)) return RedirectToPage("DisplayBooks"); 
            return RedirectToPage("Error");
            
        }
    }
}
