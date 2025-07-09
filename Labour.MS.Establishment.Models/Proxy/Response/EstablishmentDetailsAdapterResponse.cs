﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labour.MS.Establishment.Models.Proxy.Response
{
    public class EstablishmentDetailsAdapterResponse
    {
        public EstablishmenAdaptertDetail? Data { get; set; }
    }

    public class EstablishmenAdaptertDetail
    {
        public long? EstablishmentId { get; set; }
        public string? EstablishmentName { get; set; }
        public string? ContactPerson { get; set; }
        public long? MobileNumber { get; set; }
        public string? EmailId { get; set; }
        public string? DoorNumber { get; set; }
        public string? Street { get; set; }
        public int? StateId { get; set; }
        public string? StateCode { get; set; }
        public string? StateName { get; set; }
        public int? DistrictId { get; set; }
        public string? DistrictCode { get; set; }
        public string? DistrictName { get; set; }
        public int? CityId { get; set; }
        public string? CityCode { get; set; }
        public string? CityName { get; set; }
        public int? VillageOrAreaId { get; set; }
        public string? VillageOrAreaName { get; set; }
        public int? Pincode { get; set; }
        public string? IsPlanApprovalId { get; set; }
        public string? PlanApprovalId { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? WorkNatureId { get; set; }
        public string? WorkNatureName { get; set; }
        public string? CommencementDate { get; set; }
        public string? CompletionDate { get; set; }
        public decimal? ConstructionEstimatedCost { get; set; }
        public decimal? ConstructionArea { get; set; }
        public decimal? BuiltUpArea { get; set; }
        public decimal? BasicEstimatedCost { get; set; }
        public int? NoOfMaleWorkers { get; set; }
        public int? NoOfFemaleWorkers { get; set; }

    }
}
