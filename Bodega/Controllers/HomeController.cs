using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Bodega.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bodega.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            string baseUrl = "https://jsonplaceholder.typicode.com/";
            List<User> usuarios = new List<User>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(baseUrl);
                cliente.DefaultRequestHeaders.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var respuesta = await cliente.GetAsync("posts");
                if (respuesta.IsSuccessStatusCode)
                {
                    var dataRespuesta = respuesta.Content.ReadAsStringAsync().Result;
                    usuarios = JsonConvert.DeserializeObject<List<User>>(dataRespuesta);
                }
                return View(usuarios);
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}