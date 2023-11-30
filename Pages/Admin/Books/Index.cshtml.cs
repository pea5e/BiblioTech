using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BiblioTech.Pages.Admin.Books
{
    public class BooksModel : PageModel
    {
        public List<Livre> listLivres = Livre.GetLivres();
        public void OnGet()
        {
        }
        public void OnPost()
        {
        }
    }
}
