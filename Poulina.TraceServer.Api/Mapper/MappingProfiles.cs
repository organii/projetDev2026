using AutoMapper;
using System.Linq;
using AgileAi.Domain.Dto;
using AgileAi.Domain.Models;
using static AgileAi.Domain.Dto.ExecutionDto;
using static AgileAi.Domain.Dto.PlanningDto;

namespace AgileAi.Api.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Project Mappings
            CreateMap<Project, ProjectDto>().ReverseMap();

            // Sprint Mappings
            CreateMap<Sprint, SprintDto>().ReverseMap();

            // User Story Mappings
            CreateMap<UserStory, UserStoryDto>().ReverseMap();

            // Issue & Kanban Mappings
            

            // Staff / User Mappings
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<ProjectMember, ProjectMemberDto>().ReverseMap();

            // Collaboration
            CreateMap<Comment, CommentDto>().ReverseMap();

        }
    }
}