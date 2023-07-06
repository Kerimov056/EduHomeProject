using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Base;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class SliderServices : ISliderServices
{
    private readonly IEntityBaseRepository<Slider> _sliderRepository;

    public SliderServices(IEntityBaseRepository<Slider> sliderRepository)
    {
        _sliderRepository = sliderRepository;
    }
    public Task DeleteAsync(int id) => _sliderRepository.DeleteAsync(id);
    public async Task<Slider> GetByIdAsync(int id) => await _sliderRepository.GetByIdAsync(id);
    public async Task<IEnumerable<Slider>> GetSliders() => await _sliderRepository.GetAllAsync();
    public async Task<Slider> CreateAsync(Slider slider)
    {
        await _sliderRepository.AddAsync(slider);
        return slider;
    }
    public async Task<Slider> EditAsync(int id, Slider slider)
    {
        await _sliderRepository.UpdateAsync(id, slider);
        return slider;
    }
}
