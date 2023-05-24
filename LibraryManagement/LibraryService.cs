using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository _repository;

        public LibraryService(ILibraryRepository repository)
        {
            _repository = repository;
        }
        public int Add_Book(string bookname, string bookauthor, string bookpublisher, byte bookquantity, decimal bookprice)
        {
            return _repository.Add_Book(bookname,bookauthor, bookpublisher, bookquantity, bookprice);
        }
        public int Edit_Book(int bookid, string bookname, string bookauthor, string bookpublisher, byte bookquantity, decimal bookprice)
        {
            return _repository.Edit_Book(bookid,bookname,bookauthor,bookpublisher, bookquantity, bookprice);
        }
        public int Delete_Book(int bookid)
        {
            return _repository.Delete_Book(bookid);
        }
        public void Add_Student(string studentname, int studentrollno)
        {
             _repository.Add_Student(studentname, studentrollno);
        }
        public int Edit_Student_Details(int studentid, string studentname, int studentrollno)
        {
            return _repository.Edit_Student_Details(studentid, studentname, studentrollno);
        }
        public int Delete_Student(int studentid)
        {
            return _repository.Delete_Student(studentid);
        }
        public int Issue_Book_ByCheckingtheBookAlreadyIssued_ReturnsRowsAffected(int studentrollno,int bookid)
        {
            return _repository.Issue_Book_ByCheckingtheBookAlreadyIssued_ReturnsRowsAffected(studentrollno,bookid);
        }
        public int Issue_Book_WhentheBookAndStudentDetailsareValid_ReturnsRowsAffected(int bookid, string bookname, string studentname)
        {
            return _repository.Issue_Book_WhentheBookAndStudentDetailsareValid_ReturnsRowsAffected(bookid,bookname,studentname);
        }
        public int Return_Book_IfBookisAlreadyIssued_ReturnsRowsAffected(int bookid,int studentrollno)
        {
            return _repository.Return_Book_IfBookisAlreadyIssued_ReturnsRowsAffected(bookid,studentrollno);
        }
        public int Return_Book_IfBookIsNotIssuedToStudent_ReturnsRowsAffected(int bookid, int studentrollno)
        {
            return _repository.Return_Book_IfBookIsNotIssuedToStudent_ReturnsRowsAffected(bookid, studentrollno);
        }
        int ILibraryService.Add_Student(string studentname, int studentrollno)
        {
            throw new NotImplementedException();
        }
        public int Issue_Book_IsBookAlreadyIssued(int studentrollno, int bookid)
        {
            throw new NotImplementedException();
        }
    }
}
