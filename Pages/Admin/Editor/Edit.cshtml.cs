using BiblioTech.Pages.Admin.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace BiblioTech.Pages.Admin.Editor
{
    public class EditModel : PageModel
    {
        public Editeur edt;

        public string Errormessage;
        public void OnGet()
        {
        }

        public void OnPost()
        {

            if (!Request.Headers["Referer"].ToString().ToLower().EndsWith("/editor"))
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
                    Editeur editeur = new Editeur(Request.Form["Nom"], Request.Form["Email"], Request.Form["Telephone"], Request.Form["Adresse"], Request.Form["Description"], Int32.Parse(Request.Form["id"]));

                    if (editeur.update())
                        Response.Redirect("/Admin/Editor");
                    else
                        Errormessage = "Erreur de Serveur";
                }
            }
            edt = new Editeur(Int32.Parse((string)Request.Form["id"]));
        }
    }
}
