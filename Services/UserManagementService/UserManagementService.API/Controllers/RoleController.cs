using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserManagementService.API.Application.DTO.ResponseDTO;
using UserManagementService.API.Application.DTO.RoleDTO;
using UserManagementService.API.Application.DTO.UserDTO;
using UserManagementService.API.Application.Features.Role;
using UserManagementService.API.Application.Features.User;
using UserManagementService.API.Application.Parameters.UserParameters;
using UserManagementService.API.Helpers;

namespace UserManagementService.API.Controllers
{
    public class RoleController : BaseApiController
    {

        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<IEnumerable<RoleRDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<IEnumerable<RoleRDTO>>>> GetAll()
        {
            ResponseRDTO<IEnumerable<RoleRDTO>> result = await _mediator.Send(new GetRoleListQuery());
            return StatusCode(result.StatusCode, result);
        }
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<RoleRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<RoleRDTO>>> GetByName([FromQuery] string name)
        {
            ResponseRDTO<RoleRDTO> result = await _mediator.Send(new GetRoleByNameQuery(name));
            return StatusCode(result.StatusCode, result);
        }
    }
}
