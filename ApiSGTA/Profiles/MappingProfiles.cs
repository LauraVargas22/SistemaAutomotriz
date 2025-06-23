using AutoMapper;
using Application.DTOs;
using Domain.Entities;

namespace ApiSGTA.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Auditory, AuditoryDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<DetailInspection, DetailInspectionDto>().ReverseMap();
            CreateMap<DetailsDiagnostic, DetailsDiagnosticDto>().ReverseMap();
            CreateMap<Diagnostic, DiagnosticDto>().ReverseMap();
            CreateMap<Inspection, InspectionDto>().ReverseMap();
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();
            CreateMap<Rol, RolDto>().ReverseMap();
            CreateMap<ServiceOrder, ServiceOrderDto>().ReverseMap();
            CreateMap<SparePart, SparePartDto>().ReverseMap();
            CreateMap<Specialization, SpecializationDto>().ReverseMap();
            CreateMap<State, StateDto>().ReverseMap();
            CreateMap<TypeService, TypeServiceDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserRol, UserRolDto>().ReverseMap();
            CreateMap<UserSpecialization, UserSpecializationDto>().ReverseMap();
            CreateMap<Vehicle, VehicleDto>().ReverseMap();
            CreateMap<TypeVehicle, TypeVehicleDto>().ReverseMap();
        }
    }
}