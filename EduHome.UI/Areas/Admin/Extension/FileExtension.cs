using EduHome.UI.Areas.Admin.ViewModel;

namespace EduHome.UI.Areas.Admin.Extension;

public static class FileExtension
{
    public static bool FormatFile(this IFormFile formFile,string fileType)
    {
        return formFile.ContentType.Contains(fileType);
    }

    public static bool FormatLenght(this IFormFile formFile,int kb)
    {
        return formFile.Length/1024 <= kb;
    }

    public async static Task<string> CopyFileAsync(this IFormFile formFile,string root,params string[] folders)
    {
        string file_name = DateTime.Now.ToString("dd MMM yyyy") + formFile.FileName;
        string folder = String.Empty;
        foreach (var item in folders)
        {
            folder = Path.Combine(folder, item);
        }
        string filePath = Path.Combine(folder, file_name);
        string path = Path.Combine(root, filePath);
        using (FileStream fileStream = new FileStream(path, FileMode.CreateNew))
        {
            await formFile.CopyToAsync(fileStream);
        }
        return filePath;
    }
}
