using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.RevasAdmin.Controllers;

[Area("RevasAdmin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
