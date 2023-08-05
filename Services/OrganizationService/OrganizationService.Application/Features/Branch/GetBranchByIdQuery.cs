using AutoMapper;
using MediatR;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Application.DTO.BranchDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.Features.Branch
{
    public class GetBranchByIdQuery : IRequest<ResponseRDTO<BranchRDTO>>
    {
        public GetBranchByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class GetBranchByIdQueryHandler : IRequestHandler<GetBranchByIdQuery, ResponseRDTO<BranchRDTO>>
    {
        private readonly IBranchRepository BranchRepository;
        private readonly IMapper mapper;

        public GetBranchByIdQueryHandler(IBranchRepository BranchRepository, IMapper mapper)
        {
            this.BranchRepository = BranchRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<BranchRDTO>> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await BranchRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<BranchRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                return new ResponseRDTO<BranchRDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<BranchRDTO>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<BranchRDTO>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = ex.Message,
                    Detail = ex.ToString(),
                };
            }
        }
    }
}
