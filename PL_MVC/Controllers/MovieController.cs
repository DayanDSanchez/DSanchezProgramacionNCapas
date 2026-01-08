using Antlr.Runtime;
using ML;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class MovieController : Controller
    {
        string token = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIxMzNhMDNlODU5YmRmYTM0MzVlYTQxYjMxNGYwNWZlYSIsIm5iZiI6MTczNDYyNjk3Ny4wNTQwMDAxLCJzdWIiOiI2NzY0NGVhMTFjMzQ2ZWQxYmQwMDFiZTkiLCJzY29wZXMiOlsiYXBpX3JlYWQiXSwidmVyc2lvbiI6MX0.sWIPKeYNTDAZKxcWCUTm_iXv4hbxLenJ8CQwHtwmKGQ";
        // GET: Movie
        public ActionResult DBMovie()
        {
            ML.Pelicula pelicula = new ML.Pelicula();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var responseTask = client.GetAsync("movie/popular");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Pelicula>();
                    readTask.Wait();

                    var peliculasList = readTask.Result;

                    pelicula = peliculasList;
                }
            }
            return View(pelicula);
        }

        [HttpPost]
        public ActionResult AgregarAFavoritos(int idPelicula)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var payload = new
                    {
                        media_type = "movie",
                        media_id = idPelicula,
                        favorite = true
                    };

                    var jsonPayload = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    var responseTask = client.PostAsync("account/21697509/favorite", content);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Mensaje"] = "Película agregada a favoritos con éxito.";
                    }
                    else
                    {
                        TempData["Error"] = "No se pudo agregar la película a favoritos en TMDb.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Ocurrió un error: {ex.Message}";
            }

            return RedirectToAction("Favoritos");
        }

        [HttpPost]
        public ActionResult EliminarDeFavoritos(int idPelicula)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var content = new StringContent($"{{ \"media_type\": \"movie\", \"media_id\": {idPelicula}, \"favorite\": false }}",
                                                    Encoding.UTF8, "application/json");

                    var responseTask = client.PostAsync("account/21697509/favorite", content);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Success"] = "Película eliminada de favoritos.";
                    }
                    else
                    {
                        TempData["Error"] = "No se pudo eliminar la película de favoritos.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Ocurrió un error: {ex.Message}";
            }

            return RedirectToAction("Favoritos");
        }

        public ActionResult Favoritos()
        {
            ML.Pelicula pelicula = new ML.Pelicula();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var responseTask = client.GetAsync("account/21697509/favorite/movies");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ML.Pelicula>();
                        readTask.Wait();
                        pelicula = readTask.Result;
                    }
                    else
                    {
                        TempData["Error"] = "No se pudieron obtener las películas favoritas.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Ocurrió un error: {ex.Message}";
            }
            return View(pelicula);
        }

    }
}