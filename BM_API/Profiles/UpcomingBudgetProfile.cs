using AutoMapper;
using BM_API.DTOs.Budget;
using BM_API.Models;

namespace BM_API.Profiles
{
    public class UpcomingBudgetProfile:Profile
    {
        public UpcomingBudgetProfile() 
        {
            CreateMap<Budget,UpcomingBudgetDTO>()
                .ForMember(dest=>dest.Name,
                from=> from.MapFrom(x => $"{x.Name}"))
                .ForMember(dest=>dest.Date,
                from=>from.MapFrom(x=>$"{x.Date}"))
                .ForMember(dest=>dest.PaymentTypeName,
                from => from.MapFrom(x => $"{x.PaymentTypeName}"))
                .ForMember(dest => dest.Value,
                from => from.MapFrom(x => $"{x.Value}")).ReverseMap();
        }
    }
}
