using BiblioTech.Pages.Admin.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BiblioTech.Pages.Admin.Author;
using BiblioTech.Pages.Admin.Category;
using BiblioTech.Pages.Admin.Editor;
using System.Reflection.Metadata;
using System.Net;

namespace BiblioTech.Pages
{
    public class BooksModel : PageModel
    {
        public List<Book> livres = null;
        public void OnGet()
        {
            string search = Request.Query["q"];
            if (search != null && search != "")
            {
                livres = new List<Book>();
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync("https://www.googleapis.com/books/v1/volumes?q=" + search).Result;
                    JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                    JArray books = (JArray)json["items"];
                    if (books != null)
                    {
                        foreach (JObject item in books)
                        {
                            JObject info = (JObject)item["volumeInfo"];
                            string title = info.GetValue("title").ToString();
                            JArray tmpa = ((JArray)info["authors"]);
                            string aut;
                            if (tmpa != null)
                            {
                                aut = tmpa.First.ToString();
                            }
                            else
                            {
                                aut = null;
                            }
                            JToken j = info.GetValue("description");
                            string descr;
                            if (j != null)
                            {
                                descr = j.ToString();
                            }
                            else
                            {
                                descr = null;
                            }

                            j = info.GetValue("publishedDate");
                            string annee;
                            if (j != null)
                            {
                                annee = j.ToString();
                            }
                            else
                            {
                                annee = null;
                            }
                            j = info.GetValue("previewLink");
                            string preview;
                            if (j != null)
                            {
                                preview = j.ToString();
                            }
                            else
                            {
                                preview = null;
                            }
                            j = info.GetValue("publisher");
                            string edit;
                            if (j != null)
                            {
                                edit = j.ToString();
                            }
                            else
                            {
                                edit = null;
                            }
                            j = info.GetValue("canonicalVolumeLink");
                            string url;
                            if (j != null)
                            {
                                url = j.ToString();
                            }
                            else
                            {
                                url = "";
                            }

                            JObject tmp = ((JObject)info["imageLinks"]);
                            if (tmp != null)
                                j = tmp.GetValue("thumbnail");
                            else
                                j = null;
                            string img;
                            if (j != null)
                            {
                                img = j.ToString();
                            }
                            else
                            {
                                img = null;
                            }
                            tmpa = ((JArray)info["categories"]);
                            string cat = null;
                            if (tmpa != null)
                            {
                                j = tmpa.First;
                                if (j != null)
                                {
                                    cat = j.ToString();
                                }
                                else
                                {
                                    cat = null;
                                }
                            }
                            string downloadlink;
                            
                            if(item["accessInfo"]["pdf"]["isAvailable"].ToString().ToLower()=="true")
                            {
                                downloadlink = (string)item["accessInfo"]["pdf"]["acsTokenLink"];
                            }
                            else
                            {
                                downloadlink = null;
                            }

                            Book b = new Book(title, descr, annee, edit, cat, aut, img, url, preview, downloadlink);
                            livres.Add(b);
                        }
                    }
                    Console.WriteLine(livres.Count);
                }
            }
        }
    }

    public class Book
    {
        public string img;
        public string titre;
        public string descr;
        public string annee;
        public string edit;
        public string cat;
        public string aut;
        public string url;
        public string preview;
        public string downloadlink;
        public Book(string titre, string descr, string annee, string edit, string cat, string aut, string img, string url, string preview, string downloadlink)
        {
            this.url = url;
            this.img = img;
            this.titre = titre;
            this.descr = descr;
            this.annee = annee;
            this.edit = edit;
            this.cat = cat;
            this.aut = aut;
            this.preview = preview;
            this.downloadlink = downloadlink;
        }
    }
}