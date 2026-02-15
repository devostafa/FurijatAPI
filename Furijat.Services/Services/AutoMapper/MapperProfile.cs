using AutoMapper;
using Furijat.Data.Data.DTOs;
using Furijat.Data.Data.DTOs.RequestDTO;
using Furijat.Data.Data.DTOs.ResponseDTO;
using Furijat.Data.Data.Models;
using Furijat.Services.Services.JWT.DTO;

namespace Furijat.Services.Services.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        //model to DTO
        CreateMap<User,UserDTO>();
        CreateMap<Project,ProjectResponseDTO>();
        CreateMap<Donation, DonationResponseDTO>();
        CreateMap<News,NewsResponseDTO>();
        CreateMap<User,JWTRequestDTO>();
        CreateMap<RegisterRequestDTO, UserDTO>();
        CreateMap<Category,CategoryResponseDTO>();

        //DTO to Model
        CreateMap<ProjectRequestDTO, Project>();
        CreateMap<DonationRequestDTO, Donation>();
        CreateMap<DonationResponseDTO, Donation >();
        CreateMap<UserDTO,User>();
        
        //DTO to DTO
        CreateMap<UserDTO, JWTRequestDTO>();
    }
}