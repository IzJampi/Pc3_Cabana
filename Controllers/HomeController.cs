using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Pc3_Cabana.Models;

namespace Pc3_Cabana.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Tareas()
    {
        return View();
    }

    public IActionResult TareasExternas()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
