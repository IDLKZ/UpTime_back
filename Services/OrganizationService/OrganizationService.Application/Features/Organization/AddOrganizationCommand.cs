using AutoMapper;
using FluentValidation;
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
    public class AddOrganizationCommand : IRequest<ResponseRDTO<OrganizationRDTO>>
    {
        public AddOrganizationCommand(OrganizationCDTO model)
        {
            this.model = model;
        }

        public OrganizationCDTO model { get; set; }
    }

    public class AddOrganizationCommandHandler : IRequestHandler<AddOrganizationCommand, ResponseRDTO<OrganizationRDTO>>
    {
        private readonly IOrganizationRepository OrganizationRepository;
        private readonly IMapper mapper;

        public AddOrganizationCommandHandler(IOrganizationRepository OrganizationRepository, IMapper mapper)
        {
            this.OrganizationRepository = OrganizationRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<OrganizationRDTO>> Handle(AddOrganizationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var model = mapper.Map<OrganizationModel>(request.model);
                var entity =  await OrganizationRepository.AddAsync(model);
                return new ResponseRDTO<OrganizationRDTO>
                {
                    StatusCode = 201,
                    Success = true,
                    Data = mapper.Map<OrganizationRDTO>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<OrganizationRDTO>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = ex.Message,
                    Detail = ex.ToString(),
                };
            }
        }

    }


    public class AddOrganizationValidator : AbstractValidator<AddOrganizationCommand>
    {
        public AddOrganizationValidator()
        {
            RuleFor<string>(p => p.model.TitleRu)
                .NotNull().WithMessage("Not Null")
                .NotEmpty().WithMessage("Not Empty")
                .MaximumLength(255).WithMessage("Maximum Length is 255")
                .OverridePropertyName("TitleRu");
            RuleFor<string>(p => p.model.TitleKk)
                .NotNull().WithMessage("Not Null")
                .NotEmpty().WithMessage("Not Empty")
                .MaximumLength(255).WithMessage("Maximum Length is 255")
                .OverridePropertyName("TitleKk");
            RuleFor<string>(p => p.model.TitleEn)
                .NotNull().WithMessage("Not Null")
                .NotEmpty().WithMessage("Not Empty")
                .MaximumLength(255).WithMessage("Maximum Length is 255")
                .OverridePropertyName("TitleEn");
            RuleFor<int>(p => p.model.Status)
                .NotNull().WithMessage("Not Null")
                .NotEmpty().WithMessage("Not Empty")
                .OverridePropertyName("Status");
        }


    }
}
