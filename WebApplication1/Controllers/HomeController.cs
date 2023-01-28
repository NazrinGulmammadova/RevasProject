using Microsoft.AspNetCore.Mvc;
using Revas.DataAccess.Contexts;
using System.Diagnostics;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
	private readonly AppDbContext _context;

	public HomeController(AppDbContext context)
	{
		_context = context;
	}

	public IActionResult Index()
	{
		return View(_context.Portfolios);
	}
}