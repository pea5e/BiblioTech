using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BiblioTech.Pages
{
    public class adminModel : PageModel
    {
        public void OnGet()
        {
            string? session_id = HttpContext.Request.Cookies["session_id"];
            if(session_id != null)
            {
                
            }
        }
    }
}
