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
    public class UpdateOrganizationCommand : IRequest<ResponseRDTO<OrganizationRDTO>>
    {
        public UpdateOrganizationCommand(OrganizationUDTO model, string id)
        {
            this.model = model;
            Id = id;
        }
        public OrganizationUDTO model { get; set; }
        public string Id { get; set; }
    }

    public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand, ResponseRDTO<OrganizationRDTO>>
    {
        private readonly IOrganizationRepository OrganizationRepository;
        private readonly IMapper mapper;

        public UpdateOrganizationCommandHandler(IOrganizationRepository OrganizationRepository, IMapper mapper)
        {
            this.OrganizationRepository = OrganizationRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<OrganizationRDTO>> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await OrganizationRepository.GetByIdAsync(request.Id);
                if(entity == null)
                {
                    return new ResponseRDTO<OrganizationRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                entity = mapper.Map<OrganizationUDTO,OrganizationModel>(request.model,entity);
                entity = await OrganizationRepository.UpdateAsync(entity);
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

    public class UpdateOrganizationValidator : AbstractValidator<UpdateOrganizationCommand>
    {
        public UpdateOrganizationValidator()
        {
            RuleFor<string>(p => p.Id)
                .NotNull().WithMessage("Not Null")
                .NotEmpty().WithMessage("Not Empty")
                .OverridePropertyName("Id");
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
