namespace EduHome.UI.Areas.Admin.Data.Services;

public interface IEmailService
{
    void Send(string to , string subject, string html, string form = null);
}
