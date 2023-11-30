using BiblioTech.Pages.Admin.Author;
using BiblioTech.Pages.Admin.Category;
using BiblioTech.Pages.Admin.Editor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace BiblioTech.Pages.Admin.Books
{
    public class EditModel : PageModel
    {
        public string Errormessage;
        public List<Categorie> listCat = Categorie.GetCategories();
        public List<Auteur> listAut = Auteur.GetAuteurs();
        public List<Editeur> listEdit = Editeur.GetEditeurs();
        public Livre liv;


        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (!Request.Headers["Referer"].ToString().ToLower().EndsWith("/books"))
            {
                if (((string)Request.Form["Titre"]).Length == 0)
                {
                    Errormessage = "Le champs de titre est obligatoire";
                }
                else if (((string)Request.Form["Annee"]).Length > 0 && !Regex.Match(Request.Form["Annee"], @"20([0-1][0-9]|2[0-3])").Success)
                {
                    Errormessage = "l'annee invalid";
                }
                else if (Request.Form["IDA"] == "DEFAULT")
                {
                    Errormessage = "L'auteur est obligatoire";
                }
                else if (Request.Form["IDE"] == "DEFAULT")
                {
                    Errormessage = "L'editeur est obligatoire";
                }
                else if (Request.Form["IDC"] == "DEFAULT")
                {
                    Errormessage = "La categorie est obligatoire";
                }
                else
                {
                    int annee = 0;
                    if (((string)Request.Form["Annee"]).Length > 0)
                        annee = Int32.Parse(Request.Form["Annee"]);
                    Livre livre = new Livre(Request.Form["Titre"], Request.Form["Description"], annee, Int32.Parse(Request.Form["IDE"]), Int32.Parse(Request.Form["IDC"]), Int32.Parse(Request.Form["IDA"]), Int32.Parse(Request.Form["id"]));
                    if (livre.update())
                        Response.Redirect("/Admin/Books");
                    else
                        Errormessage = "Erreur de Serveur";
                }
            }
            liv = new Livre(Int32.Parse((string)Request.Form["id"]));
            
        }
    }
}
