using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.ViewModel;

namespace EduHome.UI.Areas.Admin.AutoMapper;

public class MappingProfile:Profile
{
	public MappingProfile()
	{
		CreateMap<Blog, BlogViewModel>();
		CreateMap<BlogViewModel, Blog>();
	}
}
