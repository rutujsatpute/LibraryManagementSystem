using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public interface ILibraryRepository
    {
        int Add_Book(string bookname, string bookauthor, string bookpublisher, byte bookquantity, decimal bookprice);
        int Edit_Book(int bookid,string bookname, string bookauthor, string bookpublisher, byte bookquantity, decimal bookprice);
        int Delete_Book(int bookid);
        int Add_Student(string studentname, int studentrollno);
        int Edit_Student_Details(int studentid, string studentname, int studentrollno);
        int Delete_Student(int studentid);
        int Issue_Book_ByCheckingtheBookAlreadyIssued_ReturnsRowsAffected(int studentrollno, int bookid);
        int Issue_Book_WhentheBookAndStudentDetailsareValid_ReturnsRowsAffected(int bookid, string bookname, string studentname);
        int Return_Book_IfBookisAlreadyIssued_ReturnsRowsAffected(int bookid, int studentrollno);
        int Return_Book_IfBookIsNotIssuedToStudent_ReturnsRowsAffected(int bookid, int studentrollno);
    }
}
