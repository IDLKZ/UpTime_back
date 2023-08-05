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
    public class GetLegalFormByIdQuery : IRequest<ResponseRDTO<LegalFormRDTO>>
    {
        public GetLegalFormByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class GetLegalFormByIdQueryHandler : IRequestHandler<GetLegalFormByIdQuery, ResponseRDTO<LegalFormRDTO>>
    {
        private readonly ILegalFormRepository LegalFormRepository;
        private readonly IMapper mapper;

        public GetLegalFormByIdQueryHandler(ILegalFormRepository LegalFormRepository, IMapper mapper)
        {
            this.LegalFormRepository = LegalFormRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<LegalFormRDTO>> Handle(GetLegalFormByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await LegalFormRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<LegalFormRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                return new ResponseRDTO<LegalFormRDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<LegalFormRDTO>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<LegalFormRDTO>
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
