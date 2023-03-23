using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Models;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    public class JobController : Controller
    {
        public const string baseUrl = "http://localhost:5000/";
        private Uri clientBaseAddress = new Uri(baseUrl);
        private HttpClient client;

        public JobController()
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

        // GET: Job
        public async Task<ActionResult> Index()
        {
            List<Job> jobs = new List<Job>();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await client.GetAsync("api/Jobs");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                jobs = JsonConvert.DeserializeObject<List<Job>>(responseMessage);
            }
            return View(jobs);
        }

        // GET: Job/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Job job = new Job();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await client.GetAsync($"api/Jobs/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                job = JsonConvert.DeserializeObject<Job>(responseMessage);
            }
            return View(job);
        }

        // GET: Job/Create
        public async Task<ActionResult> CreateAsync()
        {
            List<Category> categories = new List<Category>();
            HeaderClearing();
            HttpResponseMessage httpResponseMessage = await client.GetAsync("api/Categories");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                categories = JsonConvert.DeserializeObject<List<Category>>(responseMessage);
            }

            var viewModel = new JobCategory
            {
                Job = new Job(),
                Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "ID", "Name")
            };
            return View(viewModel);
        }

        // POST: Job/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Job job)
        {
            job.Category = new Category { Id = job.JobCategoryId };
            if (ModelState.IsValid)
            {
                string createJobInfo = JsonConvert.SerializeObject(job);
                StringContent stringContentInfo = new StringContent(createJobInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage createHttpResponseMessage = client.PostAsync(client.BaseAddress + "api/Jobs", stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(job);
        }

        // GET: Job/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Job job = new Job();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await client.GetAsync($"api/Jobs/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                job = JsonConvert.DeserializeObject<Job>(responseMessage);
            }

            List<Category> categories = new List<Category>();
            httpResponseMessage = await client.GetAsync("api/Categories");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                categories = JsonConvert.DeserializeObject<List<Category>>(responseMessage);
            }
            var viewModel = new JobCategory
            {
                Job = job,
                Categories = new SelectList(categories, "ID", "Name", job.JobCategoryId)
            };
            return View(viewModel);
        }

        // POST: Job/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, JobCategory jobCategoryModel)
        {
            jobCategoryModel.Job.Category = new Category { Id = jobCategoryModel.Job.JobCategoryId };
            if (ModelState.IsValid)
            {
                string createJobInfo = JsonConvert.SerializeObject(jobCategoryModel.Job);
                StringContent stringContentInfo = new StringContent(createJobInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage editHttpResponseMessage = client.PutAsync(client.BaseAddress + $"api/Jobs/{id}", stringContentInfo).Result;
                if (editHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(jobCategoryModel);
        }

        // GET: Job/Delete/5
        public ActionResult Delete(int id)
        {
            Job jobInfo = new Job();
            HttpResponseMessage getJobHttpResponseMessage = client.GetAsync(client.BaseAddress + $"api/Jobs/{id}").Result;
            if (getJobHttpResponseMessage.IsSuccessStatusCode)
            {
                jobInfo = getJobHttpResponseMessage.Content.ReadAsAsync<Job>().Result;
            }
            return View(jobInfo);
        }

        // POST: Job/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Job job)
        {
            HttpResponseMessage deleteJobHttpResponseMessage = client.DeleteAsync(client.BaseAddress + $"api/Jobs/{id}").Result;
            if (deleteJobHttpResponseMessage.IsSuccessStatusCode)
            {
                //jobInfo = getJobHttpResponseMessage.Content.ReadAsAsync<Job>().Result;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
