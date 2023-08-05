using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserManagementService.API.Application.DTO.ResponseDTO;
using UserManagementService.API.Application.DTO.RoleDTO;
using UserManagementService.API.Application.DTO.UserDTO;
using UserManagementService.API.Domain.Models;

namespace UserManagementService.API.Application.DTO
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<ApplicationUser, UserRDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserCDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserUDTO>().ReverseMap();
            CreateMap<IdentityRole, RoleRDTO>().ReverseMap();
            CreateMap<Pagination<ApplicationUser>, Pagination<UserRDTO>>().ReverseMap();
        }


    }
}
