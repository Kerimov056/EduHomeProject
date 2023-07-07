using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.ViewModel;

namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface ISliderServices
{
    Task<IEnumerable<Slider>> GetSliders();
    Task CreateAsync(SliderViewModel SliderViewModel);
    Task EditAsync(int id, SliderViewModel SliderViewModel);
    Task<Slider> FindByIdAsync(int id);
    Task DeleteAsync(int id);
}
