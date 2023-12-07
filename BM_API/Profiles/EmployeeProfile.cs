using AutoMapper;
using BM_API.DTOs.EmployeeUpdateDto;
using BM_API.Models;

namespace BM_API.Profiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeUpdateDTO, Employee>()
                .ForMember(
                dest => dest.FirstName,
                from => from.MapFrom(x => $"{x.FirstName}"))
            .ForMember(
                dest => dest.LastName,
                from => from.MapFrom(x => $"{x.LastName}"))
            .ForMember(
                dest => dest.BirthDate,
                from => from.MapFrom(x => x.BirthDate))
            .ForMember(
                dest => dest.PhoneNumber,
                from => from.MapFrom(x => $"{x.PhoneNumber}"))
            .ForMember(
                dest => dest.Address,
                from => from.MapFrom(x => $"{x.Address}"))
            .ForMember(
                dest => dest.City,
                from => from.MapFrom(x => $"{x.City}"))
            .ForMember(
                dest => dest.StartDate,
                from => from.MapFrom(x => x.StartDate))
            .ForMember(
                dest => dest.EndDate,
                from => from.MapFrom(x => x.EndDate))
            .ForMember(
                dest => dest.Department,
                from => from.MapFrom(x => $"{x.Department}"))
            .ForMember(
                dest => dest.Salary,
                from => from.MapFrom(x => $"{x.Salary}")).ReverseMap();

        }
    }
}
