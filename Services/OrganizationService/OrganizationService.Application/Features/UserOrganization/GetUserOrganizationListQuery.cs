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
    public class GetUserOrganizationListQuery : IRequest<ResponseRDTO<IEnumerable<UserOrganizationRDTO>>>
    {

    }

    public class GetUserOrganizationListQueryHandler : IRequestHandler<GetUserOrganizationListQuery, ResponseRDTO<IEnumerable<UserOrganizationRDTO>>>
    {

        private readonly IUserOrganizationRepository UserOrganizationRepository;
        private readonly IMapper mapper;

        public GetUserOrganizationListQueryHandler(IUserOrganizationRepository UserOrganizationRepository, IMapper mapper)
        {
            this.UserOrganizationRepository = UserOrganizationRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<IEnumerable<UserOrganizationRDTO>>> Handle(GetUserOrganizationListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await UserOrganizationRepository.ListAllAsync();
                
                return new ResponseRDTO<IEnumerable<UserOrganizationRDTO>>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<IEnumerable<UserOrganizationRDTO>>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<IEnumerable<UserOrganizationRDTO>>
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
