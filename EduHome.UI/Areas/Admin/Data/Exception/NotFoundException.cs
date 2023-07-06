namespace EduHome.UI.Areas.Admin.Data.Exception;

public class NotFoundException : IOException
{
    public NotFoundException(string msg) : base(msg)
    {

    }
}