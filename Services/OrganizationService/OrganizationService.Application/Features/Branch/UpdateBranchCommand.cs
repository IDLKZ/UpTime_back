using AutoMapper;
using FluentValidation;
using MediatR;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Application.DTO.BranchDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using OrganizationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.Features.Branch
{
    public class UpdateBranchCommand : IRequest<ResponseRDTO<BranchRDTO>>
    {
        public UpdateBranchCommand(BranchUDTO model, string id)
        {
            this.model = model;
            Id = id;
        }
        public BranchUDTO model { get; set; }
        public string Id { get; set; }
    }

    public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, ResponseRDTO<BranchRDTO>>
    {
        private readonly IBranchRepository BranchRepository;
        private readonly IMapper mapper;

        public UpdateBranchCommandHandler(IBranchRepository BranchRepository, IMapper mapper)
        {
            this.BranchRepository = BranchRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<BranchRDTO>> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await BranchRepository.GetByIdAsync(request.Id);
                if(entity == null)
                {
                    return new ResponseRDTO<BranchRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                entity = mapper.Map<BranchUDTO,BranchModel>(request.model,entity);
                entity = await BranchRepository.UpdateAsync(entity);
                return new ResponseRDTO<BranchRDTO>
                {
                    StatusCode = 201,
                    Success = true,
                    Data = mapper.Map<BranchRDTO>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<BranchRDTO>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = ex.Message,
                    Detail = ex.ToString(),
                };
            }
        }
    }

    public class UpdateBranchValidator : AbstractValidator<UpdateBranchCommand>
    {
        public UpdateBranchValidator()
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
