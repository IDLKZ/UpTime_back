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
    public class UpdateAreaCommand : IRequest<ResponseRDTO<AreaRDTO>>
    {
        public UpdateAreaCommand(AreaUDTO model, string id)
        {
            this.model = model;
            Id = id;
        }
        public AreaUDTO model { get; set; }
        public string Id { get; set; }
    }

    public class UpdateAreaCommandHandler : IRequestHandler<UpdateAreaCommand, ResponseRDTO<AreaRDTO>>
    {
        private readonly IAreaRepository areaRepository;
        private readonly IMapper mapper;

        public UpdateAreaCommandHandler(IAreaRepository areaRepository, IMapper mapper)
        {
            this.areaRepository = areaRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<AreaRDTO>> Handle(UpdateAreaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await areaRepository.GetByIdAsync(request.Id);
                if(entity == null)
                {
                    return new ResponseRDTO<AreaRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                entity = mapper.Map<AreaUDTO,AreaModel>(request.model,entity);
                entity = await areaRepository.UpdateAsync(entity);
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

    public class UpdateAreaValidator : AbstractValidator<UpdateAreaCommand>
    {
        public UpdateAreaValidator()
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
