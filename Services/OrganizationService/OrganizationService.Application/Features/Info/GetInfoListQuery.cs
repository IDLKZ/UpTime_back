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
    public class GetInfoListQuery : IRequest<ResponseRDTO<IEnumerable<InfoRDTO>>>
    {

    }

    public class GetInfoListQueryHandler : IRequestHandler<GetInfoListQuery, ResponseRDTO<IEnumerable<InfoRDTO>>>
    {

        private readonly IInfoRepository InfoRepository;
        private readonly IMapper mapper;

        public GetInfoListQueryHandler(IInfoRepository InfoRepository, IMapper mapper)
        {
            this.InfoRepository = InfoRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<IEnumerable<InfoRDTO>>> Handle(GetInfoListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await InfoRepository.ListAllAsync();
                
                return new ResponseRDTO<IEnumerable<InfoRDTO>>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<IEnumerable<InfoRDTO>>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<IEnumerable<InfoRDTO>>
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
