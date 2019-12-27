using AutoMapper;
using RetrospectiveApi.Entities;
using RetrospectiveApi.Models;
using RetrospectiveApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrospectiveApi.MappingProfile
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<RetropsectiveProject, ProjectDto>();
            CreateMap<ProjectDto,RetropsectiveProject>();
            CreateMap<Feedback, FeedbackDto>();
            CreateMap<FeedbackDto, Feedback>();
            CreateMap<RetrospectiveWithoutFeedback, ProjectDto>();
            CreateMap<ProjectDto, RetrospectiveWithoutFeedback>();
            CreateMap<ProjectDto, RetrospectiveViewModel>();
            CreateMap<RetrospectiveViewModel, ProjectDto>();
            CreateMap<FeedbackViewModel, FeedbackDto>();
            CreateMap<FeedbackDto, FeedbackViewModel>();
        }
    }
}
