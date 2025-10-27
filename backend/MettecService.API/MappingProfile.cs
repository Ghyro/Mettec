using AutoMapper;
using MettecService.API.Models;
using MettecService.DataAccess.Entities;

namespace MettecService.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskItem, TaskDto>().ReverseMap();
        CreateMap<CreateTaskRequest, TaskItem>();
        CreateMap<UpdateTaskStatusRequest, TaskItem>()
            .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.IsCompleted));
    }
}