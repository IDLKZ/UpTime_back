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
    public class GetInfoTypeListQuery : IRequest<ResponseRDTO<IEnumerable<InfoTypeRDTO>>>
    {

    }

    public class GetInfoTypeListQueryHandler : IRequestHandler<GetInfoTypeListQuery, ResponseRDTO<IEnumerable<InfoTypeRDTO>>>
    {

        private readonly IInfoTypeRepository InfoTypeRepository;
        private readonly IMapper mapper;

        public GetInfoTypeListQueryHandler(IInfoTypeRepository InfoTypeRepository, IMapper mapper)
        {
            this.InfoTypeRepository = InfoTypeRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<IEnumerable<InfoTypeRDTO>>> Handle(GetInfoTypeListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await InfoTypeRepository.ListAllAsync();
                
                return new ResponseRDTO<IEnumerable<InfoTypeRDTO>>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<IEnumerable<InfoTypeRDTO>>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<IEnumerable<InfoTypeRDTO>>
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
