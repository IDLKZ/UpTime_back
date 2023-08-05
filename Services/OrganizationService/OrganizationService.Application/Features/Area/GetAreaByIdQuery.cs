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
    public class GetAreaByIdQuery : IRequest<ResponseRDTO<AreaRDTO>>
    {
        public GetAreaByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class GetAreaByIdQueryHandler : IRequestHandler<GetAreaByIdQuery, ResponseRDTO<AreaRDTO>>
    {
        private readonly IAreaRepository areaRepository;
        private readonly IMapper mapper;

        public GetAreaByIdQueryHandler(IAreaRepository areaRepository, IMapper mapper)
        {
            this.areaRepository = areaRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<AreaRDTO>> Handle(GetAreaByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await areaRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<AreaRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                return new ResponseRDTO<AreaRDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<AreaRDTO>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<AreaRDTO>
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
