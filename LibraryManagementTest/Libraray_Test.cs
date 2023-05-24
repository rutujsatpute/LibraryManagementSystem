using LibraryManagement;
using NUnit.Framework;
using Spectre.Console;
using System.Data.SqlClient;
using Moq;
using System.Net.Http.Headers;

namespace LibraryManagementTest
{
    public class Library_Test
    {
        private SqlConnection sqlConnection;
        private Library library;

        [OneTimeSetUp]
        public void Setup()
        {
            // Set up the necessary dependencies
            sqlConnection = new SqlConnection("Server=IN-PF2HZG00; database=Librarymanagement; Integrated Security=true");
            library = new Library();
        }
    }

    [Test]
    public void Add_Book_WhenRecoedInsert_ReturnsNofoRowsAffected()
    {
        var repo = new Mock<IBookRepository>();
        repo.Setup(x => x.Add_Book("Harry Potter", "Author", "XYZ", 2, 199)).Returns(1);
        var service = new Mock<BookService>();
        var result = service.Add_Book("Harry Potter", "Author", "XYZ", 2, 199);

        Assert.That(result, Is.EqualTo(1));
    }   
        
    
}

