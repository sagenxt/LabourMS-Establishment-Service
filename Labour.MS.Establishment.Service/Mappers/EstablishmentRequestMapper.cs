using AutoMapper;
using Labour.MS.Establishment.Models.DTOs.Request;
using Labour.MS.Establishment.Models.Proxy.Request;

namespace Labour.MS.Establishment.Service.Mappers
{
    public class EstablishmentRequestMapper : Profile
    {
        public EstablishmentRequestMapper()
        {
            CreateMap<EstablishmentDetailsRequest, EstablishmentDetailsAdapterRequest>()
                .ForMember(dest => dest.EstablishmentId, opt => opt.MapFrom(src => src.EstablishmentId))
                //.ForMember(dest => dest.EstablishmentName, opt => opt.MapFrom(src => src.EstablishmentDetails != null ? src.EstablishmentDetails.EstablishmentName : null))
                //.ForMember(dest => dest.ContactPerson, opt => opt.MapFrom(src => src.EstablishmentDetails != null ? src.EstablishmentDetails.ContactPerson : null))
                //.ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.EstablishmentDetails != null ? src.EstablishmentDetails.MobileNumber : null))
                //.ForMember(dest => dest.EmailId, opt => opt.MapFrom(src => src.EstablishmentDetails != null ? src.EstablishmentDetails.EmailId : null))

                //.ForMember(dest => dest.DoorNumber, opt => opt.MapFrom(src => src.AddressDetails != null ? src.AddressDetails.DoorNumber : null))
                //.ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.AddressDetails != null ? src.AddressDetails.Street : null))
                //.ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.AddressDetails != null ? src.AddressDetails.StateId : null))
                //.ForMember(dest => dest.StateCode, opt => opt.MapFrom(src => src.AddressDetails != null ? src.AddressDetails.StateCode : null))
                //.ForMember(dest => dest.DistrictId, opt => opt.MapFrom(src => src.AddressDetails != null ? src.AddressDetails.DistrictId : null))
                //.ForMember(dest => dest.DistrictCode, opt => opt.MapFrom(src => src.AddressDetails != null ? src.AddressDetails.DistrictCode : null))
                //.ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.AddressDetails != null ? src.AddressDetails.CityId : null))
                //.ForMember(dest => dest.CityCode, opt => opt.MapFrom(src => src.AddressDetails != null ? src.AddressDetails.CityCode : null))
                //.ForMember(dest => dest.VillageOrArea, opt => opt.MapFrom(src => src.AddressDetails != null ? src.AddressDetails.VillageOrArea : null))
                //.ForMember(dest => dest.Pincode, opt => opt.MapFrom(src => src.AddressDetails != null ? src.AddressDetails.Pincode : null))

                //.ForMember(dest => dest.IsPlanApprovalId, opt => opt.MapFrom(src => src.BusinessDetails != null ? src.BusinessDetails.IsPlanApprovalId : null))
                //.ForMember(dest => dest.PlanApprovalId, opt => opt.MapFrom(src => src.BusinessDetails != null ? src.BusinessDetails.PlanApprovalId : null))
                //.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.BusinessDetails != null ? src.BusinessDetails.CategoryId : null))
                //.ForMember(dest => dest.WorkNatureId, opt => opt.MapFrom(src => src.BusinessDetails != null ? src.BusinessDetails.WorkNatureId : null))
                //.ForMember(dest => dest.CommencementDate, opt => opt.MapFrom(src => src.BusinessDetails != null ? src.BusinessDetails.CommencementDate : null))
                //.ForMember(dest => dest.CompletionDate, opt => opt.MapFrom(src => src.BusinessDetails != null ? src.BusinessDetails.CompletionDate : null))

                //.ForMember(dest => dest.ConstructionEstimatedCost, opt => opt.MapFrom(src => src.ConstructionDetails != null ? src.ConstructionDetails.ConstructionEstimatedCost : null))
                //.ForMember(dest => dest.ConstructionArea, opt => opt.MapFrom(src => src.ConstructionDetails != null ? src.ConstructionDetails.ConstructionArea : null))
                //.ForMember(dest => dest.BuiltUpArea, opt => opt.MapFrom(src => src.ConstructionDetails != null ? src.ConstructionDetails.BuiltUpArea : null))
                //.ForMember(dest => dest.BasicEstimatedCost, opt => opt.MapFrom(src => src.ConstructionDetails != null ? src.ConstructionDetails.BasicEstimatedCost : null))
                //.ForMember(dest => dest.NoOfMaleWorkers, opt => opt.MapFrom(src => src.ConstructionDetails != null ? src.ConstructionDetails.NoOfMaleWorkers : null))
                //.ForMember(dest => dest.NoOfFemaleWorkers, opt => opt.MapFrom(src => src.ConstructionDetails != null ? src.ConstructionDetails.NoOfFemaleWorkers : null))

                .ForMember(dest => dest.IsAcceptedTermsAndConditions, opt => opt.MapFrom(src => src.IsAcceptedTermsAndConditions));

            CreateMap<EstablishmentRequest, EstablishmentAdapterRequest>()
                .ForMember(dest => dest.EstablishmentId, opt => opt.MapFrom(src => src.EstablishmentId));
        }
    }
}
