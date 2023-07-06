namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface IEmailService
{
    void Send(string to, string subject, string html, string form = null);
}
