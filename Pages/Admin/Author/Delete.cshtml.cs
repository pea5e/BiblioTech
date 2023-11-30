using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace BiblioTech.Pages.Admin.Author
{
    public class DeleteModel : PageModel
    {
        Auteur aut;
        public void OnGet()
        {
        }
        public void OnPost()
        {
            aut = new Auteur(Int32.Parse((string)Request.Form["id"]));
            aut.remove();
            Response.Redirect("/Admin/Author");
        }
    }
}
