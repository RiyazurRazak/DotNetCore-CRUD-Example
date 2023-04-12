using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication1.Pages.Book
{
    public class CreateBookModel : PageModel
    {

        Books book = new Books();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {

        }
        public void OnPost()
        {
            const string CONN_STRING = "Data Source = DESKTOP-50S2HSR;Initial Catalog=LMS_DB;Integrated Security=True;Encrypt=False;";
            SqlConnection sqlConnection = new SqlConnection(CONN_STRING);
            sqlConnection.Open();

            book.Id = Request.Form["id"];
            book.BookName = Request.Form["name"];
            book.category = Request.Form["category"];
            book.author = Request.Form["author"];
            book.price = Convert.ToDecimal(Request.Form["price"]);
            book.publication = Request.Form["publication"];
            book.publish_date = Convert.ToDateTime(Request.Form["publish_date"]);
            book.book_edition = Request.Form["book_edition"];

            if (book.price > 100)
            {
                errorMessage = "The price can't be more than 100";
                return;
            }

            book.date_arrival = Convert.ToDateTime("2023-03-01");
            book.rack_num = "A1";
            book.supplier_id = "S03";


            try
            {
                errorMessage = "";
                successMessage = "";

                string query = $"insert into lms_book_details values" +
               $"( '{book.Id}','{book.BookName}','{book.category}'," +
               $" '{book.author}', '{book.publication}', " +
               $"'{book.publish_date}', {book.book_edition}, {book.price}, " +
               $"'{book.rack_num}' ,'{book.date_arrival}', '{book.supplier_id}' )";

                Console.WriteLine(query);
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.ExecuteNonQuery();

                successMessage = "Book added successfully";

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);

                errorMessage = ex.Message;
            }

        }
    }

    public class Books
    {
        public string Id { get; set; }
        public string BookName { get; set; }
        public string category { get; set; }
        public string author { get; set; }
        public decimal price { get; set; }
        public string publication { get; set; }

        public string book_edition { get; set; }

        public DateTime publish_date { get; set; }
        public DateTime date_arrival { get; set; }
        public string rack_num { get; set; }
        public string supplier_id { get; set; }
    }

}
