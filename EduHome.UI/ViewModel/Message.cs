namespace EduHome.UI.ViewModel;

public class Message
{
    public string[] Recipients { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }

    public Message(string[] recipients, string subject, string body)
    {
        Recipients = recipients;
        Subject = subject;
        Body = body;
    }
}
