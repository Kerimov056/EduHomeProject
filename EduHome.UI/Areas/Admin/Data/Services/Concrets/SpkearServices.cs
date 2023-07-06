using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Base;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class SpkearServices : ISpkearServices
{
    public SpkearServices(IEntityBaseRepository<Speakers> speakerRepository)
    {
        _speakerRepository = speakerRepository;
    }
    private readonly IEntityBaseRepository<Speakers> _speakerRepository;
    public async Task<IEnumerable<Speakers>> GetSpeakers() => await _speakerRepository.GetAllAsync();
    public async Task<Speakers> GetByIdAsync(int id) => await _speakerRepository.GetByIdAsync(id);
    public Task DeleteAsync(int id) => _speakerRepository.DeleteAsync(id);
    public async Task<Speakers> CreateAsync(Speakers speakers)
    {
        await _speakerRepository.AddAsync(speakers);
        return speakers;
    }
    public async Task<Speakers> Edit(int id, Speakers speakers)
    {
        await _speakerRepository.UpdateAsync(id, speakers);
        return speakers;
    }
}
