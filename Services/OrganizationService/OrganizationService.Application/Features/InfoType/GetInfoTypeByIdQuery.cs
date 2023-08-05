using AutoMapper;
using MediatR;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Application.DTO.InfoTypeDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.Features.InfoType
{
    public class GetInfoTypeByIdQuery : IRequest<ResponseRDTO<InfoTypeRDTO>>
    {
        public GetInfoTypeByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class GetInfoTypeByIdQueryHandler : IRequestHandler<GetInfoTypeByIdQuery, ResponseRDTO<InfoTypeRDTO>>
    {
        private readonly IInfoTypeRepository InfoTypeRepository;
        private readonly IMapper mapper;

        public GetInfoTypeByIdQueryHandler(IInfoTypeRepository InfoTypeRepository, IMapper mapper)
        {
            this.InfoTypeRepository = InfoTypeRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<InfoTypeRDTO>> Handle(GetInfoTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await InfoTypeRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<InfoTypeRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                return new ResponseRDTO<InfoTypeRDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<InfoTypeRDTO>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<InfoTypeRDTO>
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
