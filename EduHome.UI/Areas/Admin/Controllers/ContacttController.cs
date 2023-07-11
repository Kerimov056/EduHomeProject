using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.ViewModel;
using EduHomeDataAccess.Database;
using EduHomeDataAccess.Migrations;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace EduHome.UI.Areas.Admin.Controllers;
[Area("Admin")]
public class ContacttController : Controller
{
    private readonly IContactService _contactService;
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    public ContacttController(
        IContactService contactService,
        AppDbContext context,
        UserManager<User> userManager)
    {
        _contactService = contactService;
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        HomeViewModel homeViewModel = new()
        {
            viewers = await _contactService.GetViewer()
        };
        return View(homeViewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        if (!ModelState.IsValid) return NotFound();
        var cont = await _contactService.FindByIdAsync(id);
        if (cont == null) return NotFound();
        ViewBag.Cont = cont.Id;
        HomeViewModel homeViewModel = new()
        {
            viewers= await _contactService.GetViewer()
        };
        return View(homeViewModel);
    }

    public async Task<IActionResult> Response(int id)
    {
        var cont = await _contactService.FindByIdAsync(id);
        if (cont == null) return NotFound();
        ViewerViewModel viewerViewModel = new()
        {
            Name = cont.Name,
            Email = cont.Email,
            Subject = cont.Subject,
            Message = cont.Message
        };
        return View(viewerViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Response(ViewerViewModel viewerViewModel, string email, string message)
    {
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress("ulvisk@code.edu.com"); // Admin e-posta adresinizi buraya yazın
        mail.To.Add(viewerViewModel.Email);
        mail.Subject = "Cevap"; // Başlık olarak sabit bir değer kullanabilirsiniz
        mail.Body = message;

        using (SmtpClient smtpClient = new SmtpClient("smtp.mailserver.com", 587)) // SMTP sunucusu bilgilerini buraya yazın
        {
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("smtp_username", "smtp_password"); // SMTP kimlik bilgilerinizi buraya yazın
            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(mail);
        }

        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        var cont = await _contactService.FindByIdAsync(id);
        if (cont == null) return NotFound();
        ViewBag.ContDelete = cont.Id;
        HomeViewModel homeViewModel = new()
        {
            viewers = await _contactService.GetViewer()
        };
        return View(homeViewModel);
    }

    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        await _contactService.DeleteAsync(id);
        return RedirectToAction("Index");
    }

}




