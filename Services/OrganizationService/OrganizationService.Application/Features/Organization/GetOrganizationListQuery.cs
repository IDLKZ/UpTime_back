using AutoMapper;
using MediatR;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Application.DTO.OrganizationDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.Features.Organization
{
    public class GetOrganizationListQuery : IRequest<ResponseRDTO<IEnumerable<OrganizationRDTO>>>
    {

    }

    public class GetOrganizationListQueryHandler : IRequestHandler<GetOrganizationListQuery, ResponseRDTO<IEnumerable<OrganizationRDTO>>>
    {

        private readonly IOrganizationRepository OrganizationRepository;
        private readonly IMapper mapper;

        public GetOrganizationListQueryHandler(IOrganizationRepository OrganizationRepository, IMapper mapper)
        {
            this.OrganizationRepository = OrganizationRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<IEnumerable<OrganizationRDTO>>> Handle(GetOrganizationListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await OrganizationRepository.ListAllAsync();
                
                return new ResponseRDTO<IEnumerable<OrganizationRDTO>>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<IEnumerable<OrganizationRDTO>>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<IEnumerable<OrganizationRDTO>>
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
