using AutoMapper;
using FluentValidation;
using MediatR;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Application.DTO.AreaDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using OrganizationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.Features.Area
{
    public class AddAreaCommand : IRequest<ResponseRDTO<AreaRDTO>>
    {
        public AddAreaCommand(AreaCDTO model)
        {
            this.model = model;
        }

        public AreaCDTO model { get; set; }
    }

    public class AddAreaCommandHandler : IRequestHandler<AddAreaCommand, ResponseRDTO<AreaRDTO>>
    {
        private readonly IAreaRepository areaRepository;
        private readonly IMapper mapper;

        public AddAreaCommandHandler(IAreaRepository areaRepository, IMapper mapper)
        {
            this.areaRepository = areaRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<AreaRDTO>> Handle(AddAreaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var model = mapper.Map<AreaModel>(request.model);
                var entity =  await areaRepository.AddAsync(model);
                return new ResponseRDTO<AreaRDTO>
                {
                    StatusCode = 201,
                    Success = true,
                    Data = mapper.Map<AreaRDTO>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<AreaRDTO>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = ex.Message,
                    Detail = ex.ToString(),
                };
            }
        }

    }


    public class AddAreaValidator : AbstractValidator<AddAreaCommand>
    {
        public AddAreaValidator()
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
