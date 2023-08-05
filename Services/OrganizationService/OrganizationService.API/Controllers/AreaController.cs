using MediatR;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Application.DTO.AreaDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using OrganizationService.Application.Features.Area;
using System.Net;

namespace OrganizationService.API.Controllers
{
    public class AreaController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AreaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<AreaRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<AreaRDTO>>> Get([FromQuery] string Id)
        {
            ResponseRDTO<AreaRDTO> result = await _mediator.Send(new GetAreaByIdQuery(Id));
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<IReadOnlyList<AreaRDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<IReadOnlyList<AreaRDTO>>>> All()
        {
            ResponseRDTO<IEnumerable<AreaRDTO>> result = await _mediator.Send(new GetAreaListQuery());
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRDTO<AreaRDTO>), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ResponseRDTO<AreaRDTO>>> Create([FromBody] AreaCDTO model)
        {
            ResponseRDTO<AreaRDTO> result = await _mediator.Send(new AddAreaCommand(model));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseRDTO<AreaRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<AreaRDTO>>> Update([FromBody] AreaUDTO model, [FromQuery] string Id)
        {
            ResponseRDTO<AreaRDTO> result = await _mediator.Send(new UpdateAreaCommand(model, Id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseRDTO<bool>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<bool>>> Delete([FromQuery] string Id)
        {
            ResponseRDTO<bool> result = await _mediator.Send(new DeleteAreaCommand(Id));
            return StatusCode(result.StatusCode, result);
        }

    }
}
