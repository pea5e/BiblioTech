using BiblioTech.Pages.Admin.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BiblioTech.Pages.Admin.Editor
{
    public class DeleteModel : PageModel
    {
        Editeur edt;
        public void OnGet()
        {
        }
        public void OnPost()
        {
            edt = new Editeur(Int32.Parse((string)Request.Form["id"]));
            edt.remove();
            Response.Redirect("/Admin/Editor");
        }
    }
}
