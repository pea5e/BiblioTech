using BiblioTech.Pages.Admin.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BiblioTech.Pages.Admin.Editor
{
    public class EditorModel : PageModel
    {
        public List<Editeur> listEditeur = Editeur.GetEditeurs();
        public void OnGet()
        {
        }

        public void OnPost()
        {
        }
    }
}
