﻿using AutoMapper;
using EduHome.Core.Entities;
using EduHome.UI.Areas.Admin.ViewModel;

namespace EduHome.UI.Areas.Admin.AutoMapper;

public class MappingProfile:Profile
{
	public MappingProfile()
	{	
		//blog
		CreateMap<Blog, BlogViewModel>();
		CreateMap<BlogViewModel, Blog>();

		//course
		CreateMap<Courses, CourseFullDetailsViewModel>();
		CreateMap<CoursesDetails, CourseFullDetailsViewModel>();
		CreateMap<CourseFullDetailsViewModel, Courses>();
		CreateMap<CourseFullDetailsViewModel, CoursesDetails>();

		//Events
		CreateMap<Events, EventsViewModel>();
		CreateMap<EventsViewModel, Events>();

		//Spkears
		CreateMap<Speakers, SpeakerViewModel>();
		CreateMap<SpeakerViewModel, Speakers>();

        //slider
        CreateMap<Slider, SliderViewModel>();
        CreateMap<SliderViewModel, Slider>();

		//Notice
		CreateMap<Notice, NoticeViewModel>();
		CreateMap<NoticeViewModel, Notice>();

		//Info
		CreateMap<Info, InfoViewModel>();
		CreateMap<InfoViewModel, Info>();

		//Teaceher
		//CreateMap<Teacher, TeacherViewModel>().ForMember(dest => dest., opt => opt.MapFrom(src=>src.teacherDetails));
    }
}
