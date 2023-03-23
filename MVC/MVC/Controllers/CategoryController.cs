using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;

namespace MVC.Controllers
{
    public class CategoryController : Controller
    {
        public const string baseUrl = "http://localhost:49694/";
        private Uri clientBaseAddress = new Uri(baseUrl);
        private HttpClient client;

        public CategoryController()
        {
            client = new HttpClient();
            client.BaseAddress = clientBaseAddress;
        }
        private void HeaderClearing()
        {
            // Clearing default headers
            client.DefaultRequestHeaders.Clear();

            // Define the request type of the data
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Category
        public async Task<ActionResult> Index()
        {
            List<Category> categorys = new List<Category>();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await client.GetAsync("api/Categories");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                categorys = JsonConvert.DeserializeObject<List<Category>>(responseMessage);
            }
            return View(categorys);
        }

        // GET: Category/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Category category = new Category();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await client.GetAsync($"api/Categories/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<Category>(responseMessage);
            }
            return View(category);
        }

        // GET: Category/Create
        public async Task<ActionResult> CreateAsync()
        {
            var model = new Category();
            return View(model);
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                string createCategoryInfo = JsonConvert.SerializeObject(category);
                StringContent stringContentInfo = new StringContent(createCategoryInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage createHttpResponseMessage = client.PostAsync(client.BaseAddress + "api/Categories", stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(category);
        }

        // GET: Category/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Category category = new Category();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await client.GetAsync($"api/Categories/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<Category>(responseMessage);
            }



            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                string createCategoryInfo = JsonConvert.SerializeObject(category);
                StringContent stringContentInfo = new StringContent(createCategoryInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage editHttpResponseMessage = client.PutAsync(client.BaseAddress + $"api/Categories/{id}", stringContentInfo).Result;
                if (editHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            Category categoryInfo = new Category();
            HttpResponseMessage getCategoryHttpResponseMessage = client.GetAsync(client.BaseAddress + $"api/Categories/{id}").Result;
            if (getCategoryHttpResponseMessage.IsSuccessStatusCode)
            {
                categoryInfo = getCategoryHttpResponseMessage.Content.ReadAsAsync<Category>().Result;
            }
            return View(categoryInfo);
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            HttpResponseMessage deleteCategoryHttpResponseMessage = client.DeleteAsync(client.BaseAddress + $"api/Categories/{id}").Result;
            if (deleteCategoryHttpResponseMessage.IsSuccessStatusCode)
            {
                //categoryInfo = getCategoryHttpResponseMessage.Content.ReadAsAsync<Category>().Result;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
