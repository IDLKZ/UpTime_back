using AutoMapper;
using FluentValidation;
using MediatR;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Application.DTO.InfoDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using OrganizationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.Features.Info
{
    public class UpdateInfoCommand : IRequest<ResponseRDTO<InfoRDTO>>
    {
        public UpdateInfoCommand(InfoUDTO model, string id)
        {
            this.model = model;
            Id = id;
        }
        public InfoUDTO model { get; set; }
        public string Id { get; set; }
    }

    public class UpdateInfoCommandHandler : IRequestHandler<UpdateInfoCommand, ResponseRDTO<InfoRDTO>>
    {
        private readonly IInfoRepository InfoRepository;
        private readonly IMapper mapper;

        public UpdateInfoCommandHandler(IInfoRepository InfoRepository, IMapper mapper)
        {
            this.InfoRepository = InfoRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<InfoRDTO>> Handle(UpdateInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await InfoRepository.GetByIdAsync(request.Id);
                if(entity == null)
                {
                    return new ResponseRDTO<InfoRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                entity = mapper.Map<InfoUDTO,InfoModel>(request.model,entity);
                entity = await InfoRepository.UpdateAsync(entity);
                return new ResponseRDTO<InfoRDTO>
                {
                    StatusCode = 201,
                    Success = true,
                    Data = mapper.Map<InfoRDTO>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<InfoRDTO>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = ex.Message,
                    Detail = ex.ToString(),
                };
            }
        }
    }

    public class UpdateInfoValidator : AbstractValidator<UpdateInfoCommand>
    {
        public UpdateInfoValidator()
        {
            RuleFor<string>(p => p.Id)
                .NotNull().WithMessage("Not Null")
                .NotEmpty().WithMessage("Not Empty")
                .OverridePropertyName("Id");
            
        }


    }
}
