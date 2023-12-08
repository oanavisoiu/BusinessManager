using AutoMapper;
using BM_API.DTOs.EmployeeUpdateDto;
using BM_API.DTOs.Supplier;
using BM_API.Models;

namespace BM_API.Profiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<SupplierDTO, Supplier>()
                .ForMember(
                dest => dest.Name,
                from => from.MapFrom(x => $"{x.Name}"))
            .ForMember(
                dest => dest.Address,
                from => from.MapFrom(x => $"{x.Address}"))
            .ForMember(
                dest => dest.City,
                from => from.MapFrom(x => x.City))
            .ForMember(
                dest => dest.PhoneNumber,
                from => from.MapFrom(x => $"{x.PhoneNumber}"))
            .ForMember(
                dest => dest.Email,
                from => from.MapFrom(x => $"{x.Email}"))
            .ForMember(
                dest => dest.Link,
                from => from.MapFrom(x => $"{x.Link}")).ReverseMap();
        }
    }
}
