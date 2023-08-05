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
    public class AddUserOrganizationCommand : IRequest<ResponseRDTO<UserOrganizationRDTO>>
    {
        public AddUserOrganizationCommand(UserOrganizationCDTO model)
        {
            this.model = model;
        }

        public UserOrganizationCDTO model { get; set; }
    }

    public class AddUserOrganizationCommandHandler : IRequestHandler<AddUserOrganizationCommand, ResponseRDTO<UserOrganizationRDTO>>
    {
        private readonly IUserOrganizationRepository UserOrganizationRepository;
        private readonly IMapper mapper;

        public AddUserOrganizationCommandHandler(IUserOrganizationRepository UserOrganizationRepository, IMapper mapper)
        {
            this.UserOrganizationRepository = UserOrganizationRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<UserOrganizationRDTO>> Handle(AddUserOrganizationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var model = mapper.Map<UserOrganizationModel>(request.model);
                var entity =  await UserOrganizationRepository.AddAsync(model);
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


    public class AddUserOrganizationValidator : AbstractValidator<AddUserOrganizationCommand>
    {
        public AddUserOrganizationValidator()
        {
            
        }


    }
}
