using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ProcessUploadData.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Library.Models;

namespace Process_Upload_Data.Pages
{
    public class CheckIn : PageModel
    {
        private IFileRepository fileRepo;
        public List<Book> BookList { get; set; }
        [BindProperty]
        public string BookName { get; set; }
        [BindProperty]
        public string BookDesc { get; set; }
        [BindProperty]
        public string Count { get; set; }
        public CheckIn(IFileRepository fileRepo)
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
            if(!string.IsNullOrEmpty(BookName))currBook.bookName = BookName;          
            else return RedirectToPage("Error");
            if (!string.IsNullOrEmpty(Count)&&int.TryParse(Count, out tmp))currBook.count = tmp;//Handle if the user enters anything that is not an int     
            else return RedirectToPage("Error");
            if (fileRepo.CheckInBook(currBook, currBook.count))
            {
                string userName = Request.Cookies["User"];
                if (fileRepo.CheckOutCheckOutTable(userName, currBook))
                {
                    fileRepo.DeleteEmptyCheckOutTable();
                    return RedirectToPage("DisplayBooks");
                }
            }
            return RedirectToPage("Error");
        }


    }
}
