using AutoMapper;
using Furijat.Data.DTOs;
using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Models;
using Furijat.Services.Jwt.DTO;
using ProjectResponseDTO = Furijat.Data.DTOs.ResponseDTO.ProjectResponseDTO;

namespace Furijat.Services.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        //model to DTO
        CreateMap<User, UserResponseDTO>();
        CreateMap<Project, ProjectResponseDTO>();
        CreateMap<Data.Models.Donation, DonationResponseDTO>();
        CreateMap<BlogArticle, BlogArticleResponseDTO>();
        CreateMap<User, JWTRequestDTO>();
        CreateMap<RegisterRequestDTO, UserResponseDTO>();
        CreateMap<Category, CategoryResponseDTO>();

        //DTO to Model
        CreateMap<ProjectRequestDTO, Project>();
        CreateMap<DonationRequestDTO, Data.Models.Donation>();
        CreateMap<DonationResponseDTO, Data.Models.Donation>();
        CreateMap<UserResponseDTO, User>();

        //DTO to DTO
        CreateMap<UserResponseDTO, JWTRequestDTO>();
    }
}