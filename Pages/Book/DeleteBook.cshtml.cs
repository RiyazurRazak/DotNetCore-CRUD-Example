using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracticeWeb.Pages.Book;
using System.Data.SqlClient;

namespace WebApplication1.Pages.Book
{
    public class DeleteBookModel : PageModel
    {
        public string bookCode;
        public void OnGet()
        {
          bookCode = Request.Query["BookCode"];
        }

        public void OnPost()
        {
            try
            {
                bookCode = Request.Query["BookCode"];
                SqlConnection connection = new SqlConnection("Data Source = DESKTOP-50S2HSR;Initial Catalog=LMS_DB;Integrated Security=True;Encrypt=False;");
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"DELETE FROM LMS_BOOK_DETAILS WHERE BOOK_CODE='{bookCode}';";
                cmd.ExecuteReader().Close();
                Response.Redirect("/Book/Index");
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
    }
}
