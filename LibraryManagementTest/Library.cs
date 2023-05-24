using System.Data.SqlClient;
using Spectre.Console;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Channels;
using System.Net.Http.Headers;

namespace LibraryManagement
{
    public class Library
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("Server=IN-PF2HZG00; database=Librarymanagement; Integrated Security=true");
            con.Open();
            return con;
        }
        public int Add_Book()
        {
            SqlConnection con = GetConnection();
            string query = $"insert into NewBook values (@bookname,@bookauthor,@bookpublisher,@bookquantity,@bookprice,@bookavailableqty)";
            SqlCommand cmd = new SqlCommand(query, con);

            string bookname = AnsiConsole.Ask<string>("[skyblue1]Enter Book_Name:[/]");
            string bookauthor = AnsiConsole.Ask<string>("[skyblue1]Enter Book_Author:[/]");
            string bookpublisher = AnsiConsole.Ask<string>("[skyblue1]Enter Book_Publisher:[/]");
            byte bookquantity = AnsiConsole.Ask<byte>("[skyblue1]Enter Book_Quantity:[/]");
            decimal bookprice = AnsiConsole.Ask<decimal>("[skyblue1]Enter Book_Price:[/]");

            cmd.Parameters.AddWithValue("@bookname", bookname);
            cmd.Parameters.AddWithValue("@bookauthor", bookauthor);
            cmd.Parameters.AddWithValue("@bookpublisher", bookpublisher);
            cmd.Parameters.AddWithValue("@bookquantity", bookquantity);
            cmd.Parameters.AddWithValue("@bookprice", bookprice);
            cmd.Parameters.AddWithValue("@bookavailableqty", bookquantity);

            cmd.ExecuteNonQuery();

            AnsiConsole.MarkupLine("[springgreen1]Book Added Sucessfully [/]");

            con.Close();
            return 0;
           
        }

        public static void Librarys(string query)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            var table = new Table();
            table.AddColumn("bookid");
            table.AddColumn("bookname");
            table.AddColumn("bookauthor");
            table.AddColumn("bookpublisher");
            table.AddColumn("bookquantity");
            table.AddColumn("bookprice");
            table.AddColumn("bookavailableqty");
            table.Title("[underline purple4_1]Book Details[/]");
            table.BorderColor(Color.DarkViolet_1);
            foreach (var column in table.Columns)
            {
                column.Centered();
            }

            while (reader.Read())
            {
                table.AddRow(reader["bookid"].ToString(), reader["bookname"].ToString(), reader["bookauthor"].ToString(), reader["bookpublisher"].ToString(), reader["bookquantity"].ToString(), reader["bookprice"].ToString(), reader["bookavailableqty"].ToString());
            }

            AnsiConsole.Write(table);
            con.Close();
        }

        public void Edit_book()
        {
            try
            {
                SqlConnection con = GetConnection();
                int bookid = AnsiConsole.Ask<int>("Enter the Book_ID you want to update:");
                string query = $"update NewBook set bookname=@bookname,bookauthor=@bookauthor,bookpublisher=@bookpublisher,bookquantity=@bookquantity,bookprice=@bookprice,bookavailableqty=@bookquantity where bookid = {bookid}";
                SqlCommand cmd = new SqlCommand(query, con);
                {
                    string bookname = AnsiConsole.Ask<string>("[skyblue1]Enter Book_Name:[/]");
                    string bookauthor = AnsiConsole.Ask<string>("[skyblue1]Enter Book_Author:[/]");
                    string bookpublisher = AnsiConsole.Ask<string>("[skyblue1]Enter Book_Publisher:[/]");
                    byte bookquantity = AnsiConsole.Ask<byte>("[skyblue1]Enter Book_Quantity:[/]");
                    decimal bookprice = AnsiConsole.Ask<decimal>("[skyblue1]Enter Book_Price:[/]");

                    cmd.Parameters.AddWithValue("@bookname", bookname);
                    cmd.Parameters.AddWithValue("@bookauthor", bookauthor);
                    cmd.Parameters.AddWithValue("@bookpublisher", bookpublisher);
                    cmd.Parameters.AddWithValue("@bookquantity", bookquantity);
                    cmd.Parameters.AddWithValue("@bookprice", bookprice);
                    cmd.Parameters.AddWithValue("@bookavailableqty", bookquantity);
                    cmd.ExecuteNonQuery();

                    AnsiConsole.MarkupLine("[springgreen1]Book Details Updated Sucessfully [/]");
                    con.Close();
                }
               
            }catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red1]An error occurred: {ex.Message}[/]");
            }
        }

        public void Delete_Book()
        {
            try
            {
                SqlConnection con = GetConnection();
                int bookid = AnsiConsole.Ask<int>("[skyblue1]Enter the Book_ID you want to Delete:[/]");
                string query = $"delete from NewBook where bookid = {bookid}";
                SqlCommand cmd = new SqlCommand(query, con);

                int bookdetails = cmd.ExecuteNonQuery();

                if (bookdetails > 0)
                {
                    AnsiConsole.MarkupLine("[springgreen1]Book_Deleted Sucessfully [/]");
                }
                else
                {
                    AnsiConsole.MarkupLine("[greenyellow]No book found with this Book_ID[/]");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red1]An error occurred: {ex.Message}[/]");
            }
        }

        public void Add_Student()
        {
            SqlConnection con = GetConnection();
            string query = $"insert into StudentDetails values (@studentname,@studentrollno)";
            SqlCommand cmd = new SqlCommand(query, con);

            string studentname = AnsiConsole.Ask<string>("[skyblue1]Enter Student_Name:[/]");
            byte studentrollno = AnsiConsole.Ask<byte>("[skyblue1]Enter Student_RollNo:[/]");
            cmd.Parameters.AddWithValue("@studentname", studentname);
            cmd.Parameters.AddWithValue("@studentrollno", studentrollno);
            cmd.ExecuteNonQuery();
            AnsiConsole.MarkupLine("[springgreen1]Student Added Sucessfully [/]");

            con.Close();
        }

        public void Edit_Student_Details()
        {
            SqlConnection con = GetConnection();
            int studentid = AnsiConsole.Ask<int>("[skyblue1]Enter the Student_ID you want to update: [/]");
            string query = $"update StudentDetails set studentname=@studentname,studentrollno=@studentrollno where studentid = {studentid}";
            SqlCommand cmd = new SqlCommand(query, con);

            string studentname = AnsiConsole.Ask<string>("[skyblue1]Enter Student_Name:[/]");
            byte studentrollno = AnsiConsole.Ask<byte>("[skyblue1]Enter Student_RollNo:[/]");

            cmd.Parameters.AddWithValue("@studentname", studentname);
            cmd.Parameters.AddWithValue("@studentrollno", studentrollno);
            cmd.ExecuteNonQuery();

            AnsiConsole.MarkupLine("[springgreen1]Student Details Updated Sucessfully [/]");

            con.Close();
        }


        public void Delete_Student()
        {
            try
            {
                SqlConnection con = GetConnection();
                int studentid = AnsiConsole.Ask<int>("[skyblue1]Enter the Student_ID you want to Delete:[/]");
                string query = $"delete from StudentDetails where studentid = {studentid}";
                SqlCommand cmd = new SqlCommand(query, con);

                int studentdetails = cmd.ExecuteNonQuery();
                if (studentdetails == 0)
                {
                    AnsiConsole.MarkupLine("[greenyellow]No Student found with this Student_ID [/]");                   
                }
                else
                {
                    AnsiConsole.MarkupLine("[springgreen1]Student Deleted Sucessfully [/]"); ;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red1]An error occurred: {ex.Message}[/]");
            }
        }

            public void Issue_Book()
        {
            string query = "select * from NewBook";
            Library.Librarys(query);

            SqlConnection con = GetConnection();
            string query1 = "select count(*) from StudentDetails where studentrollno = @studentrollno and studentname = @studentname";
            SqlCommand cmd = new SqlCommand(query1, con);

            try
            {

                int studentrollno = AnsiConsole.Ask<int>("[skyblue1]Enter Student_RollNo:[/]");
                string studentname = AnsiConsole.Ask<string>("[skyblue1]Enter Student_Name:[/]");

                cmd.Parameters.AddWithValue("@studentrollno", studentrollno);
                cmd.Parameters.AddWithValue("@studentname", studentname);

                int count = (int)cmd.ExecuteScalar();

                if (count == 0)
                {
                    AnsiConsole.MarkupLine("[red1]Student is Not Register [/]");
                }
                else
                {
                    string query2 = $"insert into issuedregister values (@studentrollno,@studentname,@bookid,@bookname,@issueddate)";
                    SqlCommand cmd1 = new SqlCommand(query2, con);

                    int bookid = AnsiConsole.Ask<int>("[skyblue1]Enter Book_ID:[/]");
                    string bookname = AnsiConsole.Ask<string>("[skyblue1]Enter Book_Name:[/]");

                    cmd1.Parameters.AddWithValue("@studentrollno", studentrollno);
                    cmd1.Parameters.AddWithValue("@studentname", studentname);
                    cmd1.Parameters.AddWithValue("@bookid", bookid);
                    cmd1.Parameters.AddWithValue("@bookname", bookname);
                    cmd1.Parameters.AddWithValue("@issueddate", DateTime.Now);

                    cmd1.ExecuteNonQuery();
                    string query3 = "update NewBook set bookavailableqty = bookavailableqty - 1 where bookid = @bookid";

                    using (SqlCommand cmd2 = new SqlCommand(query3, con))
                    {
                        cmd2.Parameters.AddWithValue("@bookid", bookid);
                        cmd2.ExecuteNonQuery();
                    }

                    AnsiConsole.MarkupLine("[springgreen1]Book_Issued To Student [/]");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[red1]An error occurred: [/]" + ex.Message);
            }
            con.Close();
        }

        public void Return_Book()
        {
            SqlConnection con = GetConnection();
            string query = "select count(*) from issuedregister where bookid = @bookid and studentrollno = @studentrollno";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                int bookid = AnsiConsole.Ask<int>("[skyblue1]Enter Book_ID:[/]");
                int studentrollno = AnsiConsole.Ask<int>("[skyblue1]Enter Student_RollNo:[/]");

                cmd.Parameters.AddWithValue("@bookid", bookid);
                cmd.Parameters.AddWithValue("@studentrollno", studentrollno);
                int count = (int)cmd.ExecuteScalar();

                if (count == 0)
                {
                    AnsiConsole.MarkupLine("[red1]The book is not currently borrowed by the student [/]");
                }
                else if (count == 1)
                {
                    string query1 = "delete from issuedregister where bookid = @bookid and studentrollno = @studentrollno";
                    using (SqlCommand rcmd = new SqlCommand(query1, con))
                    {
                        rcmd.Parameters.AddWithValue("@bookid", bookid);
                        rcmd.Parameters.AddWithValue("@studentrollno", studentrollno);
                        rcmd.ExecuteNonQuery();

                        string query2 = "update NewBook set bookavailableqty = bookavailableqty + 1 where bookid = @bookid";
                        using (SqlCommand cmd1 = new SqlCommand(query2, con))
                        {
                            cmd1.Parameters.AddWithValue("@bookid", bookid);
                            cmd1.ExecuteNonQuery();
                        }
                        AnsiConsole.MarkupLine("[springgreen1]Book_Return Successfully [/]");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            con.Close();
        }

        public void Search_Book_By_Author_Or_Publisher()
        {
            SqlConnection con = GetConnection();
            AnsiConsole.Markup("[springgreen1]Enter Author Or Publication Name: [/]");
            string find = Console.ReadLine();

            try
            {
                string selectQuery = "select bookname from NewBook where bookauthor like @Find OR bookpublisher like @Find";
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, con))
                {
                    selectCommand.Parameters.AddWithValue("@Find", "%" + find + "%");
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<string> bookNames = new List<string>();
                        while (reader.Read())
                        {
                            string bookName = reader.GetString(0);
                            bookNames.Add(bookName);
                        }
                        if (bookNames.Count > 0)
                        {
                            AnsiConsole.MarkupLine("[greenyellow]Following are the Books: [/]");

                            foreach (string bookName in bookNames)
                            {
                                var table = new Table();

                                table.Title("[underline purple4_1]Books_Details[/]");
                                table.BorderColor(Color.DarkViolet_1);
                                table.AddColumn(bookName);
                                table.LeftAligned();
                                AnsiConsole.Write(table);

                            }

                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red1] No books found [/]");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup("[red1]An error occurred: [/]" + ex.Message);
            }
            con.Close();
        }

        public static void Students(string query)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

              var table = new Table();
                table.AddColumn("studentid");
                table.AddColumn("studentname");
                table.AddColumn("studentrollno");
                table.Title("[underline purple4_1]Student_Details[/]");
                table.BorderColor(Color.DarkViolet_1);
                foreach (var column in table.Columns)
                {
                    column.Centered();
                }

                while (reader.Read())
                {
                    table.AddRow(reader["studentid"].ToString(), reader["studentname"].ToString(), reader["studentrollno"].ToString());
                }
            
            AnsiConsole.Write(table);
            con.Close();
        }
        public void Search_Student_By_RollNo()
        {
            try
            {
                SqlConnection con = GetConnection();

                string id = AnsiConsole.Ask<string>("[skyblue1]Enter Student Roll No.: [/]");
                string query = $"select * from StudentDetails where studentrollno = {id}";
                Library.Students(query);

                con.Close();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]An error occurred: {ex.Message}[/]");
            }
        }

        public static void StudentRegister(string query)
        {
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            var table = new Table();
            table.AddColumn("issueid");
            table.AddColumn("studentrollno");
            table.AddColumn("studentname");
            table.AddColumn("bookid");
            table.AddColumn("bookname");
            table.AddColumn("issueddate");
            table.Title("[underline purple4_1]Student_Details[/]");
            table.BorderColor(Color.DarkViolet_1);
            foreach (var column in table.Columns)
            {
                column.Centered();
            }

            while (reader.Read())
            {
                table.AddRow(reader["issueid"].ToString(), reader["studentrollno"].ToString(), reader["studentname"].ToString(), reader["bookid"].ToString(), reader["bookname"].ToString(), reader["issueddate"].ToString());
            }

            AnsiConsole.Write(table);
            con.Close();
        }

        public void Studentissuedregister()
        {
            SqlConnection con = GetConnection();
            string query = $"select * from issuedregister";
            Library.StudentRegister(query);

        }

        static void Main(string[] args)
        {
            AnsiConsole.Write(new FigletText("Libraray Management System").Centered().Color(Color.LightSeaGreen));
            Library lib = new Library();

            AnsiConsole.Markup("[underline greenyellow] Login into System[/]");
            Console.WriteLine();
            string username = AnsiConsole.Ask<string>("[skyblue1]Enter UserName:[/]");
            string password = AnsiConsole.Ask<string>("[skyblue1]Enter Password:[/]");

            SqlConnection conn = new SqlConnection("Server=IN-PF2HZG00; database=Librarymanagement; Integrated Security=true");
            {
                string query = "select * from loginTable where username = @Username and Pass = @password";
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn; cmd.CommandText = query;
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        conn.Open();
                        

                        if (password == "open")
                        {
                            int count = (int)cmd.ExecuteScalar();
                            bool close = true;
                            while (close)
                            {

                                AnsiConsole.MarkupLine("[skyblue1]\nMenu\n" +
                                "1)Add_Book\n" +
                                "2)Edit_Book_Details\n" +
                                "3)Delete_Book\n" +
                                "4)Add_Student\n" +
                                "5)Edit_Student_Details\n" +
                                "6)Delete_Student\n" +
                                "7)Issue_Book_to_Student\n" +
                                "8)Return_Book\n" +
                                "9)Search_Book_By_Author_Or_Publisher\n" +
                                "10)Search_Student_By_RollNo\n" +
                                "11)Student_Issued_Register\n" +
                                "12)Close\n\n [/]");

                                AnsiConsole.MarkupLine("[greenyellow]Choose your option from menu : [/]");

                                int option = int.Parse(Console.ReadLine());
                                if (option == 1)
                                {
                                    lib.Add_Book();
                                }
                                else if (option == 2)
                                {
                                    lib.Edit_book();
                                }
                                else if (option == 3)
                                {
                                    lib.Delete_Book();
                                }
                                else if (option == 4)
                                {
                                    lib.Add_Student();
                                }
                                else if (option == 5)
                                {
                                    lib.Edit_Student_Details();
                                }
                                else if (option == 6)
                                {
                                    lib.Delete_Student();
                                }
                                else if (option == 7)
                                {
                                    lib.Issue_Book();
                                }
                                else if (option == 8)
                                {
                                    lib.Return_Book();
                                }
                                else if (option == 9)
                                {
                                    lib.Search_Book_By_Author_Or_Publisher();
                                }
                                else if (option == 10)
                                {
                                    lib.Search_Student_By_RollNo();
                                }
                                else if (option == 11)
                                {
                                    lib.Studentissuedregister();
                                }
                                else if (option == 12)
                                {
                                    AnsiConsole.MarkupLine("[springgreen1]Thank You [/]");
                                    close = false;
                                    break;
                                }
                                
                            }
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red1]Invalid username or password [/]");
                        }
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.MarkupLine("[red1]An error occurred: [/]" + ex.Message);
                    }
                }
                AnsiConsole.MarkupLine("[greenyellow]Press Any Key To Exit [/]");
                Console.ReadKey();
            }

        }

    }
}