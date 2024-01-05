using AutoMapper;
using BM_API.DTOs.ToDo;
using BM_API.Models;

namespace BM_API.Profiles
{
    public class ToDoProfile:Profile
    {
        public ToDoProfile()
        {
            CreateMap<ToDo,ToDoDTO>()
                .ForMember(dest=>dest.Text,
                from=>from.MapFrom(x => $"{x.Text}"))
                .ForMember(dest=>dest.Description,
                from=> from.MapFrom(x => $"{x.Description}"))
                .ForMember(dest => dest.RecurrenceRule,
                from => from.MapFrom(x => $"{x.RecurrenceRule}"))
                .ForMember(dest=>dest.StartDate,
                from=> from.MapFrom(x => $"{x.StartDate}"))
                .ForMember(dest=>dest.EndDate,
                from=> from.MapFrom(x => $"{x.EndDate}"))
                .ForMember(dest => dest.AllDay,
                from => from.MapFrom(x => $"{x.AllDay}")).ReverseMap();
        }
    }
}
