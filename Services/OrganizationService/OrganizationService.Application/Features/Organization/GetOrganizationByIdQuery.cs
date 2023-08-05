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
    public class GetOrganizationByIdQuery : IRequest<ResponseRDTO<OrganizationRDTO>>
    {
        public GetOrganizationByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class GetOrganizationByIdQueryHandler : IRequestHandler<GetOrganizationByIdQuery, ResponseRDTO<OrganizationRDTO>>
    {
        private readonly IOrganizationRepository OrganizationRepository;
        private readonly IMapper mapper;

        public GetOrganizationByIdQueryHandler(IOrganizationRepository OrganizationRepository, IMapper mapper)
        {
            this.OrganizationRepository = OrganizationRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<OrganizationRDTO>> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await OrganizationRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<OrganizationRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                return new ResponseRDTO<OrganizationRDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<OrganizationRDTO>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<OrganizationRDTO>
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
