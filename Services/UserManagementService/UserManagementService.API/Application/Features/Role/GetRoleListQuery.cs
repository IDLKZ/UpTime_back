using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagementService.API.Application.DTO.ResponseDTO;
using UserManagementService.API.Application.DTO.RoleDTO;

namespace UserManagementService.API.Application.Features.Role
{
    public class GetRoleListQuery : IRequest<ResponseRDTO<IEnumerable<RoleRDTO>>>
    {


    }



    public class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery,ResponseRDTO<IEnumerable<RoleRDTO>>>
    {
        private RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public GetRoleListQueryHandler(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<IEnumerable<RoleRDTO>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<IdentityRole> roles = await roleManager.Roles.ToListAsync();
                return new ResponseRDTO<IEnumerable<RoleRDTO>>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<IEnumerable<RoleRDTO>>(roles)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<IEnumerable<RoleRDTO>>
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
