using AutoMapper;
using MediatR;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Application.DTO.UserOrganizationDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.Features.UserOrganization
{
    public class GetUserOrganizationByIdQuery : IRequest<ResponseRDTO<UserOrganizationRDTO>>
    {
        public GetUserOrganizationByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class GetUserOrganizationByIdQueryHandler : IRequestHandler<GetUserOrganizationByIdQuery, ResponseRDTO<UserOrganizationRDTO>>
    {
        private readonly IUserOrganizationRepository UserOrganizationRepository;
        private readonly IMapper mapper;

        public GetUserOrganizationByIdQueryHandler(IUserOrganizationRepository UserOrganizationRepository, IMapper mapper)
        {
            this.UserOrganizationRepository = UserOrganizationRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<UserOrganizationRDTO>> Handle(GetUserOrganizationByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await UserOrganizationRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<UserOrganizationRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                return new ResponseRDTO<UserOrganizationRDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<UserOrganizationRDTO>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<UserOrganizationRDTO>
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
