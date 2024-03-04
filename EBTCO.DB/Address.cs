﻿using EBTCO.DB.Abstracts;

namespace EBTCO.DB
{
    public class Address : Entity
    {
        public required String City { get; set; }
        public required String State { get; set; }
        public required String ZipCode { get; set; }
    }
}
