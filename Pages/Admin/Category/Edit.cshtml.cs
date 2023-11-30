using BiblioTech.Pages.Admin.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BiblioTech.Pages.Admin.Category
{
    public class EditModel : PageModel
    {
        public Categorie cat;

        public string Errormessage;
        public void OnGet()
        {
        }

        public void OnPost()
        {

            if (!Request.Headers["Referer"].ToString().ToLower().EndsWith("/category"))
            {
                if (((string)Request.Form["Nom"]).Length == 0)
                {
                    Errormessage = "Le champs de nom est obligatoire";
                }
                else
                {
                    cat = new Categorie(Request.Form["Nom"], Request.Form["Description"], Int32.Parse(Request.Form["id"]));
                    if (cat.update())
                        Response.Redirect("/Admin/Category");
                    else
                        Errormessage = "Erreur de Serveur";
                }
            }
            cat = new Categorie(Int32.Parse((string)Request.Form["id"]));
        }
    }
}
