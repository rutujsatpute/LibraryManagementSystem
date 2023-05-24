using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class LibraryRepository : ILibraryRepository
    {
        SqlConnection con = new SqlConnection("Server=IN-PF2HZG00; database=Librarymanagement; Integrated Security=true");
        public int Add_Book(string bookname, string bookauthor, string bookpublisher, byte bookquantity, decimal bookprice)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"insert into NewBook values (@bookname,@bookauthor,@bookpublisher,@bookquantity,@bookprice,@bookavailableqty)",con);
            cmd.Parameters.AddWithValue("@bookname", bookname);
            cmd.Parameters.AddWithValue("@bookauthor", bookauthor);
            cmd.Parameters.AddWithValue("@bookpublisher", bookpublisher);
            cmd.Parameters.AddWithValue("@bookquantity", bookquantity);
            cmd.Parameters.AddWithValue("@bookprice", bookprice);
            cmd.Parameters.AddWithValue("@bookavailableqty", bookquantity);

            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int Edit_Book(int bookid, string bookname, string bookauthor, string bookpublisher, byte bookquantity, decimal bookprice)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"update NewBook set bookname=@bookname,bookauthor=@bookauthor,bookpublisher=@bookpublisher,bookquantity=@bookquantity,bookprice=@bookprice,bookavailableqty=@bookquantity where bookid = {bookid}", con);

            cmd.Parameters.AddWithValue("@bookid",bookid);
            cmd.Parameters.AddWithValue("@bookname", bookname);
            cmd.Parameters.AddWithValue("@bookauthor", bookauthor);
            cmd.Parameters.AddWithValue("@bookpublisher", bookpublisher);
            cmd.Parameters.AddWithValue("@bookquantity", bookquantity);
            cmd.Parameters.AddWithValue("@bookprice", bookprice);
            cmd.Parameters.AddWithValue("@bookavailableqty", bookquantity);
            
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int Delete_Book(int bookid)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"delete from NewBook where bookid = {bookid}", con);

            cmd.Parameters.AddWithValue("@bookid", bookid);
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int Add_Student(string studentname, int studentrollno)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"insert into StudentDetails values (@studentname,@studentrollno)", con);

            cmd.Parameters.AddWithValue("@studentname", studentname);
            cmd.Parameters.AddWithValue("@studentrollno", studentrollno);

            int res = cmd.ExecuteNonQuery();
            con.Close ();
            return res;
        }

        public int Edit_Student_Details(int studentid, string studentname, int studentrollno)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"update StudentDetails set studentname=@studentname,studentrollno=@studentrollno where studentid = {studentid}", con);

            cmd.Parameters.AddWithValue("@studentid", studentid);
            cmd.Parameters.AddWithValue("@studentname", studentname);
            cmd.Parameters.AddWithValue("@studentrollno", studentrollno);

            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int Delete_Student(int studentid)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"delete from StudentDetails where studentid = {studentid}");

            cmd.Parameters.AddWithValue("@studenid", studentid);

            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int Issue_Book_ByCheckingtheBookAlreadyIssued_ReturnsRowsAffected(int studentrollno,int bookid)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from issuedregister where (studentrollno = @studentrollno and bookid = @bookid)");

            cmd.Parameters.AddWithValue("@studentrollno", studentrollno);
            cmd.Parameters.AddWithValue("@bookid", bookid);

            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int Issue_Book_WhentheBookAndStudentDetailsareValid_ReturnsRowsAffected(int bookid, string bookname, string studentname)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand($"insert into issuedregister values (@studentrollno,@studentname,@bookid,@bookname,@issueddate)");

            cmd.Parameters.AddWithValue("@bookid", bookid);
            cmd.Parameters.AddWithValue("@bookname", bookname);
            cmd.Parameters.AddWithValue("@studentname", studentname);

            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int Return_Book_IfBookisAlreadyIssued_ReturnsRowsAffected(int bookid, int studentrollno)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from issuedregister where bookid = @bookid and studentrollno = @studentrollno");

            cmd.Parameters.AddWithValue("@bookid", bookid);
            cmd.Parameters.AddWithValue("@studentrollno", studentrollno);

            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int Return_Book_IfBookIsNotIssuedToStudent_ReturnsRowsAffected(int bookid, int studentrollno)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from issuedregister where bookid = @bookid and studentrollno = @studentrollno");

            cmd.Parameters.AddWithValue("@bookid", bookid);
            cmd.Parameters.AddWithValue("@studentrollno", studentrollno);

            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
    }
}
