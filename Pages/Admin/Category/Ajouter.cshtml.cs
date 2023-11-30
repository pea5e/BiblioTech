using BiblioTech.Pages.Admin.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace BiblioTech.Pages.Admin.Category
{
    public class AjouterModel : PageModel
    {
        public string Errormessage;
        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (((string)Request.Form["Nom"]).Length == 0)
            {
                Errormessage = "Le champs de nom est obligatoire";
            }
            else
            {
                Categorie cat = new Categorie(Request.Form["Nom"], Request.Form["Description"]);
                if (cat.save())
                    Response.Redirect("/Admin/Category");
                else
                    Errormessage = "Erreur de Serveur";
            }
        }
    }
}
