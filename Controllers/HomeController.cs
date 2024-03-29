﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NET_Project.Models;

namespace NET_Project.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // INDEX
    public IActionResult Index()
    {
        IEnumerable<Horse> horses = null;
        IEnumerable<News> news = null;

        // Get latest horses (GET)
        using (var client = new HttpClient()) {

            client.BaseAddress = new Uri("http://localhost:5154/api/");
            var responseTask = client.GetAsync("horse");
            responseTask.Wait();

            var result = responseTask.Result;
            if(result.IsSuccessStatusCode) {
                var readTask = result.Content.ReadAsAsync<IList<Horse>>();

                horses = readTask.Result.Reverse();
            } else {
                horses = Enumerable.Empty<Horse>();

                ModelState.AddModelError(string.Empty, "Something went wrong.");
            }
        }

        // Get latest news (GET)
        using (var client = new HttpClient()) {

            client.BaseAddress = new Uri("http://localhost:5154/api/");
            var responseTask = client.GetAsync("news");
            responseTask.Wait();

            var result = responseTask.Result;
            if(result.IsSuccessStatusCode) {
                var readTask = result.Content.ReadAsAsync<IList<News>>();

                news = readTask.Result.Reverse();
            } else {
                news = Enumerable.Empty<News>();

                ModelState.AddModelError(string.Empty, "Something went wrong.");
            }
        }

        ViewBag.Horses = horses;
        ViewBag.News = news;

        return View();
    }

    [Route("/omsso")]
    public IActionResult About()
    {
        return View();
    }

    // Get horses (GET)
    [Route("/dinahästar")]
    public IActionResult Horses()
    {
        IEnumerable<Horse> horses = null;

        using (var client = new HttpClient()) {

            client.BaseAddress = new Uri("http://localhost:5154/api/");
            var responseTask = client.GetAsync("horse");
            responseTask.Wait();

            var result = responseTask.Result;
            if(result.IsSuccessStatusCode) {
                var readTask = result.Content.ReadAsAsync<IList<Horse>>();

                horses = readTask.Result;
            } else {
                horses = Enumerable.Empty<Horse>();

                ModelState.AddModelError(string.Empty, "Something went wrong.");
            }
        }

        ViewBag.Horses = horses;

        return View();
    }

    // Save new horse (POST)
    [HttpPost("/dinahästar")]
    public IActionResult Horses(Horse horse)
    {
        if (ModelState.IsValid) {

            using (var client = new HttpClient()) {
            client.BaseAddress = new Uri("http://localhost:5154/api/horse");

            var postTask = client.PostAsJsonAsync<Horse>("horse", horse);
            postTask.Wait();

            var result = postTask.Result;

            if(result.IsSuccessStatusCode) {
                return RedirectToAction("Horses");
            }

            ModelState.AddModelError(string.Empty, "Something went wrong.");

            }

            return View(horse);

        } else {
            IEnumerable<Horse> horses = null;

            using (var client = new HttpClient()) {

                client.BaseAddress = new Uri("http://localhost:5154/api/");
                var responseTask = client.GetAsync("horse");
                responseTask.Wait();

                var result = responseTask.Result;
                if(result.IsSuccessStatusCode) {
                    var readTask = result.Content.ReadAsAsync<IList<Horse>>();

                    horses = readTask.Result;
                } else {
                    horses = Enumerable.Empty<Horse>();

                    ModelState.AddModelError(string.Empty, "Something went wrong.");
                }
            }

        ViewBag.Horses = horses;

        return View();

        }

    }

    // Get notes (GET)
    [Route("/anteckningar")]
    public IActionResult Notes()
    {
        IEnumerable<Note> notes = null;

        using (var client = new HttpClient()) {

            client.BaseAddress = new Uri("http://localhost:5154/api/");
            var responseTask = client.GetAsync("note");
            responseTask.Wait();

            var result = responseTask.Result;
            if(result.IsSuccessStatusCode) {
                var readTask = result.Content.ReadAsAsync<IList<Note>>();

                notes = readTask.Result;
            } else {
                notes = Enumerable.Empty<Note>();

                ModelState.AddModelError(string.Empty, "Something went wrong.");
            }
        }

        ViewBag.Notes = notes;

        return View();
    }

    // Save new note (POST)
    [HttpPost("/anteckningar")]
    public IActionResult Notes(Note note)
    {
        if (ModelState.IsValid) { // If fields are correct
            using (var client = new HttpClient()) {
            client.BaseAddress = new Uri("http://localhost:5154/api/note");

            var postTask = client.PostAsJsonAsync<Note>("note", note);
            postTask.Wait();

            var result = postTask.Result;
            if(result.IsSuccessStatusCode) {
                return RedirectToAction("Notes");
            }

            ModelState.AddModelError(string.Empty, "Something went wrong.");

            return View(note);

            }

        } else { // GET if fields are not correct
            IEnumerable<Note> notes = null;

            using (var client = new HttpClient()) {

                client.BaseAddress = new Uri("http://localhost:5154/api/");
                var responseTask = client.GetAsync("note");
                responseTask.Wait();

                var result = responseTask.Result;
                if(result.IsSuccessStatusCode) {
                    var readTask = result.Content.ReadAsAsync<IList<Note>>();

                    notes = readTask.Result;
                } else {
                    notes = Enumerable.Empty<Note>();

                    ModelState.AddModelError(string.Empty, "Something went wrong.");
                }
            }

            ViewBag.Notes = notes;

        return View();

        }
    }

    // Edit note (GET with ID)
    public IActionResult Edit(int id)
    {
        Note note = null;

        using (var client = new HttpClient()) {
            client.BaseAddress = new Uri("http://localhost:5154/api/note/");

            var responseTask = client.GetAsync(id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;

            if(result.IsSuccessStatusCode) {
                var readTask = result.Content.ReadAsAsync<Note>();
                readTask.Wait();

                note = readTask.Result;
            }
        }

        return View(note);
    }

    // Save edited note (PUT)
    [HttpPost]
    public IActionResult Edit(Note note)
    {
        using (var client = new HttpClient()) {
            client.BaseAddress = new Uri("http://localhost:5154/api/");

            var putTask = client.PutAsJsonAsync<Note>("note/" + note.NoteId, note);
            putTask.Wait();

            var result = putTask.Result;
            if(result.IsSuccessStatusCode) {
                return RedirectToAction("Notes");
            }
        }

        return View(note);
    }

    // Edit horse (GET with ID)
    public IActionResult HorseEdit(int id)
    {
        Horse horse = null;

        using (var client = new HttpClient()) {
            client.BaseAddress = new Uri("http://localhost:5154/api/horse/");

            var responseTask = client.GetAsync(id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;

            if(result.IsSuccessStatusCode) {
                var readTask = result.Content.ReadAsAsync<Horse>();
                readTask.Wait();

                horse = readTask.Result;
            }
        }

        return View(horse);
    }

    // Save edited horse (PUT)
    [HttpPost]
    public IActionResult HorseEdit(Horse horse)
    {
        using (var client = new HttpClient()) {
            client.BaseAddress = new Uri("http://localhost:5154/api/");

            var putTask = client.PutAsJsonAsync<Horse>("horse/" + horse.HorseId, horse);
            putTask.Wait();

            var result = putTask.Result;
            if(result.IsSuccessStatusCode) {
                return RedirectToAction("Horses");
            }
        }

        return View(horse);
    }

    // Delete
    public IActionResult Delete(int id, string category)
    {
        using (var client = new HttpClient()) {
            client.BaseAddress = new Uri("http://localhost:5154/api/");

            var deleteTask = client.DeleteAsync(category + "/" + id.ToString());
            deleteTask.Wait();

            var result = deleteTask.Result;
            if(result.IsSuccessStatusCode) {
                return RedirectToAction(category + "s");
            }
        }

        return RedirectToAction(category + "s");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
