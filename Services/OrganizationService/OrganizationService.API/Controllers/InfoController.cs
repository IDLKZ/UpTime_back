using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Application.DTO.InfoDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using OrganizationService.Application.Features.Info;
using System.Net;

namespace OrganizationService.API.Controllers
{
    
    class InfoController : BaseApiController
    {
        private readonly IMediator _mediator;

        public InfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<InfoRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<InfoRDTO>>> Get([FromQuery] string Id)
        {
            ResponseRDTO<InfoRDTO> result = await _mediator.Send(new GetInfoByIdQuery(Id));
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<IReadOnlyList<InfoRDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<IReadOnlyList<InfoRDTO>>>> All()
        {
            ResponseRDTO<IEnumerable<InfoRDTO>> result = await _mediator.Send(new GetInfoListQuery());
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRDTO<InfoRDTO>), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ResponseRDTO<InfoRDTO>>> Create([FromBody] InfoCDTO model)
        {
            ResponseRDTO<InfoRDTO> result = await _mediator.Send(new AddInfoCommand(model));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseRDTO<InfoRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<InfoRDTO>>> Update([FromBody] InfoUDTO model, [FromQuery] string Id)
        {
            ResponseRDTO<InfoRDTO> result = await _mediator.Send(new UpdateInfoCommand(model, Id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseRDTO<bool>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<bool>>> Delete([FromQuery] string Id)
        {
            ResponseRDTO<bool> result = await _mediator.Send(new DeleteInfoCommand(Id));
            return StatusCode(result.StatusCode, result);
        }
    }

}
