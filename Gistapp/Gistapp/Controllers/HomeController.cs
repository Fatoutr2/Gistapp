// Contrôleur par défaut qui gère les requêtes pour la page d'accueil
//Gère généralement la page d’accueil et d’autres pages de base.

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gistapp.Models;

namespace Gistapp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewData["HideHeader"] = true; // Cache le header uniquement pour l'accueil
        return View();
    }
    public IActionResult Authentification()
    {
        return View("~/Views/Login/Authentification.cshtml");
    }
    public IActionResult Inscription()
    {
        return View("~/Views/Login/Inscription.cshtml");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
