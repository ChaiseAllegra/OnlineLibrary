using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class AccountInfo
    {
        public AccountInfo() { }
        public AccountInfo(string name,string BookTitle,int count)
        {
            this.name = name;
            this.BookTitle = BookTitle;
            this.count = count;
        }
        public string name { get; set; }
        public string BookTitle { get; set; }
        public int count { get; set; }
    }
}
