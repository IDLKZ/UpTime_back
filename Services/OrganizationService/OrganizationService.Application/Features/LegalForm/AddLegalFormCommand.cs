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
    public class AddLegalFormCommand : IRequest<ResponseRDTO<LegalFormRDTO>>
    {
        public AddLegalFormCommand(LegalFormCDTO model)
        {
            this.model = model;
        }

        public LegalFormCDTO model { get; set; }
    }

    public class AddLegalFormCommandHandler : IRequestHandler<AddLegalFormCommand, ResponseRDTO<LegalFormRDTO>>
    {
        private readonly ILegalFormRepository LegalFormRepository;
        private readonly IMapper mapper;

        public AddLegalFormCommandHandler(ILegalFormRepository LegalFormRepository, IMapper mapper)
        {
            this.LegalFormRepository = LegalFormRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<LegalFormRDTO>> Handle(AddLegalFormCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var model = mapper.Map<LegalFormModel>(request.model);
                var entity =  await LegalFormRepository.AddAsync(model);
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


    public class AddLegalFormValidator : AbstractValidator<AddLegalFormCommand>
    {
        public AddLegalFormValidator()
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
