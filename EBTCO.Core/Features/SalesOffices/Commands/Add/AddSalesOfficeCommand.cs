﻿using EBTCO.Core.Api;
using EBTCO.Core.Features.SalesOffices.DTO;
using EBTCO.Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EBTCO.Core.Features.SalesOffices.Commands.Add
{
    public record AddSalesOfficeCommand([MinLength(3)] String OfficeName, AddressDto Address) : IRequest<APIResponse<AddSalesOfficeCommandResponse>>
    {
        public SalesOffice Map()
        {
            return new SalesOffice
            {
                OfficeName = OfficeName.Trim(),
                Address = Address.Map(),
                Timestamp = DateTime.UtcNow,
            };
        }
    }
}
