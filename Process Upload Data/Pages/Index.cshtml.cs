using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProcessUploadData.Services;
using RestSharp;

namespace Process_Upload_Data.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IFileRepository fileRepo;
        [Required]
        [BindProperty]
        public string USERNAME { get; set; }
        [Required]
        [BindProperty]
        public string PASSWORD { get; set; }
        public IndexModel(IFileRepository fileRepo)
        {
            this.fileRepo = fileRepo;
        }
        public IActionResult OnGet()
        {
            var cookieValueFromReq = Request.Cookies["User"];
            if(!String.IsNullOrEmpty(cookieValueFromReq)) return RedirectToPage("DisplayBooks");
            return this.Page();
        }
        public IActionResult OnPost(User newUser)
        {
            User currUser = new User();
            if (ModelState.IsValid)
            {
                if (newUser.userName != null)
                    currUser.userName = newUser.userName;
                if (newUser.password != null || newUser.password != "")
                    currUser.password = newUser.password;
                if (fileRepo.UpdateUser(currUser, 1))
                {
                    Set("User",currUser.userName,10);
                    return RedirectToPage("DisplayBooks");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
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
