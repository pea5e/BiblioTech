using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BiblioTech.Pages.Admin.Author
{
    public class AuthorModel : PageModel
    {
        public int index = 0;
        public List<Auteur> listAuteurs = Auteur.GetAuteurs();
        public void OnGet()
        {
        }

        public void OnPost()
        {
            index = Int32.Parse(Request.Form["id"]);
        }
    }
}
