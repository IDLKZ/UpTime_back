using AutoMapper;
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
    public class DeleteInfoTypeCommand : IRequest<ResponseRDTO<bool>>
    {
        public DeleteInfoTypeCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class DeleteInfoTypeCommandHandler : IRequestHandler<DeleteInfoTypeCommand, ResponseRDTO<bool>>
    {
        private readonly IInfoTypeRepository InfoTypeRepository;
        private readonly IMapper mapper;

        public DeleteInfoTypeCommandHandler(IInfoTypeRepository InfoTypeRepository, IMapper mapper)
        {
            this.InfoTypeRepository = InfoTypeRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<bool>> Handle(DeleteInfoTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await InfoTypeRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<bool>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                await InfoTypeRepository.DeleteAsync(entity);
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
