using Labour.MS.Establishment.Models.Data;
using Labour.MS.Establishment.Models.Data.Common;

namespace Labour.MS.Establishment.Models.DTOs.Request
{
    public class EstablishmentDetailsRequest
    {
        public long? EstablishmentId { get; set; }
        public EstablishmentDetail? EstablishmentDetails { get; set; }
        public AddressDetail? AddressDetails { get; set; }
        public EstablishmentBusinessDetail? BusinessDetails { get; set; }
        public EstablishmentConstructionDetail? ConstructionDetails { get; set; }
        public string? IsAcceptedTermsAndConditions { get; set; }
    }
}
