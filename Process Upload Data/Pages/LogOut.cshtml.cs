using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProcessUploadData.Services;

namespace Process_Upload_Data.Pages
{
    public class LogOutModel : PageModel
    {
        private readonly IFileRepository fileRepo;
        public LogOutModel(IFileRepository fileRepo)
        {
            this.fileRepo = fileRepo;
        }
        public IActionResult OnGet()
        {
            //fileRepo.UpdateUser(currUser, 1);
            Response.Cookies.Delete("User");
            return RedirectToPage("Index");
        }
         
    }
}
