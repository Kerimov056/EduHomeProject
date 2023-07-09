using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.ViewModel;
using EduHome.UI.ViewModel;

namespace EduHome.UI.Areas.Admin.Data.Services.Interfaces;

public interface ISpkearServices
{
    Task<IEnumerable<Speakers>> GetSpeakers();
    Task CreateAsync(SpeakerViewModel speakerViewModel, int[] SelectedEventIds);
    Task EditAsync(int id, SpeakerViewModel SpeakerViewModel,int EventId);
    Task<Speakers> GetByIdAsync(int id);
    Task DeleteAsync(int id);
    Task<HomeViewModel> Details();
}
