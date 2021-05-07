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
        string baseUrl = "https://jsonplaceholder.typicode.com/";

        public async Task<ActionResult> Index()
        {
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


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                var json = JsonConvert.SerializeObject(user);
                var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var respuesta =  await client.PostAsync("posts", stringContent);

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(user);
        }
    }
}