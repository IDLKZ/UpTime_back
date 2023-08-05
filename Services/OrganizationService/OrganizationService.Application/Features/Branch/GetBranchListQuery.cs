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
    public class GetBranchListQuery : IRequest<ResponseRDTO<IEnumerable<BranchRDTO>>>
    {

    }

    public class GetBranchListQueryHandler : IRequestHandler<GetBranchListQuery, ResponseRDTO<IEnumerable<BranchRDTO>>>
    {

        private readonly IBranchRepository BranchRepository;
        private readonly IMapper mapper;

        public GetBranchListQueryHandler(IBranchRepository BranchRepository, IMapper mapper)
        {
            this.BranchRepository = BranchRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<IEnumerable<BranchRDTO>>> Handle(GetBranchListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await BranchRepository.ListAllAsync();
                
                return new ResponseRDTO<IEnumerable<BranchRDTO>>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<IEnumerable<BranchRDTO>>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<IEnumerable<BranchRDTO>>
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
