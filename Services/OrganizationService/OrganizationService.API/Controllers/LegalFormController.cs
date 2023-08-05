using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Application.DTO.LegalFormDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using OrganizationService.Application.Features.LegalForm;
using System.Net;

namespace OrganizationService.API.Controllers
{
    public class LegalFormController : BaseApiController
    {
        private readonly IMediator _mediator;

        public LegalFormController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<LegalFormRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<LegalFormRDTO>>> Get([FromQuery] string Id)
        {
            ResponseRDTO<LegalFormRDTO> result = await _mediator.Send(new GetLegalFormByIdQuery(Id));
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<IReadOnlyList<LegalFormRDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<IReadOnlyList<LegalFormRDTO>>>> All()
        {
            ResponseRDTO<IEnumerable<LegalFormRDTO>> result = await _mediator.Send(new GetLegalFormListQuery());
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRDTO<LegalFormRDTO>), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ResponseRDTO<LegalFormRDTO>>> Create([FromBody] LegalFormCDTO model)
        {
            ResponseRDTO<LegalFormRDTO> result = await _mediator.Send(new AddLegalFormCommand(model));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseRDTO<LegalFormRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<LegalFormRDTO>>> Update([FromBody] LegalFormUDTO model, [FromQuery] string Id)
        {
            ResponseRDTO<LegalFormRDTO> result = await _mediator.Send(new UpdateLegalFormCommand(model, Id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseRDTO<bool>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<bool>>> Delete([FromQuery] string Id)
        {
            ResponseRDTO<bool> result = await _mediator.Send(new DeleteLegalFormCommand(Id));
            return StatusCode(result.StatusCode, result);
        }

    }
}
