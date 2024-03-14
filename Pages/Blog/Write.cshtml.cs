using BiblioTech.Pages.Admin.Author;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Text.RegularExpressions;


namespace BiblioTech.Pages.Blog
{
    public class WriteModel : PageModel
    {
        public string Errormessage;
        public void OnGet()
        {
            string session = Request.Cookies["session"];
            if (string.IsNullOrEmpty(session) || !Utilisateur.isLogged(session))
            {
                Response.Redirect("/Blog");
            }
        }

        public async void OnPost()
        {
            if (((string)Request.Form["Titre"]).Length == 0)
            {
                Errormessage = "Le champs de Titre est obligatoire";
            }
            else if (((string)Request.Form["Para"]).Length == 0 )
            {
                Errormessage = "Le champs de text est obligatoire";
            }
            else if (((string)Request.Form["Imgpath"]).Length == 0)
            {
                Errormessage = "Le champs de image est obligatoire";
            }
            else
            {
                
                Article article = new Article(Request.Form["Titre"], Request.Form["Para"], Request.Form["Imgpath"], Request.Cookies["session"]);
                if (article.Save())
                    Response.Redirect("/Blog/Home");
                else
                    Errormessage = "Erreur de Serveur";
            }
        }
    }
}
