using AutoMapper;
using LicensePlate.Core.Commands.Requests;
using LicensePlate.Core.Commands.Responses;
using LicensePlate.Core.Contracts.V1.Requests;
using LicensePlate.Core.Contracts.V1.Responses;

namespace LicensePlate.Core.Infrastructure.Profiles;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<LoginRequestDTO, LoginRequest>().ReverseMap();
        CreateMap<ChangePasswordRequestDTO, ChangePasswordRequest>().ReverseMap();
        CreateMap<RefreshRequestDTO, RefreshRequest>().ReverseMap();
        CreateMap<RegistrationRequestDTO, RegistrationRequest>().ReverseMap();
        CreateMap<RevokeRequestDTO, RevokeRequest>().ReverseMap();
        
        CreateMap<LoginResponseDTO, LoginResponse>().ReverseMap();
        CreateMap<RefreshResponseDTO, RefreshResponse>().ReverseMap();
        CreateMap<RegistrationResponseDTO, RegistrationResponse>().ReverseMap();
    }
}