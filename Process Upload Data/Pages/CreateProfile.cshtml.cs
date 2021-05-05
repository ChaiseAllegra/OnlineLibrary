using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProcessUploadData.Services;

namespace Process_Upload_Data.Pages
{
    public class CreateProfileModel : PageModel
    {
        private readonly IFileRepository fileRepo;
        [BindProperty]
        public string USERNAME { get; set; }
        [BindProperty]
        public string PASSWORD { get; set; }
        public CreateProfileModel(IFileRepository fileRepo)
        {
            this.fileRepo = fileRepo;
        }
        public IActionResult OnPost(User newUser)
        {
            User currUser = new User();
            if (newUser.userName != null)
                currUser.userName = newUser.userName;
            if (newUser.password != null || newUser.password != "")
                currUser.password = newUser.password;
            if (fileRepo.UpdateUser(currUser, 1))
            {
                Set("User", currUser.userName, 10);
                return RedirectToPage("DisplayBooks");
            }
            else if (fileRepo.AddUser(currUser))
            {
                Set("User", currUser.userName, 10);
                return RedirectToPage("DisplayBooks");
            }
                return RedirectToPage("Error");
        }
        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append(key, value, option);
        }
    }
}
