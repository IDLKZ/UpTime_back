using AutoMapper;
using OrganizationService.Application.DTO.AreaDTO;
using OrganizationService.Application.DTO.BaseDTO;
using OrganizationService.Application.DTO.BranchDTO;
using OrganizationService.Application.DTO.InfoDTO;
using OrganizationService.Application.DTO.InfoTypeDTO;
using OrganizationService.Application.DTO.LegalFormDTO;
using OrganizationService.Application.DTO.OrganizationDTO;
using OrganizationService.Application.DTO.UserOrganizationDTO;
using OrganizationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Application.DTO
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            //Base
            CreateMap<BaseModel, BaseRDTO>().ReverseMap();
            //Areas
            CreateMap<AreaModel, AreaRDTO>().ReverseMap();
            CreateMap<AreaModel, AreaCDTO>().ReverseMap();
            CreateMap<AreaModel, AreaUDTO>().ReverseMap();
            //Legal Form
            CreateMap<LegalFormModel, LegalFormRDTO>().ReverseMap();
            CreateMap<LegalFormModel, LegalFormCDTO>().ReverseMap();
            CreateMap<LegalFormModel, LegalFormUDTO>().ReverseMap();
            //InfoType 
            CreateMap<InfoTypeModel, InfoTypeRDTO>().ReverseMap();
            CreateMap<InfoTypeModel, InfoTypeCDTO>().ReverseMap();
            CreateMap<InfoTypeModel, InfoTypeUDTO>().ReverseMap();
            //Organization
            CreateMap<OrganizationModel, OrganizationRDTO>().ReverseMap();
            CreateMap<OrganizationModel, OrganizationCDTO>().ReverseMap();
            CreateMap<OrganizationModel, OrganizationUDTO>().ReverseMap();
            //Branch
            CreateMap<BranchModel, BranchRDTO>().ReverseMap();
            CreateMap<BranchModel, BranchCDTO>().ReverseMap();
            CreateMap<BranchModel, BranchUDTO>().ReverseMap();
            //Info
            CreateMap<InfoModel, InfoRDTO>().ReverseMap();
            CreateMap<InfoModel, InfoCDTO>().ReverseMap();
            CreateMap<InfoModel, InfoUDTO>().ReverseMap();
            //UserOrganization
            CreateMap<UserOrganizationModel, UserOrganizationRDTO>().ReverseMap();
            CreateMap<UserOrganizationModel, UserOrganizationCDTO>().ReverseMap();
            CreateMap<UserOrganizationModel, UserOrganizationUDTO>().ReverseMap();

        }
    }
}
