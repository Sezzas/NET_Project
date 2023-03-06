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

    public IActionResult Index()
    {
        return View();
    }

    [Route("/omsso")]
    public IActionResult About()
    {
        return View();
    }

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

    [HttpPost("/anteckningar")]
    public IActionResult Notes(Note note)
    {
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
    }

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

    [HttpPost]
    public IActionResult Edit(Note note)
    {
        using (var client = new HttpClient()) {
            client.BaseAddress = new Uri("http://localhost:5154/api/note");

            var putTask = client.PutAsJsonAsync<Note>("note", note);
            putTask.Wait();

            var result = putTask.Result;
            if(result.IsSuccessStatusCode) {
                return RedirectToAction("Notes");
            }
        }

        return View(note);
    }

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
