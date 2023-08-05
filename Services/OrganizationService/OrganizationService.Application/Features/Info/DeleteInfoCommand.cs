using AutoMapper;
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
    public class DeleteInfoCommand : IRequest<ResponseRDTO<bool>>
    {
        public DeleteInfoCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class DeleteInfoCommandHandler : IRequestHandler<DeleteInfoCommand, ResponseRDTO<bool>>
    {
        private readonly IInfoRepository InfoRepository;
        private readonly IMapper mapper;

        public DeleteInfoCommandHandler(IInfoRepository InfoRepository, IMapper mapper)
        {
            this.InfoRepository = InfoRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<bool>> Handle(DeleteInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await InfoRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<bool>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                await InfoRepository.DeleteAsync(entity);
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
