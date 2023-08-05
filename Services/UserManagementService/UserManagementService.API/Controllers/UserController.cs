using MediatR;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserManagementService.API.Application.DTO.ResponseDTO;
using UserManagementService.API.Application.DTO.UserDTO;
using UserManagementService.API.Application.Features.User;
using UserManagementService.API.Application.Parameters.UserParameters;
using UserManagementService.API.Configuration;
using UserManagementService.API.Helpers;

namespace UserManagementService.API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AuthorizeByRole(AppConstants.Admin)]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRDTO<bool>), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ResponseRDTO<bool>>> Create([FromBody] UserCDTO model)
        {
            ResponseRDTO<bool> result = await _mediator.Send(new AddUserCommand(model));
            return StatusCode(result.StatusCode, result);
        }

        [AuthorizeByRole(AppConstants.Admin)]
        [HttpPut]
        [ProducesResponseType(typeof(ResponseRDTO<bool>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<bool>>> Update([FromBody] UserUDTO model, [FromQuery] string Id)
        {
            ResponseRDTO<bool> result = await _mediator.Send(new UpdateUserCommand(Id, model));
            return StatusCode(result.StatusCode, result);
        }
        [AuthorizeByRole(AppConstants.Admin)]
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseRDTO<bool>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<bool>>> Delete([FromQuery] string UserId)
        {
            ResponseRDTO<bool> result = await _mediator.Send(new DeleteUserCommand(UserId));
            return StatusCode(result.StatusCode, result);
        }
        [AuthorizeByRole(AppConstants.Admin)]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<Pagination<UserRDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<Pagination<UserRDTO>>>> GetAll([FromQuery] UserParameter parameter)
        {
            ResponseRDTO<Pagination<UserRDTO>> result = await _mediator.Send(new GetUserListQuery(parameter));
            return StatusCode(result.StatusCode, result);
        }
        [AuthorizeByRole(AppConstants.Admin)]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<UserRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<UserRDTO>>> GetById([FromQuery] string UserId)
        {
            ResponseRDTO<UserRDTO> result = await _mediator.Send(new GetUserByIdQuery(UserId));
            return StatusCode(result.StatusCode, result);
        }


    }
}
