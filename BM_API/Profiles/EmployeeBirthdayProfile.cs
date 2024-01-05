using AutoMapper;
using BM_API.DTOs.Employee;
using BM_API.Models;

namespace BM_API.Profiles
{
    public class EmployeeBirthdayProfile:Profile
    {
        public EmployeeBirthdayProfile()
        {
            CreateMap<Employee,EmployeeBirthdayDTO>()
                .ForMember(dest=>dest.FirstName,
                from => from.MapFrom(x => $"{x.FirstName}"))
                .ForMember(dest => dest.LastName,
                from => from.MapFrom(x => $"{x.LastName}"))
                .ForMember(dest => dest.Birthdate,
                from => from.MapFrom(x => $"{x.BirthDate}")).ReverseMap();
        }
    }
}
