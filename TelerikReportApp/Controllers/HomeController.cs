using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TelerikReportApp.Models;

namespace TelerikReportApp.Controllers;

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

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult HtmlViewer()
    {
        return Redirect("/Sample/Html5ReportViewer.html");
    }
    public IActionResult GraphViewer()
    {
        return Redirect("/GraphSample/Graph.html");
    }

    public IActionResult CrossTab()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "CrossTab", "CrossTab.html");
        var contentType = "text/html";

        if (!System.IO.File.Exists(filePath))
            return NotFound();

        return PhysicalFile(filePath, contentType);
    }


    public IActionResult SubReport()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "SubReport", "SubReport.html");
        var contentType = "text/html";

        if (!System.IO.File.Exists(filePath))
            return NotFound();

        return PhysicalFile(filePath, contentType);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
