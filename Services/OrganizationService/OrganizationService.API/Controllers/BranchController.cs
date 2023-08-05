using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Application.DTO.BranchDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using OrganizationService.Application.Features.Branch;
using System.Net;

namespace OrganizationService.API.Controllers
{
    public class BranchController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BranchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<BranchRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<BranchRDTO>>> Get([FromQuery] string Id)
        {
            ResponseRDTO<BranchRDTO> result = await _mediator.Send(new GetBranchByIdQuery(Id));
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<IReadOnlyList<BranchRDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<IReadOnlyList<BranchRDTO>>>> All()
        {
            ResponseRDTO<IEnumerable<BranchRDTO>> result = await _mediator.Send(new GetBranchListQuery());
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRDTO<BranchRDTO>), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ResponseRDTO<BranchRDTO>>> Create([FromBody] BranchCDTO model)
        {
            ResponseRDTO<BranchRDTO> result = await _mediator.Send(new AddBranchCommand(model));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseRDTO<BranchRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<BranchRDTO>>> Update([FromBody] BranchUDTO model, [FromQuery] string Id)
        {
            ResponseRDTO<BranchRDTO> result = await _mediator.Send(new UpdateBranchCommand(model, Id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseRDTO<bool>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<bool>>> Delete([FromQuery] string Id)
        {
            ResponseRDTO<bool> result = await _mediator.Send(new DeleteBranchCommand(Id));
            return StatusCode(result.StatusCode, result);
        }

    }
}
