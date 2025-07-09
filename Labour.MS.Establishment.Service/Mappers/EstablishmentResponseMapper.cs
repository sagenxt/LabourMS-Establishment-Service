using AutoMapper;
using Labour.MS.Establishment.Models.Data;
using Labour.MS.Establishment.Models.Data.Common;
using Labour.MS.Establishment.Models.DTOs.Response;
using Labour.MS.Establishment.Models.Proxy.Response;

namespace Labour.MS.Establishment.Service.Mappers
{
    public class EstablishmentResponseMapper : Profile
    {
        public EstablishmentResponseMapper()
        {
            CreateMap<EstablishmentAdapterResponse, EstablishmentResponse>()
                .ForMember(dest => dest.StatusCode, opt => opt.MapFrom(src => src.StatusCode))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message));
        }

        public static EstablishmentDetailsResponse MapToEstalishmentDetailsResponse(EstablishmenAdaptertDetail establishmenAdaptertDetail)
        {
            var response = new EstablishmentDetailsResponse
            {
                EstablishmentId = establishmenAdaptertDetail.EstablishmentId,
                EstablishmentDetails = new EstablishmentDetail
                {
                    EstablishmentName = establishmenAdaptertDetail.EstablishmentName,
                    ContactPerson = establishmenAdaptertDetail.ContactPerson,
                    MobileNumber = establishmenAdaptertDetail.MobileNumber,
                    EmailId = establishmenAdaptertDetail.EmailId
                },
                AddressDetails = new AddressDetail
                {
                    DoorNumber = establishmenAdaptertDetail.DoorNumber,
                    Street = establishmenAdaptertDetail.Street,
                    StateId = establishmenAdaptertDetail.StateId,
                    StateCode = establishmenAdaptertDetail.StateCode,
                    StateName = establishmenAdaptertDetail.StateName,
                    DistrictId = establishmenAdaptertDetail.DistrictId,
                    DistrictCode = establishmenAdaptertDetail.DistrictCode,
                    DistrictName = establishmenAdaptertDetail.DistrictName,
                    CityId = establishmenAdaptertDetail.CityId,
                    CityCode = establishmenAdaptertDetail.CityCode,
                    CityName = establishmenAdaptertDetail.CityName,
                    VillageOrAreaId = establishmenAdaptertDetail.VillageOrAreaId,
                    VillageOrAreaName = establishmenAdaptertDetail.VillageOrAreaName,
                    Pincode = establishmenAdaptertDetail.Pincode
                },
                BusinessDetails = new EstablishmentBusinessDetail
                {
                    IsPlanApprovalId = establishmenAdaptertDetail.IsPlanApprovalId,
                    PlanApprovalId = establishmenAdaptertDetail.PlanApprovalId,
                    CategoryId = establishmenAdaptertDetail.CategoryId,
                    CategoryName = establishmenAdaptertDetail.CategoryName,
                    WorkNatureId = establishmenAdaptertDetail.WorkNatureId,
                    WorkNatureName = establishmenAdaptertDetail.WorkNatureName,
                    CommencementDate = string.IsNullOrEmpty(establishmenAdaptertDetail.CommencementDate)
                        ? null
                        : DateOnly.Parse(establishmenAdaptertDetail.CommencementDate),
                    CompletionDate = string.IsNullOrEmpty(establishmenAdaptertDetail.CompletionDate)
                        ? null
                        : DateOnly.Parse(establishmenAdaptertDetail.CompletionDate)
                },
                ConstructionDetails = new EstablishmentConstructionDetail
                {
                    ConstructionEstimatedCost = establishmenAdaptertDetail.ConstructionEstimatedCost,
                    ConstructionArea = establishmenAdaptertDetail.ConstructionArea,
                    BuiltUpArea = establishmenAdaptertDetail.BuiltUpArea,
                    BasicEstimatedCost = establishmenAdaptertDetail.BasicEstimatedCost,
                    NoOfMaleWorkers = establishmenAdaptertDetail.NoOfMaleWorkers,
                    NoOfFemaleWorkers = establishmenAdaptertDetail.NoOfFemaleWorkers
                }
            };

            return response;
        }
    }
}
