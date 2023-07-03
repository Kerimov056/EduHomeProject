using EduHome.Core.Entities;
using EduHome.UI.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace EduHome.UI.Contollers;

public class SiginUpController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public SiginUpController(UserManager<User> userManager,SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM register)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        User user = new User
        {
            UserName = register.UserName,
            Fullname = string.Concat(register.FirstName, " ", register.LastName),
            Email = register.Email
        };
        IdentityResult result = await _userManager.CreateAsync(user, register.Password);
        if (!result.Succeeded)
        {

            foreach (IdentityError message in result.Errors)
            {
                ModelState.AddModelError("", message.Description);
            }
            return View();
        }
        return RedirectToAction("Index", "Home");
    }

    public IActionResult LogIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(LoginVM login)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }        
        User user = await _userManager.FindByNameAsync(login.UserName);
        if (user is null) 
        {
            ModelState.AddModelError("","Username or password is incorrect");
            return View();
        }
        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, login.IsLoggedIn, true);
        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("","Due to overtrying your account has been blocked for 5 minutes");
                return View();
            }
            ModelState.AddModelError("","Username or password is incorrect");
            return View();  
        }
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
