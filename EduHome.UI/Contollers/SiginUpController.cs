using AutoMapper;
using EduHome.Core.Entities;
using EduHome.Core.Utilities;
using EduHome.UI.Areas.Admin.Data.Services;
using EduHome.UI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Security.Principal;

namespace EduHome.UI.Contollers;

public class SiginUpController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IWebHostEnvironment _env;
    private readonly IEmailService _emailService;

    public SiginUpController(UserManager<User> userManager,SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IEmailService emailService, IWebHostEnvironment env)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _emailService = emailService;
        _env = env;
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
        await _userManager.AddToRoleAsync(user,UserRole.Member);
        return RedirectToAction(nameof(LogIn));
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
        User user = await _userManager.FindByEmailAsync(login.Email);
        if (user is null) 
        {
            ModelState.AddModelError("","Email or password is incorrect");
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
            ModelState.AddModelError("","Email or password is incorrect");
            return View();  
        }
        return RedirectToAction("Index", "Home");
    }

    [Authorize(Roles = "Member,Admin")]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(LogIn));
    }


    #region Create Role
    public async Task CreateRole()
    {
        foreach (var role in Enum.GetValues(typeof(UserRole.Roles)))
        {
            bool isExist = await _roleManager.RoleExistsAsync(role.ToString());
            if (!isExist)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
            }
        }
    }
    #endregion

    public async Task<IActionResult> ConfirmEmail(string token,string email)
    {
        if (token is null || email is null)  return BadRequest(); 
        
        User user = await _userManager.FindByEmailAsync(email);
        if (user is null) return NotFound();

        await _userManager.ConfirmEmailAsync(user,token);
        await _signInManager.SignInAsync(user,false);
        
        return RedirectToAction("Index","Home");
    }
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }

    public IActionResult ForgetPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgetPassword(ForgotPasswordModel forgotPasswordModel)
    {
        if (!ModelState.IsValid) return View(forgotPasswordModel);

        var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
        if (user is null)
        {
            ModelState.AddModelError("Email", "User not found");
            return View();
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var link = Url.Action(nameof(ConfirmEmail), "SiginUp", new { token, email = user.Email }, Request.Scheme, Request.Host.ToString());

        string subject = "Verfiy password reset email";

        string html = string.Empty;
        using (StreamReader reader = new StreamReader("wwwroot/templates/htmlpage.html"))
        {
            html = reader.ReadToEnd();
        }


        html = html.Replace("{{link}}", link);
        html = html.Replace("{{Account}}","Hello");

        _emailService.Send(user.Email, subject,html);

        return RedirectToAction(nameof(ForgotPasswordConfirmation));
    }



    public IActionResult ResetPassword(string token, string email)
    {
        var model = new ResetPasswordModel
        {
            Email = email,
            Token = token
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
    {
        if (!ModelState.IsValid) return View(resetPasswordModel);

        User user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
        if (user == null)
        {
            return View(resetPasswordModel);
        }

        var result = await _userManager.ResetPasswordAsync(user,resetPasswordModel.Token,resetPasswordModel.Password);
        if (!result.Succeeded) 
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("",error.Description);
            }
            return View();
        }
        return RedirectToAction(nameof(ResetPasswordConfirmation));
    }
}
