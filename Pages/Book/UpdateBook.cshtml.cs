using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Globalization;
using WebApplication1.Pages.Book;

namespace PracticeWeb.Pages.Book
{

    public class UpdateBookModel : PageModel
    {
        public Books book = new Books();
        public string message = "";
        public string BookCode = "";
        public void OnGet()
        {
            try
            {
                BookCode = Request.Query["BookCode"];

                SqlConnection connection = new SqlConnection("Data Source = DESKTOP-50S2HSR;Initial Catalog=LMS_DB;Integrated Security=True;Encrypt=False;");
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = $"SELECT BOOK_CODE,BOOK_TITLE,AUTHOR,CATEGORY,PUBLICATION," +
                    $"publish_date,BOOK_EDITION,PRICE FROM LMS_BOOK_DETAILS WHERE BOOK_CODE = '{BookCode}';";



                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    book.BookTitle = (string)reader["book_title"];
                    book.Author = (string)reader["author"];
                    book.Category = (string)reader["category"];
                    book.Publication = (string)reader["publication"];
                    book.PublicDate = (DateTime)reader["publish_date"];
                    book.BookEdition = (int)reader["book_edition"];
                    book.Price = (int) reader["price"];

                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

        public void OnPost()
        {
            message = "";
            BookCode = Request.Query["BookCode"];
            try
            {

                SqlConnection connection = new SqlConnection("Data Source = DESKTOP-50S2HSR;Initial Catalog=LMS_DB;Integrated Security=True;Encrypt=False;");
                connection.Open();

                book.BookTitle = Request.Form["BookTitle"];
                book.Author = Request.Form["Author"];
                book.Category = Request.Form["Category"];
                book.Publication = Request.Form["Publication"];
                book.PublicDate = Convert.ToDateTime(Request.Form["PublicDate"]);
                book.BookEdition = Convert.ToInt32(Request.Form["BookEdition"]);
                book.Price = Convert.ToInt32(Request.Form["Price"]);

                SqlCommand cmd = connection.CreateCommand();

                Console.WriteLine(book.Category);
                Console.WriteLine(BookCode);

                try
                {
                    cmd.CommandText = $"UPDATE LMS_BOOK_DETAILS SET BOOK_TITLE ='{book.BookTitle}'," +
                        $"AUTHOR = '{book.Author}',CATEGORY = '{book.Category}', PUBLICATION = '{book.Publication}'," +
                        $"publish_date = '{book.PublicDate}', BOOK_EDITION = {book.BookEdition}, PRICE = {book.Price} WHERE " +
                        $"BOOK_CODE = '{BookCode}'";

                    cmd.ExecuteNonQuery();

                    message = "Book Updated Successfully";

                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    Console.WriteLine(ex.Message);
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
                Console.WriteLine(ex.Message);
            }

        }
    }
    public class Books
    {
        public string BookCode { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Publication { get; set; }
        public DateTime PublicDate { get; set; }
        public int BookEdition { get; set; }
        public int Price { get; set; }
        public string RackNum { get; set; }
        public DateTime DateArrival { get; set; }
        public string SupplierId { get; set; }
    }


}