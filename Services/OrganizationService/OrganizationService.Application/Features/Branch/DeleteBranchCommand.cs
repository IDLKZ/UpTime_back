using AutoMapper;
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
    public class DeleteBranchCommand : IRequest<ResponseRDTO<bool>>
    {
        public DeleteBranchCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand, ResponseRDTO<bool>>
    {
        private readonly IBranchRepository BranchRepository;
        private readonly IMapper mapper;

        public DeleteBranchCommandHandler(IBranchRepository BranchRepository, IMapper mapper)
        {
            this.BranchRepository = BranchRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<bool>> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await BranchRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<bool>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Not Found",
                    };
                }
                await BranchRepository.DeleteAsync(entity);
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
