using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Text.RegularExpressions;

namespace BiblioTech.Pages
{
    public class LoginModel : PageModel
    {
        public string Errormessage;
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

            if (((string)Request.Form["Email"]).Length == 0)
            {
                Errormessage = "Email obligatoire";
            }
            else if (! Utilisateur.isFindMail((string)Request.Form["Email"]))
            {
                Errormessage = "Email introuvable";
            }
            else
            {
                Utilisateur user = new Utilisateur("",Request.Form["Email"], Request.Form["password"]);
                string s = user.login();
                if (s!=null)
                {
                    //session = s;
                    Response.Cookies.Append("session", s);
                    Response.Redirect("/Blog/Home");
                }
                else
                    Errormessage = "Mot de passe Invalid";
            }
        }
    }
}
