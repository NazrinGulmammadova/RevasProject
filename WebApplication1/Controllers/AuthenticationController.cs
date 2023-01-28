using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Revas.Core.Entities;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers;

public class AuthenticationController : Controller
{
    private readonly UserManager<UserSide> _userManager;
    private readonly SignInManager<UserSide> _signInManager;

    public AuthenticationController(SignInManager<UserSide> signInManager, UserManager<UserSide> userManager = null)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {
        if (!ModelState.IsValid) return View(registerVM);
        UserSide user = new()
        {
            UserName = registerVM.Username,
            Email = registerVM.Email,
            IsActive = true
        };
        var identityResult = await _userManager.CreateAsync(user, registerVM.Password);
        if (!identityResult.Succeeded)
        {
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(registerVM);
        }
        await _userManager.AddToRoleAsync(user, "member");
        return View(nameof(Login));
    }

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if (!ModelState.IsValid) return View(loginVM);
        var user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
        if (user == null)
        {
            user = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
            if (user == null)
            {
                ModelState.AddModelError("", "Username/Email or password is invalid");
                return View(loginVM);
            }
        }
        var signInResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);
        if (signInResult.IsLockedOut)
        {
            ModelState.AddModelError("", "Password is incorrect, please wait");
            return View(loginVM);
        }
        if (!signInResult.Succeeded)
        {
            ModelState.AddModelError("", "Username/Email or password is invalid");
            return View(loginVM);
        }
        if (!user.IsActive)
        {
            ModelState.AddModelError("", "Not found ");
            return View(loginVM);
        }
        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> Logout()
    {
        if (User.Identity.IsAuthenticated)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        return BadRequest();
    }
    public IActionResult ForgotPassword()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordVM)
    {
        if (!ModelState.IsValid) return View(forgotPasswordVM);
        var user = await _userManager.FindByEmailAsync(forgotPasswordVM.Email);
        {
            if (user is null)
            {
                ModelState.AddModelError("Email", "Email not found");
                return View(forgotPasswordVM);
            }
        }

        return RedirectToAction("Index", "Home");
    }
}
