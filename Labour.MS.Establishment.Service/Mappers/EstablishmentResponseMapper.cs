using AutoMapper;
using Labour.MS.Establishment.Models.DTOs.Response;
using Labour.MS.Establishment.Models.Proxy.Response;

namespace Labour.MS.Establishment.Service.Mappers
{
    public class EstablishmentResponseMapper : Profile
    {
        public EstablishmentResponseMapper()
        {
            CreateMap<EstablishmentDetailsAdapterResponse, EstablishmentDetailsResponse>()
                .ForMember(dest => dest.EstablishmentId, opt => opt.MapFrom(src => src.EstablishmentId));
                //.ForMember(dest => dest.EstablishmentDetails != null ? dest.EstablishmentDetails.EstablishmentName : null, opt => opt.MapFrom(src => src.EstablishmentName))
                //.ForMember(dest => dest.EstablishmentDetails != null ? dest.EstablishmentDetails.ContactPerson : null, opt => opt.MapFrom(src => src.ContactPerson))
                //.ForMember(dest => dest.EstablishmentDetails != null ? dest.EstablishmentDetails.MobileNumber : null, opt => opt.MapFrom(src => src.MobileNumber))
                //.ForMember(dest => dest.EstablishmentDetails != null ? dest.EstablishmentDetails.EmailId : null, opt => opt.MapFrom(src => src.EmailId))

                //.ForMember(dest => dest.AddressDetails != null ? dest.AddressDetails.DoorNumber : null, opt => opt.MapFrom(src => src.DoorNumber))
                //.ForMember(dest => dest.AddressDetails != null ? dest.AddressDetails.Street : null, opt => opt.MapFrom(src => src.Street))
                //.ForMember(dest => dest.AddressDetails != null ? dest.AddressDetails.StateId : null, opt => opt.MapFrom(src => src.StateId))
                //.ForMember(dest => dest.AddressDetails != null ? dest.AddressDetails.StateCode : null, opt => opt.MapFrom(src => src.StateCode))
                //.ForMember(dest => dest.AddressDetails != null ? dest.AddressDetails.StateName : null, opt => opt.MapFrom(src => src.StateName))
                //.ForMember(dest => dest.AddressDetails != null ? dest.AddressDetails.DistrictId : null, opt => opt.MapFrom(src => src.DistrictId))
                //.ForMember(dest => dest.AddressDetails != null ? dest.AddressDetails.DistrictCode : null, opt => opt.MapFrom(src => src.DistrictCode))
                //.ForMember(dest => dest.AddressDetails != null ? dest.AddressDetails.DistrictName : null, opt => opt.MapFrom(src => src.DistrictName))
                //.ForMember(dest => dest.AddressDetails != null ? dest.AddressDetails.CityId : null, opt => opt.MapFrom(src => src.CityId))
                //.ForMember(dest => dest.AddressDetails != null ? dest.AddressDetails.CityCode : null, opt => opt.MapFrom(src => src.CityCode))
                //.ForMember(dest => dest.AddressDetails != null ? dest.AddressDetails.CityName : null, opt => opt.MapFrom(src => src.CityName))
                //.ForMember(dest => dest.AddressDetails != null ? dest.AddressDetails.VillageOrArea : null, opt => opt.MapFrom(src => src.VillageOrArea))
                //.ForMember(dest => dest.AddressDetails != null ? dest.AddressDetails.Pincode : null, opt => opt.MapFrom(src => src.Pincode))

                //.ForMember(dest => dest.BusinessDetails != null ? dest.BusinessDetails.IsPlanApprovalId : null, opt => opt.MapFrom(src => src.IsPlanApprovalId))
                //.ForMember(dest => dest.BusinessDetails != null ? dest.BusinessDetails.PlanApprovalId : null, opt => opt.MapFrom(src => src.PlanApprovalId))
                //.ForMember(dest => dest.BusinessDetails != null ? dest.BusinessDetails.CategoryId : null, opt => opt.MapFrom(src => src.CategoryId))
                //.ForMember(dest => dest.BusinessDetails != null ? dest.BusinessDetails.CategoryName : null, opt => opt.MapFrom(src => src.CategoryName))
                //.ForMember(dest => dest.BusinessDetails != null ? dest.BusinessDetails.WorkNatureId : null, opt => opt.MapFrom(src => src.WorkNatureId))
                //.ForMember(dest => dest.BusinessDetails != null ? dest.BusinessDetails.WorkNatureName : null, opt => opt.MapFrom(src => src.WorkNatureName))
                //.ForMember(dest => dest.BusinessDetails != null ? dest.BusinessDetails.CommencementDate : null, opt => opt.MapFrom(src => src.CommencementDate))
                //.ForMember(dest => dest.BusinessDetails != null ? dest.BusinessDetails.CompletionDate : null, opt => opt.MapFrom(src => src.CompletionDate))

                //.ForMember(dest => dest.ConstructionDetails != null ? dest.ConstructionDetails.ConstructionEstimatedCost : null, opt => opt.MapFrom(src => src.ConstructionEstimatedCost))
                //.ForMember(dest => dest.ConstructionDetails != null ? dest.ConstructionDetails.ConstructionArea : null, opt => opt.MapFrom(src => src.ConstructionArea))
                //.ForMember(dest => dest.ConstructionDetails != null ? dest.ConstructionDetails.BuiltUpArea : null, opt => opt.MapFrom(src => src.BuiltUpArea))
                //.ForMember(dest => dest.ConstructionDetails != null ? dest.ConstructionDetails.BasicEstimatedCost : null, opt => opt.MapFrom(src => src.BasicEstimatedCost))
                //.ForMember(dest => dest.ConstructionDetails != null ? dest.ConstructionDetails.NoOfMaleWorkers : null, opt => opt.MapFrom(src => src.NoOfMaleWorkers))
                //.ForMember(dest => dest.ConstructionDetails != null ? dest.ConstructionDetails.NoOfFemaleWorkers : null, opt => opt.MapFrom(src => src.NoOfFemaleWorkers));

                //.ForMember(dest => dest.IsAcceptedTermsAndConditions, opt => opt.MapFrom(src => src.IsAcceptedTermsAndConditions));

            CreateMap<EstablishmentAdapterResponse, EstablishmentResponse>()
                .ForMember(dest => dest.StatusCode, opt => opt.MapFrom(src => src.StatusCode))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message));
        }
    }
}
