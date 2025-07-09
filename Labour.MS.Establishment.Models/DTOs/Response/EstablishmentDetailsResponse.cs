using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labour.MS.Establishment.Models.Data.Common;
using Labour.MS.Establishment.Models.Data;

namespace Labour.MS.Establishment.Models.DTOs.Response
{
    public class EstablishmentDetailsResponse
    {
        public long? EstablishmentId { get; set; }
        public EstablishmentDetail? EstablishmentDetails { get; set; }
        public AddressDetail? AddressDetails { get; set; }
        public EstablishmentBusinessDetail? BusinessDetails { get; set; }
        public EstablishmentConstructionDetail? ConstructionDetails { get; set; }
    }
}
