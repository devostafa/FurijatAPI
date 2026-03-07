using AutoMapper;
using Furijat.Data.DTOs;
using Furijat.Data.DTOs.RequestDTO;
using Furijat.Data.DTOs.ResponseDTO;
using Furijat.Data.Models;
using Furijat.Services.JWT.DTO;

namespace Furijat.Services.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        //model to DTO
        CreateMap<User, UserDTO>();
        CreateMap<Project, ProjectResponseDTO>();
        CreateMap<Donation, DonationResponseDTO>();
        CreateMap<BlogArticle, BlogArticleResponseDTO>();
        CreateMap<User, JWTRequestDTO>();
        CreateMap<RegisterRequestDTO, UserDTO>();
        CreateMap<Category, CategoryResponseDTO>();

        //DTO to Model
        CreateMap<ProjectRequestDTO, Project>();
        CreateMap<DonationRequestDTO, Donation>();
        CreateMap<DonationResponseDTO, Donation>();
        CreateMap<UserDTO, User>();

        //DTO to DTO
        CreateMap<UserDTO, JWTRequestDTO>();
    }
}