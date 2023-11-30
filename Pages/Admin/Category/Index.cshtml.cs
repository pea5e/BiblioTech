using BiblioTech.Pages.Admin.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BiblioTech.Pages.Admin.Category
{
    public class CategoryModel : PageModel
    {

        public List<Categorie> listCategories = Categorie.GetCategories() ;
        public void OnGet()
        {
        }
        public void OnPost()
        {
        }
    }
}
