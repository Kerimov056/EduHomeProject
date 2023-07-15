using EduHome.UI.Areas.Admin.ViewModel;

namespace EduHome.UI.Areas.Admin.Extension;

public static class FileExtension
{
    public static bool FormatFile(this IFormFile formFile,string fileType)
    {
        return formFile.ContentType.Contains(fileType);
    }

    public static bool FormatLength(this IFormFile formFile, int kb)
    {
        return formFile.Length / 1024 <= kb;
    }


    public async static Task<string> CopyFileAsync(this IFormFile formFile,string root,params string[] folders)
    {
        string file_name = Guid.NewGuid().ToString() + formFile.FileName;
        string folder = String.Empty;
        foreach (var item in folders)
        {
            folder = Path.Combine(folder, item);
        }
        string filePath = Path.Combine(folder, file_name);
        string path = Path.Combine(root, filePath);
        using (FileStream fileStream = new FileStream(path, FileMode.Create))
        {
            await formFile.CopyToAsync(fileStream);
        }
        return filePath;
    }

    public static IFormFile ConvertToIFormFile(string filePath)
    {
        byte[] fileBytes = File.ReadAllBytes(filePath);
        var fileName = Path.GetFileName(filePath);

        return new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "file", fileName);
    }

    public static string ConvertFromIFormFile(IFormFile formFile)
    {
        using (var streamReader = new StreamReader(formFile.OpenReadStream()))
        {
            return streamReader.ReadToEnd();
        }
    }



    public static string GetTimeAgo(DateTime dateTime)
    {
        TimeSpan timeSince = DateTime.Now.Subtract(dateTime);

        if (timeSince.TotalMilliseconds < 1)
        {
            return "just now";
        }
        if (timeSince.TotalMinutes < 1)
        {
            return "less than a minute ago";
        }
        if (timeSince.TotalMinutes < 2)
        {
            return "1 minute ago";
        }
        if (timeSince.TotalMinutes < 60)
        {
            return string.Format("{0} minutes ago", timeSince.Minutes);
        }
        if (timeSince.TotalMinutes < 120)
        {
            return "1 hour ago";
        }
        if (timeSince.TotalHours < 24)
        {
            return string.Format("{0} hours ago", timeSince.Hours);
        }
        if (timeSince.TotalDays < 2)
        {
            return "yesterday";
        }
        if (timeSince.TotalDays < 30)
        {
            return string.Format("{0} days ago", timeSince.Days);
        }
        if (timeSince.TotalDays < 60)
        {
            return "1 month ago";
        }
        if (timeSince.TotalDays < 365)
        {
            return string.Format("{0} months ago", Math.Round(timeSince.TotalDays / 30));
        }
        if (timeSince.TotalDays < 730)
        {
            return "1 year ago";
        }
        return string.Format("{0} years ago", Math.Round(timeSince.TotalDays / 365));
    }


}
