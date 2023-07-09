using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.Data.Exception;
using EduHome.UI.Areas.Admin.Data.Services.Interfaces;
using EduHome.UI.Areas.Admin.Extension;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHomeDataAccess.Database;
using EduHomeDataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduHome.UI.Areas.Admin.Data.Services.Concrets;

public class SliderServices : ISliderServices
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;
    private readonly IEntityBaseRepository<Slider> _entityBaseRepository;
    public SliderServices(AppDbContext context, IWebHostEnvironment env, IMapper mapper, IEntityBaseRepository<Slider> entityBaseRepository)
    {
        _context = context;
        _env = env;
        _mapper = mapper;
        _entityBaseRepository = entityBaseRepository;
    }

    public async Task CreateAsync(SliderViewModel SliderViewModel)
    {
        if (SliderViewModel is null) throw new NullReferenceException("Slider is Null");
        if (!SliderViewModel.image.FormatFile("image"))
        {
            throw new ArgumentException("Select correct image format!");
        }
        if (!SliderViewModel.image.FormatLength(100))
        {
            throw new ArgumentException("Size must be less than 100 kb");
        }

        string filePath = await SliderViewModel.image.CopyFileAsync(_env.WebRootPath, "assets", "img", "slider");

        Slider slider = _mapper.Map<Slider>(SliderViewModel);
        slider.ImagePath = filePath;

        await _entityBaseRepository.AddAsync(slider);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id == 0) throw new NullReferenceException("Slider is Null");
        var slider = await _context.Sliders.FindAsync(id);
        if (slider is null) throw new NotFoundException("Sldier is Null");

        await _entityBaseRepository.DeleteAsync(id);
        await _context.SaveChangesAsync();
    }

    public async Task EditAsync(int id, SliderViewModel SliderViewModel)
    {
        if (SliderViewModel is null) throw new NullReferenceException("Slider is null");

        var slider = await _context.Sliders.FindAsync(id);
        if (slider is null) throw new NullReferenceException("Slider is nUll");

        if (SliderViewModel.image is not null)
        {
            if (!SliderViewModel.image.FormatFile("image"))
            {
                throw new ArgumentException("Select correct image format!");
            }

            if (!SliderViewModel.image.FormatLength(100))
            {
                throw new ArgumentException("Size must be less than 100 kb");
            }

            string filePath = await SliderViewModel.image.CopyFileAsync(_env.WebRootPath, "assets", "img", "slider");
            slider.ImagePath = filePath;
        }
        slider.Information= SliderViewModel.Information;
        slider.Name = SliderViewModel.Name;
        slider.NameTwo = SliderViewModel.NameTwo;

        await _entityBaseRepository.UpdateAsync(id,slider);
        await _context.SaveChangesAsync();
    }

    public async Task<Slider> FindByIdAsync(int id)
    {
        if (id == 0) throw new NullReferenceException("Slider is Null");
        var slider = await _entityBaseRepository.GetByIdAsync(id);
        if (slider is null) throw new NotFoundException("Slider is Null");
        return slider;
    }

    public async Task<IEnumerable<Slider>> GetSliders() => await _entityBaseRepository.GetAllAsync();
}
