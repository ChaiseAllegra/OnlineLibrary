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
    public class AccountModel : PageModel
    {
        private IFileRepository fileRepo;
        public List<AccountInfo> UserBooks { get; set; }
        public AccountModel(IFileRepository fileRepo)
        {
            this.fileRepo = fileRepo;
        }
        public IActionResult OnGet()
        {
            var userName = Request.Cookies["User"];
            if (!String.IsNullOrEmpty(userName))
            {
                UserBooks = fileRepo.GetAllUserBooks(userName);
                return this.Page();
            }
            return RedirectToPage("Index");
        }
    }
}
