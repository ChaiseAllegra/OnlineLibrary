using System;

namespace Library.Models
{
    public class Book
    {
        public Book() { }
        public Book(string bookName,string bookDesc, int count,string genre,string author)
        {
            this.bookName = bookName;
            this.bookDesc = bookDesc;
            this.count = count;
            this.genre = genre;
            this.author = author;
        }
        public void instantiateBookForSearch(string bookName, string bookDesc, string author, string genre)
        {
            if (String.IsNullOrEmpty(bookName)) this.bookName = "";
            else this.bookName = bookName;
            if (String.IsNullOrEmpty(bookDesc)) this.bookDesc = "";
            else this.bookDesc = bookDesc;
            if (String.IsNullOrEmpty(genre)) this.genre = "";
            else this.genre = genre;
            if (String.IsNullOrEmpty(author)) this.author = "";
            else this.author = author;
        }
        public string bookName { get; set; }
        public string bookDesc { get; set; }
        public int count { get; set; }
        public string genre { get; set; }
        public string author { get; set; }
    }
}
