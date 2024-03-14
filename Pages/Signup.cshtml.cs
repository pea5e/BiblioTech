using BiblioTech.Pages.Admin.Author;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace BiblioTech.Pages
{
    public class SignupModel : PageModel
    {

        public string Errormessage;
        public string nom;
        public string email;

        public void OnGet()
        {
            string session = Request.Cookies["session"];
            if (!string.IsNullOrEmpty(session) && Utilisateur.isLogged(session))
            {
                Response.Redirect("/Blog/Home");
            }
        }

        public void OnPost()
        {
            if (((string)Request.Form["nom"]).Length == 0)
            {
                email = Request.Form["email"];
                Errormessage = "Le champs de nom est obligatoire";
            }
            else if (((string)Request.Form["Email"]).Length == 0 )
            {
                nom = Request.Form["nom"];
                Errormessage = "Email obligatoire";
            }
            else if (!Regex.Match(Request.Form["Email"], @"[\w\d._]+@[\w-]+.\w+").Success)
            {
                nom = Request.Form["nom"];
                Errormessage = "Email invalid";
            }
            else if (Utilisateur.isFindMail((string)Request.Form["Email"]))
            {
                nom = Request.Form["nom"];
                Errormessage = "Email Deja utilise";
            }
            else if (((string)Request.Form["password"]).Length < 8 || ((string)Request.Form["password"]).Length > 16 )
            {
                nom = Request.Form["nom"];
                email = Request.Form["email"];
                Errormessage = "mot de passe invalid";

            }
            else
            {
                Utilisateur user = new Utilisateur(Request.Form["Nom"], Request.Form["Email"], Request.Form["password"]);
                if (user.Save())
                {
                    Response.Redirect("/Login");
                }
                else
                    Errormessage = "Erreur de Serveur";
            }
        }
    }
}
