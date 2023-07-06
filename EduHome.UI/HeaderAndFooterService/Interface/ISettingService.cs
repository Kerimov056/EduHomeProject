namespace EduHome.UI.HeaderAndFooterService.Interface;

public interface ISettingService
{
    Task<Dictionary<string, string>> GetSettingAsync();
}
