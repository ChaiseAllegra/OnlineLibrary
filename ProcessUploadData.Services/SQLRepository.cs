using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Globalization;
using System.Net;
using Library.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ProcessUploadData.Services
{
    public class SQLRepository : IFileRepository
    {
        private string _connectionString = "server=(localdb)\\MSSQLLocalDB;database=Library;Integrated Security=true";
        public SQLRepository( )
        {

        }
        public List<Book> GetAllBooks() 
        {
            List<Book> BookList = new List<Book>();
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("GetAllBooks", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookList.Add(new Book
                                (
                                    reader["Name"].ToString(),
                                    reader["Description"].ToString(),
                                    (int)reader["Count"],
                                    reader["Genre"].ToString(),
                                    reader["Author"].ToString()
                                )
                            ) ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
      
            return BookList;
        }
        public bool AddBook(Book newBook)
        {
            if (newBook == null) return false;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("AddNewBook", con)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    con.Open();
                    command.Parameters.AddWithValue("@Name", newBook.bookName);
                    command.Parameters.AddWithValue("@Desc", newBook.bookDesc);
                    command.Parameters.AddWithValue("@Count", newBook.count);
                    command.Parameters.AddWithValue("@Genre", newBook.genre);
                    command.Parameters.AddWithValue("@Author", newBook.author);
                    command.Parameters.AddWithValue("@Total", newBook.count);
                    if (command.ExecuteNonQuery()==0)return false; 
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool CheckOutBook(Book currBook, int newCount)
        {
            if (currBook == null) return false;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("CheckOutBook", con)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    con.Open();
                    command.Parameters.AddWithValue("@amount", newCount);
                    command.Parameters.AddWithValue("@bookName", currBook.bookName);
                    if (command.ExecuteNonQuery() == 0) return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool CheckInBook(Book currBook, int newCount)
        {
            if (currBook == null) return false;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("CheckInBook", con)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    con.Open();
                    command.Parameters.AddWithValue("@amount", newCount);
                    command.Parameters.AddWithValue("@bookName", currBook.bookName);
                    if (command.ExecuteNonQuery() == 0) return false;
                    
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public List<AccountInfo> GetAllUserBooks(string userName)
        {
            List<AccountInfo> list = new List<AccountInfo>();
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("GetAllUserBooks", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Name", userName);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new AccountInfo
                                (
                                    reader["UserName"].ToString(),
                                    reader["BookName"].ToString(),
                                    (int)reader["Count"]
                                )
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return list;
        }
        public bool CheckInCheckOutTable(string userName, Book currBook)
        {
            if (currBook == null|| String.IsNullOrEmpty(userName)) return false;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("UpdateCheckOutAdd", con)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    con.Open();
                    command.Parameters.AddWithValue("@Name", userName);
                    command.Parameters.AddWithValue("@BookName", currBook.bookName);
                    command.Parameters.AddWithValue("@Count", currBook.count);
                    if (command.ExecuteNonQuery() == 0) return false;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool CheckOutCheckOutTable(string userName, Book currBook)
        {
            if (currBook == null) return false;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("UpdateCheckOutSub", con)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    con.Open();
                    command.Parameters.AddWithValue("@Name", userName);
                    command.Parameters.AddWithValue("@BookName", currBook.bookName);
                    command.Parameters.AddWithValue("@Count", currBook.count);
                    if (command.ExecuteNonQuery() == 0) return false;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool InsertCheckOutTable(string userName,Book currBook)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("InsertCheckOut", con)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    con.Open();
                    command.Parameters.AddWithValue("@Name", userName);
                    command.Parameters.AddWithValue("@BookName", currBook.bookName);
                    command.Parameters.AddWithValue("@Count", currBook.count);
                    if (command.ExecuteNonQuery() == 0) return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool DeleteEmptyCheckOutTable()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("DeleteFromCheckOutTable", con)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    con.Open();
                    if (command.ExecuteNonQuery() == 0) return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        bool IFileRepository.AddUser(User newUser)
        {
                if (newUser == null) return false;
                try
                {
                    using (SqlConnection con = new SqlConnection(_connectionString))
                    using (var command = new SqlCommand("InsertUser", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        con.Open();
                        command.Parameters.AddWithValue("@UserName", newUser.userName);
                        command.Parameters.AddWithValue("@Password", newUser.password);
                        command.Parameters.AddWithValue("@LoggedIn", 1);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
        }
        public bool UpdateUser(User currUser,int status)
        {
            if (currUser == null) return false;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("UpdateUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    con.Open();
                    command.Parameters.AddWithValue("@user", currUser.userName);
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@password", currUser.password);
                    if (command.ExecuteNonQuery() == 0) return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }  
        public List<Book> SearchBook(Book book)
        {
            List<Book> BookList = new List<Book>();
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SearchBooks", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@Name", book.bookName);
                    command.Parameters.AddWithValue("@Desc", book.bookDesc);
                    command.Parameters.AddWithValue("@Genre", book.genre);
                    command.Parameters.AddWithValue("@Author", book.author);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookList.Add(new Book
                                (
                                    reader["Name"].ToString(),
                                    reader["Description"].ToString(),
                                    (int)reader["Count"],
                                    reader["Genre"].ToString(),
                                    reader["Author"].ToString()
                                )
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return BookList;
        }
    }
}
