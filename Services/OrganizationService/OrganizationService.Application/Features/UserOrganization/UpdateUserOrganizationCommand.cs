using AutoMapper;
using FluentValidation;
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
    public class UpdateUserOrganizationCommand : IRequest<ResponseRDTO<UserOrganizationRDTO>>
    {
        public UpdateUserOrganizationCommand(UserOrganizationUDTO model, string id)
        {
            this.model = model;
            Id = id;
        }
        public UserOrganizationUDTO model { get; set; }
        public string Id { get; set; }
    }

    public class UpdateUserOrganizationCommandHandler : IRequestHandler<UpdateUserOrganizationCommand, ResponseRDTO<UserOrganizationRDTO>>
    {
        private readonly IUserOrganizationRepository UserOrganizationRepository;
        private readonly IMapper mapper;

        public UpdateUserOrganizationCommandHandler(IUserOrganizationRepository UserOrganizationRepository, IMapper mapper)
        {
            this.UserOrganizationRepository = UserOrganizationRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<UserOrganizationRDTO>> Handle(UpdateUserOrganizationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await UserOrganizationRepository.GetByIdAsync(request.Id);
                if(entity == null)
                {
                    return new ResponseRDTO<UserOrganizationRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                entity = mapper.Map<UserOrganizationUDTO,UserOrganizationModel>(request.model,entity);
                entity = await UserOrganizationRepository.UpdateAsync(entity);
                return new ResponseRDTO<UserOrganizationRDTO>
                {
                    StatusCode = 201,
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

    public class UpdateUserOrganizationValidator : AbstractValidator<UpdateUserOrganizationCommand>
    {
        public UpdateUserOrganizationValidator()
        {
           
        }


    }
}
