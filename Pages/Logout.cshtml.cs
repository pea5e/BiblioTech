using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BiblioTech.Pages
{
    public class SignoutModel : PageModel
    {
        public void OnGet()
        {
            Utilisateur.Logout(Request.Cookies["session"]);
        }
    }
}
