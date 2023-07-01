using EduHome.Core.Entities;

namespace EduHome.UI.Areas.Admin.Data.Services;

public interface ISliderServices
{
    Task<IEnumerable<Slider>> GetSliders();
    Task<Slider> CreateAsync(Slider slider);
    Task<Slider> EditAsync(int id, Slider slider);
    Task<Slider> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
