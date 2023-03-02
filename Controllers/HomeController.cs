using System.Diagnostics;
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

        return View(notes);
    }

    [HttpPost("/anteckningar")]
    public IActionResult Notes(Horse model)
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
