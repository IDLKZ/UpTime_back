using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Application.DTO.OrganizationDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using OrganizationService.Application.Features.Organization;
using System.Net;

namespace OrganizationService.API.Controllers
{
    public class OrganizationController : BaseApiController
    {

        private readonly IMediator _mediator;

        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<OrganizationRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<OrganizationRDTO>>> Get([FromQuery] string Id)
        {
            ResponseRDTO<OrganizationRDTO> result = await _mediator.Send(new GetOrganizationByIdQuery(Id));
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<IReadOnlyList<OrganizationRDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<IReadOnlyList<OrganizationRDTO>>>> All()
        {
            ResponseRDTO<IEnumerable<OrganizationRDTO>> result = await _mediator.Send(new GetOrganizationListQuery());
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRDTO<OrganizationRDTO>), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ResponseRDTO<OrganizationRDTO>>> Create([FromBody] OrganizationCDTO model)
        {
            ResponseRDTO<OrganizationRDTO> result = await _mediator.Send(new AddOrganizationCommand(model));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseRDTO<OrganizationRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<OrganizationRDTO>>> Update([FromBody] OrganizationUDTO model, [FromQuery] string Id)
        {
            ResponseRDTO<OrganizationRDTO> result = await _mediator.Send(new UpdateOrganizationCommand(model, Id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseRDTO<bool>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<bool>>> Delete([FromQuery] string Id)
        {
            ResponseRDTO<bool> result = await _mediator.Send(new DeleteOrganizationCommand(Id));
            return StatusCode(result.StatusCode, result);
        }




    }
}
