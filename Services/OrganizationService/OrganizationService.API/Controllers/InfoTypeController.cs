using MediatR;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Application.DTO.InfoTypeDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using OrganizationService.Application.Features.InfoType;
using System.Net;

namespace OrganizationService.API.Controllers
{
    public class InfoTypeController : BaseApiController
    {
        private readonly IMediator _mediator;

        public InfoTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<InfoTypeRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<InfoTypeRDTO>>> Get([FromQuery] string Id)
        {
            ResponseRDTO<InfoTypeRDTO> result = await _mediator.Send(new GetInfoTypeByIdQuery(Id));
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<IReadOnlyList<InfoTypeRDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<IReadOnlyList<InfoTypeRDTO>>>> All()
        {
            ResponseRDTO<IEnumerable<InfoTypeRDTO>> result = await _mediator.Send(new GetInfoTypeListQuery());
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRDTO<InfoTypeRDTO>), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ResponseRDTO<InfoTypeRDTO>>> Create([FromBody] InfoTypeCDTO model)
        {
            ResponseRDTO<InfoTypeRDTO> result = await _mediator.Send(new AddInfoTypeCommand(model));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseRDTO<InfoTypeRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<InfoTypeRDTO>>> Update([FromBody] InfoTypeUDTO model, [FromQuery] string Id)
        {
            ResponseRDTO<InfoTypeRDTO> result = await _mediator.Send(new UpdateInfoTypeCommand(model, Id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseRDTO<bool>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<bool>>> Delete([FromQuery] string Id)
        {
            ResponseRDTO<bool> result = await _mediator.Send(new DeleteInfoTypeCommand(Id));
            return StatusCode(result.StatusCode, result);
        }

    }
}
