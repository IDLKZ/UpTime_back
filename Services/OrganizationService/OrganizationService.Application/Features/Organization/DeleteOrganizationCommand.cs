using AutoMapper;
using MediatR;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Application.DTO.OrganizationDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using OrganizationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.Features.Organization
{
    public class DeleteOrganizationCommand : IRequest<ResponseRDTO<bool>>
    {
        public DeleteOrganizationCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class DeleteOrganizationCommandHandler : IRequestHandler<DeleteOrganizationCommand, ResponseRDTO<bool>>
    {
        private readonly IOrganizationRepository OrganizationRepository;
        private readonly IMapper mapper;

        public DeleteOrganizationCommandHandler(IOrganizationRepository OrganizationRepository, IMapper mapper)
        {
            this.OrganizationRepository = OrganizationRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<bool>> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await OrganizationRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<bool>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                await OrganizationRepository.DeleteAsync(entity);
                return new ResponseRDTO<bool>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<bool>
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
