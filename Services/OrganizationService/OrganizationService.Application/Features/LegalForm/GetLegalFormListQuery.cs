using AutoMapper;
using MediatR;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Application.DTO.LegalFormDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.Features.LegalForm
{
    public class GetLegalFormListQuery : IRequest<ResponseRDTO<IEnumerable<LegalFormRDTO>>>
    {

    }

    public class GetLegalFormListQueryHandler : IRequestHandler<GetLegalFormListQuery, ResponseRDTO<IEnumerable<LegalFormRDTO>>>
    {

        private readonly ILegalFormRepository LegalFormRepository;
        private readonly IMapper mapper;

        public GetLegalFormListQueryHandler(ILegalFormRepository LegalFormRepository, IMapper mapper)
        {
            this.LegalFormRepository = LegalFormRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<IEnumerable<LegalFormRDTO>>> Handle(GetLegalFormListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await LegalFormRepository.ListAllAsync();
                
                return new ResponseRDTO<IEnumerable<LegalFormRDTO>>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<IEnumerable<LegalFormRDTO>>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<IEnumerable<LegalFormRDTO>>
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
