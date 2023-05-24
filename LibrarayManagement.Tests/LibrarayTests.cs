using LibraryManagement;
using Moq;
using System.Security.Cryptography.X509Certificates;
using System;

namespace LibrarayManagement.Tests
{
    public class Tests
    {
        [Test]
        public void Add_Book_WhenCalled_ReturnsRowsAffected()
        {
            var repo = new Mock<ILibraryRepository>();
            repo.Setup(x => x.Add_Book("The Secret of Success", "Willim Walker", "Pottermore", 2, 1000)).Returns(1);
            var service = new LibraryService(repo.Object);

            var result = service.Add_Book("The Secret of Success", "Willim Walker", "Pottermore", 2, 1000);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Edit_Book_WhenCalled_ReturnsRowsAffected()
        {
            var repo = new Mock<ILibraryRepository>();
            repo.Setup(x => x.Edit_Book(1, "The Secret of Success", "Willim Walker", "Pottermore", 2, 1000)).Returns(1);
            var service = new LibraryService(repo.Object);

            var result = service.Edit_Book(1, "The Secret of Success", "Willim Walker", "Pottermore", 2, 1000);

            Assert.That(result, Is.EqualTo(1));
        }


        [Test]
        public void Delete_ook_WhenCalled_ReturnsRowsAffected()
        {
            var repo = new Mock<ILibraryRepository>();
            var service = new LibraryService(repo.Object);
            service.Delete_Book(1);
            repo.Verify(x => x.Delete_Book(1), Times.Once);

        }

        [Test]
        public void Add_Student_WhenCalled_ReturnRowsAffected()
        {
            var repo = new Mock<ILibraryRepository>();
            repo.Setup(x => x.Add_Student("Sumit", 1)).Returns(1);
            var service = new LibraryService(repo.Object);
            Assert.DoesNotThrow(() => service.Add_Student("Sumit", 1));
        }

        [Test]
        public void Edit_Student_Details_WhenCalled_ReturnsRowsAffected()
        {
            var repo = new Mock<ILibraryRepository>();
            repo.Setup(x => x.Edit_Student_Details(1, "Sumit", 1)).Returns(1);
            var service = new LibraryService(repo.Object);
            var result = service.Edit_Student_Details(1, "Sumit", 1);

            Assert.That(result, Is.EqualTo(1));

        }

        [Test]
        public void Delete_Student_WhenCalled_ReturnsRowsAffected()
        {
            var repo = new Mock<ILibraryRepository>();
            var service = new LibraryService(repo.Object);
            service.Delete_Student(1);
            repo.Verify(x => x.Delete_Student(1), Times.Once);
        }

        [Test]
        public void Issue_Book_ByCheckingtheBookAlreadyIssued_ReturnsRowsAffected()
        {
            var repo = new Mock<ILibraryRepository>();
            int studentrollno = 1;
            int bookid = 1;
            repo.Setup(x => x.Issue_Book_ByCheckingtheBookAlreadyIssued_ReturnsRowsAffected(studentrollno, bookid)).Returns(1);
            var serice = new LibraryService(repo.Object);
            serice.Issue_Book_ByCheckingtheBookAlreadyIssued_ReturnsRowsAffected(1,1);
        }

        [Test]
        public void Issue_Book_WhentheBookAndStudentDetailsareValid_ReturnsRowsAffected()
        {
            var repo = new Mock<ILibraryRepository>();
            int studentrollno = 1;
            int bookid = 1;
            string bookname = "The Secret of Success";
            string studentname = "Sumit";
            repo.Setup(x => x.Issue_Book_ByCheckingtheBookAlreadyIssued_ReturnsRowsAffected(studentrollno, bookid)).Returns(1);
            var serice = new LibraryService(repo.Object);
            repo.Setup(x => x.Issue_Book_ByCheckingtheBookAlreadyIssued_ReturnsRowsAffected(studentrollno, bookid)).Returns(0);
            repo.Setup(x => x.Issue_Book_WhentheBookAndStudentDetailsareValid_ReturnsRowsAffected(bookid, bookname, studentname)).Returns(1);
           serice.Issue_Book_WhentheBookAndStudentDetailsareValid_ReturnsRowsAffected(1, "The Secret of Success", "Sumit");
        }

        [Test]
        public void Return_Book_IfBookisAlreadyIssued_ReturnsRowsAffected()
        {
            var repo = new Mock<ILibraryRepository>();
            int bookid = 1;
            int studentrollno = 1;
            repo.Setup(x => x.Return_Book_IfBookisAlreadyIssued_ReturnsRowsAffected(bookid,studentrollno)).Returns(1);
            var serice = new LibraryService(repo.Object);
            serice.Return_Book_IfBookisAlreadyIssued_ReturnsRowsAffected(1, 1);
        }

        [Test]
        public void Return_Book_IfBookIsNotIssuedToStudent_ReturnsRowsAffected()
        {
            var repo = new Mock<ILibraryRepository>();
            int bookid = 1;
            int studentrollno = 1;
            repo.Setup(x => x.Return_Book_IfBookisAlreadyIssued_ReturnsRowsAffected(bookid, studentrollno)).Returns(1);
            var serice = new LibraryService(repo.Object);
            repo.Setup(x => x.Return_Book_IfBookisAlreadyIssued_ReturnsRowsAffected(bookid, studentrollno)).Returns(0);
            repo.Setup(x => x.Return_Book_IfBookIsNotIssuedToStudent_ReturnsRowsAffected(bookid, studentrollno)).Returns(1);
            serice.Return_Book_IfBookIsNotIssuedToStudent_ReturnsRowsAffected(1, 1);
        }
    }
}
    


