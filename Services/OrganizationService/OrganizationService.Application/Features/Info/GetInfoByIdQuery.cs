using AutoMapper;
using MediatR;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Application.DTO.InfoDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.Features.Info
{
    public class GetInfoByIdQuery : IRequest<ResponseRDTO<InfoRDTO>>
    {
        public GetInfoByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class GetInfoByIdQueryHandler : IRequestHandler<GetInfoByIdQuery, ResponseRDTO<InfoRDTO>>
    {
        private readonly IInfoRepository InfoRepository;
        private readonly IMapper mapper;

        public GetInfoByIdQueryHandler(IInfoRepository InfoRepository, IMapper mapper)
        {
            this.InfoRepository = InfoRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<InfoRDTO>> Handle(GetInfoByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await InfoRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<InfoRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                return new ResponseRDTO<InfoRDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<InfoRDTO>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<InfoRDTO>
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
