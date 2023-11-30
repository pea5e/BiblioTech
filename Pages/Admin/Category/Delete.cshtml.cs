using BiblioTech.Pages.Admin.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BiblioTech.Pages.Admin.Category
{
    public class DeleteModel : PageModel
    {
        Categorie cat;
        public void OnGet()
        {
        }
        public void OnPost()
        {
            cat = new Categorie(Int32.Parse((string)Request.Form["id"]));
            cat.remove();
            Response.Redirect("/Admin/Category");
        }
    }
}
