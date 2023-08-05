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
    public class AddInfoCommand : IRequest<ResponseRDTO<InfoRDTO>>
    {
        public AddInfoCommand(InfoCDTO model)
        {
            this.model = model;
        }

        public InfoCDTO model { get; set; }
    }

    public class AddInfoCommandHandler : IRequestHandler<AddInfoCommand, ResponseRDTO<InfoRDTO>>
    {
        private readonly IInfoRepository InfoRepository;
        private readonly IMapper mapper;

        public AddInfoCommandHandler(IInfoRepository InfoRepository, IMapper mapper)
        {
            this.InfoRepository = InfoRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<InfoRDTO>> Handle(AddInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var model = mapper.Map<InfoModel>(request.model);
                var entity =  await InfoRepository.AddAsync(model);
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


    public class AddInfoValidator : AbstractValidator<AddInfoCommand>
    {
        public AddInfoValidator()
        {
            
        }


    }
}
