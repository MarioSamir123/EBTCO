﻿using EBTCO.DB.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBTCO.DB
{
    public class Employee : Entity
    {
        public required Name Name { get; set; }
        public Guid OfficeID { get; set; }
        [ForeignKey("OfficeID")]
        public SalesOffice? SalesOffice { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
    }
}
