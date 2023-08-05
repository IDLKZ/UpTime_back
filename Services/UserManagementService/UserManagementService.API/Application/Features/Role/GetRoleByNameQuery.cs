using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagementService.API.Application.DTO.ResponseDTO;
using UserManagementService.API.Application.DTO.RoleDTO;
using UserManagementService.API.Application.DTO.UserDTO;

namespace UserManagementService.API.Application.Features.Role
{
    public class GetRoleByNameQuery : IRequest<ResponseRDTO<RoleRDTO>>
    {
        public string Name {get; set; }
        public GetRoleByNameQuery(string name)
        {
            Name = name;
        }
    }

    public class GetRoleByNameQueryHandler : IRequestHandler<GetRoleByNameQuery,ResponseRDTO<RoleRDTO>>
    {

        private RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public GetRoleByNameQueryHandler(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<RoleRDTO>> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IdentityRole role = await roleManager.Roles.Where(p => p.Name.ToLower().Contains(request.Name)).FirstOrDefaultAsync();

                if(role == null)
                {
                    return new ResponseRDTO<RoleRDTO>
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "Role doesnt exist"
                    };
                }
                return new ResponseRDTO<RoleRDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<RoleRDTO>(role)
                };



            }
            catch(Exception ex)
            {
                return new ResponseRDTO<RoleRDTO>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = ex.Message.ToString(),
                    Detail = ex.ToString(),
                };
            }
        }
    }
}
