using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Application.DTO.BranchDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using OrganizationService.Domain.Models;

namespace OrganizationService.Application.Features.Branch
{
    public class AddBranchCommand : IRequest<ResponseRDTO<BranchRDTO>>
    {
        public AddBranchCommand(BranchCDTO model)
        {
            this.model = model;
        }

        public BranchCDTO model { get; set; }
    }

    public class AddBranchCommandHandler : IRequestHandler<AddBranchCommand, ResponseRDTO<BranchRDTO>>
    {
        private readonly IBranchRepository BranchRepository;
        private readonly IMapper mapper;

        public AddBranchCommandHandler(IBranchRepository BranchRepository, IMapper mapper)
        {
            this.BranchRepository = BranchRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<BranchRDTO>> Handle(AddBranchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var model = mapper.Map<BranchModel>(request.model);
                var entity =  await BranchRepository.AddAsync(model);
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


    public class AddBranchValidator : AbstractValidator<AddBranchCommand>
    {
        public AddBranchValidator()
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
