using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MasterProject.Models;

namespace MasterProject.Controllers;
[Route("")]
[Route("[controller]")]

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [Route("")]
    [Route("[action]")]

    public IActionResult Index()
    {
        return View();
    }
    [Route("[action]")]

    public IActionResult Privacy()
    {
        return View();
    }
    [Route("[action]")]
    public IActionResult QA()
    {
        return View();
    }
    [Route("[action]")]

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
