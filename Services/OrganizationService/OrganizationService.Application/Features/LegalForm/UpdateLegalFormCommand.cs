using AutoMapper;
using FluentValidation;
using MediatR;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Application.DTO.LegalFormDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using OrganizationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.Features.LegalForm
{
    public class UpdateLegalFormCommand : IRequest<ResponseRDTO<LegalFormRDTO>>
    {
        public UpdateLegalFormCommand(LegalFormUDTO model, string id)
        {
            this.model = model;
            Id = id;
        }
        public LegalFormUDTO model { get; set; }
        public string Id { get; set; }
    }

    public class UpdateLegalFormCommandHandler : IRequestHandler<UpdateLegalFormCommand, ResponseRDTO<LegalFormRDTO>>
    {
        private readonly ILegalFormRepository LegalFormRepository;
        private readonly IMapper mapper;

        public UpdateLegalFormCommandHandler(ILegalFormRepository LegalFormRepository, IMapper mapper)
        {
            this.LegalFormRepository = LegalFormRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<LegalFormRDTO>> Handle(UpdateLegalFormCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await LegalFormRepository.GetByIdAsync(request.Id);
                if(entity == null)
                {
                    return new ResponseRDTO<LegalFormRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                entity = mapper.Map<LegalFormUDTO,LegalFormModel>(request.model,entity);
                entity = await LegalFormRepository.UpdateAsync(entity);
                return new ResponseRDTO<LegalFormRDTO>
                {
                    StatusCode = 201,
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

    public class UpdateLegalFormValidator : AbstractValidator<UpdateLegalFormCommand>
    {
        public UpdateLegalFormValidator()
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
