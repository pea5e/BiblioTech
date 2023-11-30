using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace BiblioTech.Pages.Admin.Author
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
            else if (((string)Request.Form["Email"]).Length > 0 && !Regex.Match(Request.Form["Email"], @"[\w\d._]+@[\w-]+.\w+").Success)
            {
                Errormessage = "Email invalid";
            }
            else if (((string)Request.Form["Telephone"]).Length > 0 && !Regex.Match(Request.Form["Telephone"], @"0\d{9}").Success)
            {
                Errormessage = "Numero Telephone invalid";

            }
            else
            {
                Auteur auteur = new Auteur(Request.Form["Nom"], Request.Form["Email"], Request.Form["Telephone"], Request.Form["Adresse"]);
                if (auteur.save())
                    Response.Redirect("/Admin/Author");
                else
                    Errormessage = "Erreur de Serveur";
            }
        }
    }
}
