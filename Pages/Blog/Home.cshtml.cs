using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BiblioTech.Pages.Blog
{
    public class HomeModel : PageModel
    {
        public List<Article> articles;
        public void OnGet()
        {
            string session = Request.Cookies["session"];
            if (string.IsNullOrEmpty(session) ||  ! Utilisateur.isLogged(session))
            {
                Response.Redirect("/Blog");
            }
            articles = Article.Get_Articles();
        }
    }
}
