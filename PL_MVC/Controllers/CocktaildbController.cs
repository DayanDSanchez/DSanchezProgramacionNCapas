using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class CocktaildbController : Controller
    {
        // GET: Cocktaildb
        public ActionResult Cocktail(string name)
        {
            ML.Cocktail cocktail = new ML.Cocktail();

            if (!string.IsNullOrEmpty(name))
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v1/1/");
                    var responseTask = client.GetAsync($"search.php?s={name}");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ML.Cocktail>();
                        readTask.Wait();
                        cocktail = readTask.Result;
                    }
                }
            }

            return View(cocktail);
        }

        public ActionResult CocktailDetails(int? IdConcktail)
        {
            ML.Cocktail cocktail = new ML.Cocktail();

            if(IdConcktail > 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v1/1/");
                    var responseTask = client.GetAsync($"lookup.php?i={IdConcktail}");
                    responseTask.Wait();
                     
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ML.Cocktail>();
                        readTask.Wait();
                        cocktail = readTask.Result;
                    }
                }
            }

            return View(cocktail);
        }
    }
}