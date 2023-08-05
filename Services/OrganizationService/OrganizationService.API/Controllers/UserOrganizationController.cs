using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Application.DTO.ResponseDTO;
using OrganizationService.Application.DTO.UserOrganizationDTO;
using OrganizationService.Application.Features.UserOrganization;
using System.Net;

namespace OrganizationService.API.Controllers
{
    public class UserOrganizationController : BaseApiController
    {
        private readonly IMediator _mediator;

        public UserOrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<UserOrganizationRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<UserOrganizationRDTO>>> Get([FromQuery] string Id)
        {
            ResponseRDTO<UserOrganizationRDTO> result = await _mediator.Send(new GetUserOrganizationByIdQuery(Id));
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<IReadOnlyList<UserOrganizationRDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<IReadOnlyList<UserOrganizationRDTO>>>> All()
        {
            ResponseRDTO<IEnumerable<UserOrganizationRDTO>> result = await _mediator.Send(new GetUserOrganizationListQuery());
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRDTO<UserOrganizationRDTO>), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ResponseRDTO<UserOrganizationRDTO>>> Create([FromBody] UserOrganizationCDTO model)
        {
            ResponseRDTO<UserOrganizationRDTO> result = await _mediator.Send(new AddUserOrganizationCommand(model));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseRDTO<UserOrganizationRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<UserOrganizationRDTO>>> Update([FromBody] UserOrganizationUDTO model, [FromQuery] string Id)
        {
            ResponseRDTO<UserOrganizationRDTO> result = await _mediator.Send(new UpdateUserOrganizationCommand(model, Id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseRDTO<bool>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<bool>>> Delete([FromQuery] string Id)
        {
            ResponseRDTO<bool> result = await _mediator.Send(new DeleteUserOrganizationCommand(Id));
            return StatusCode(result.StatusCode, result);
        }

    }
}
