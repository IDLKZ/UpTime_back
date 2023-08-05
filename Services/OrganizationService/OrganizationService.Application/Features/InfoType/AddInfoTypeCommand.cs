using AutoMapper;
using FluentValidation;
using MediatR;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Application.DTO.InfoTypeDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using OrganizationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.Features.InfoType
{
    public class AddInfoTypeCommand : IRequest<ResponseRDTO<InfoTypeRDTO>>
    {
        public AddInfoTypeCommand(InfoTypeCDTO model)
        {
            this.model = model;
        }

        public InfoTypeCDTO model { get; set; }
    }

    public class AddInfoTypeCommandHandler : IRequestHandler<AddInfoTypeCommand, ResponseRDTO<InfoTypeRDTO>>
    {
        private readonly IInfoTypeRepository InfoTypeRepository;
        private readonly IMapper mapper;

        public AddInfoTypeCommandHandler(IInfoTypeRepository InfoTypeRepository, IMapper mapper)
        {
            this.InfoTypeRepository = InfoTypeRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<InfoTypeRDTO>> Handle(AddInfoTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var model = mapper.Map<InfoTypeModel>(request.model);
                var entity =  await InfoTypeRepository.AddAsync(model);
                return new ResponseRDTO<InfoTypeRDTO>
                {
                    StatusCode = 201,
                    Success = true,
                    Data = mapper.Map<InfoTypeRDTO>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<InfoTypeRDTO>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = ex.Message,
                    Detail = ex.ToString(),
                };
            }
        }

    }


    public class AddInfoTypeValidator : AbstractValidator<AddInfoTypeCommand>
    {
        public AddInfoTypeValidator()
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
