using AutoMapper;
using MediatR;
using OrganizationService.Application.Contracts.IRepositories;
using OrganizationService.Application.DTO.LegalFormDTO;
using OrganizationService.Application.DTO.ResponseDTO;
using OrganizationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.Features.LegalForm
{
    public class DeleteLegalFormCommand : IRequest<ResponseRDTO<bool>>
    {
        public DeleteLegalFormCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class DeleteLegalFormCommandHandler : IRequestHandler<DeleteLegalFormCommand, ResponseRDTO<bool>>
    {
        private readonly ILegalFormRepository LegalFormRepository;
        private readonly IMapper mapper;

        public DeleteLegalFormCommandHandler(ILegalFormRepository LegalFormRepository, IMapper mapper)
        {
            this.LegalFormRepository = LegalFormRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<bool>> Handle(DeleteLegalFormCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await LegalFormRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<bool>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                await LegalFormRepository.DeleteAsync(entity);
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
