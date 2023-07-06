using EduHome.UI.HeaderAndFooterService.Interface;
using EduHomeDataAccess.Database;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.HeaderAndFooterService;

public class SettingService : ISettingService
{
    private readonly AppDbContext _context;
    public SettingService(AppDbContext context) 
    {
        _context = context;
    }

    public async Task<Dictionary<string, string>> GetSettingAsync()
    {
        return await _context.Settings.ToDictionaryAsync(s => s.Key, s => s.Value);
    }
}
