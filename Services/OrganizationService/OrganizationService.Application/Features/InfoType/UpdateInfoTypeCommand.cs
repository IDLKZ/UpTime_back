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
    public class UpdateInfoTypeCommand : IRequest<ResponseRDTO<InfoTypeRDTO>>
    {
        public UpdateInfoTypeCommand(InfoTypeUDTO model, string id)
        {
            this.model = model;
            Id = id;
        }
        public InfoTypeUDTO model { get; set; }
        public string Id { get; set; }
    }

    public class UpdateInfoTypeCommandHandler : IRequestHandler<UpdateInfoTypeCommand, ResponseRDTO<InfoTypeRDTO>>
    {
        private readonly IInfoTypeRepository InfoTypeRepository;
        private readonly IMapper mapper;

        public UpdateInfoTypeCommandHandler(IInfoTypeRepository InfoTypeRepository, IMapper mapper)
        {
            this.InfoTypeRepository = InfoTypeRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<InfoTypeRDTO>> Handle(UpdateInfoTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await InfoTypeRepository.GetByIdAsync(request.Id);
                if(entity == null)
                {
                    return new ResponseRDTO<InfoTypeRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                entity = mapper.Map<InfoTypeUDTO,InfoTypeModel>(request.model,entity);
                entity = await InfoTypeRepository.UpdateAsync(entity);
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

    public class UpdateInfoTypeValidator : AbstractValidator<UpdateInfoTypeCommand>
    {
        public UpdateInfoTypeValidator()
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
