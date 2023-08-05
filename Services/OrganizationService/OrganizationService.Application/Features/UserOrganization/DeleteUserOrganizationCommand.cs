using AutoMapper;
using MediatR;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Application.DTO.UserOrganizationDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using OrganizationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.Features.UserOrganization
{
    public class DeleteUserOrganizationCommand : IRequest<ResponseRDTO<bool>>
    {
        public DeleteUserOrganizationCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class DeleteUserOrganizationCommandHandler : IRequestHandler<DeleteUserOrganizationCommand, ResponseRDTO<bool>>
    {
        private readonly IUserOrganizationRepository UserOrganizationRepository;
        private readonly IMapper mapper;

        public DeleteUserOrganizationCommandHandler(IUserOrganizationRepository UserOrganizationRepository, IMapper mapper)
        {
            this.UserOrganizationRepository = UserOrganizationRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<bool>> Handle(DeleteUserOrganizationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await UserOrganizationRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<bool>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                await UserOrganizationRepository.DeleteAsync(entity);
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
