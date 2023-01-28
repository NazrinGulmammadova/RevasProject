using Microsoft.AspNetCore.Mvc;
using Revas.Core.Entities;
using Revas.DataAccess.Contexts;
using WebApplication1.Areas.RevasAdmin.ViewModels;
using WebApplication1.Utilities;

namespace WebApplication1.Areas.RevasAdmin.Controllers;

[Area("RevasAdmin")]
public class PortfolioController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public PortfolioController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public IActionResult Index()
    {
        return View(_context.Portfolios);
    }
    public async Task<IActionResult> Detail(int id)
    {
        var model = _context.Portfolios.FirstOrDefault(x => x.Id == id);

        if (model == null) return NotFound();


        return View(model);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PortfolioVM portfolioVM)
    {
        if (!ModelState.IsValid) return View(portfolioVM);
        if (portfolioVM.Image == null)
        {
            ModelState.AddModelError("", "Please, select the image");
            return View(portfolioVM);
        }
        if (!portfolioVM.Image.CheckFileSize(200))
        {
            ModelState.AddModelError("", "Please, choose the correct size");
            return View(portfolioVM);
        }
        if (!portfolioVM.Image.CheckFileFormat("img/"))
        {
            ModelState.AddModelError("", "Please, choose the correct format");
            return View(portfolioVM);
        }
        var filename = string.Empty;
        try
        {
            filename = await portfolioVM.Image.CopyToFileAsync(_env.WebRootPath, "assets", "img", "portfolio");
        }
        catch (Exception)
        {
            return View(portfolioVM);
        }
        Portfolio portfolio = new()
        {
            Image = filename
        };
        await _context.Portfolios.AddAsync(portfolio);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Update(int id)
    {
        var model = _context.Portfolios.FirstOrDefault(x => x.Id == id);

        if (model == null) return NotFound();

        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, PortfolioVM portfolioVM)
    {
        if (!ModelState.IsValid) return View(portfolioVM);
        if (portfolioVM.Image == null)
        {
            ModelState.AddModelError("", "Please, select the image");
            return View(portfolioVM);
        }
        if (!portfolioVM.Image.CheckFileSize(200))
        {
            ModelState.AddModelError("", "Please, choose the correct size");
            return View(portfolioVM);
        }
        if (!portfolioVM.Image.CheckFileFormat("img/"))
        {
            ModelState.AddModelError("", "Please, choose the correct format");
            return View(portfolioVM);
        }
        var filename = string.Empty;
        try
        {
            filename = await portfolioVM.Image.CopyToFileAsync(_env.WebRootPath, "assets", "img", "portfolio");
        }
        catch (Exception)
        {
            return View(portfolioVM);
        }
        Portfolio portfolio = new()
        {
            Image = filename
        };
        _context.Update(portfolio);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Delete(int id)
    {
        var model = _context.Portfolios.FirstOrDefault(x => x.Id == id);

        if (model == null) return NotFound();


        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var model = _context.Portfolios.FirstOrDefault(x => x.Id == id);

        if (model == null) return NotFound();

        Helper.DeleteFile(_env.WebRootPath, "assets", "img", "portfolio", model.Image);
        _context.Portfolios.Remove(model);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
