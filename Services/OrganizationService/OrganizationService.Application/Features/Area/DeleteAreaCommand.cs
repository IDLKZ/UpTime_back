using AutoMapper;
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
    public class DeleteAreaCommand : IRequest<ResponseRDTO<bool>>
    {
        public DeleteAreaCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class DeleteAreaCommandHandler : IRequestHandler<DeleteAreaCommand, ResponseRDTO<bool>>
    {
        private readonly IAreaRepository areaRepository;
        private readonly IMapper mapper;

        public DeleteAreaCommandHandler(IAreaRepository areaRepository, IMapper mapper)
        {
            this.areaRepository = areaRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<bool>> Handle(DeleteAreaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await areaRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<bool>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                await areaRepository.DeleteAsync(entity);
                return new ResponseRDTO<bool>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<bool>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = ex.Message,
                    Detail = ex.ToString(),
                };
            }
        }
    }
}
