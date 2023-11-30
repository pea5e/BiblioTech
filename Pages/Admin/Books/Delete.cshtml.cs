using BiblioTech.Pages.Admin.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BiblioTech.Pages.Admin.Books
{
    public class DeleteModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost()
        {
            Livre liv = new Livre(Int32.Parse((string)Request.Form["id"]));
            liv.remove();
            Response.Redirect("/Admin/Books");
        }
    }
}
