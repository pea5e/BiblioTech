using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace BiblioTech.Pages.api.v1
{
    public class emailusedModel : PageModel
    {
        public string response;
        public async void  OnGet()
        {
            string email = Request.Query["email"];
            if (Utilisateur.isFindMail(email))
            {
                response = "true";
            }
            else
            {
                response = "false";
            }
        }

        public async void OnPost()
        {

            string email = Request.Form["email"];
            //Console.WriteLine(Request.Body.);
            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            if (Utilisateur.isFindMail(email))
            {
                await Response.WriteAsJsonAsync(true);
            }
            await Response.WriteAsJsonAsync(false);
        }
    }
}
