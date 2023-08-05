using AutoMapper;
using MediatR;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Application.DTO.AreaDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.Features.Area
{
    public class GetAreaListQuery : IRequest<ResponseRDTO<IEnumerable<AreaRDTO>>>
    {

    }

    public class GetAreaListQueryHandler : IRequestHandler<GetAreaListQuery, ResponseRDTO<IEnumerable<AreaRDTO>>>
    {

        private readonly IAreaRepository areaRepository;
        private readonly IMapper mapper;

        public GetAreaListQueryHandler(IAreaRepository areaRepository, IMapper mapper)
        {
            this.areaRepository = areaRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<IEnumerable<AreaRDTO>>> Handle(GetAreaListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await areaRepository.ListAllAsync();
                
                return new ResponseRDTO<IEnumerable<AreaRDTO>>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<IEnumerable<AreaRDTO>>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<IEnumerable<AreaRDTO>>
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
