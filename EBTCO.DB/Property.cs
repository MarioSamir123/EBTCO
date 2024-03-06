﻿using EBTCO.Domain.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBTCO.Domain
{
    public class Property : Entity
    {
        public Decimal Price { get; set; }
        public bool Status { get; set; }
        public int NoBedrooms { get; set; }
        public int NoBathrooms { get; set; }
        public required String City { get; set; }
        public Guid OfficeID { get; set; }
        [ForeignKey("OfficeID")]
        public SalesOffice? SalesOffice { get; set; }
        public int OwningProgress { get; set; }
    }
}
