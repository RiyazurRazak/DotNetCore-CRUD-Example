using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication1.Pages.Book
{
    public class BookModel : PageModel
    {
        public List<IBook> Books = new();
        public void OnGet()
        {
            SqlConnection connection = new SqlConnection("Data Source = DESKTOP-50S2HSR;Initial Catalog=LMS_DB;Integrated Security=True;Encrypt=False;");
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT distinct (book_title), category, author, book_code FROM lms_book_details";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                IBook book = new IBook();
                book.Title = (string) reader.GetValue(0);
                book.Category = (string)reader.GetValue(1);
                book.Author = (string)reader.GetValue(2);
                book.BookCode = (string)reader.GetValue(3);
                Books.Add(book);
            }
        }
    }

    public class IBook
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }

        public string BookCode { get; set; }
    }
}
