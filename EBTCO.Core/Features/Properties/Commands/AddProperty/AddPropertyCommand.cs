using EBTCO.Core.Api;
using EBTCO.Domain;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EBTCO.Core.Features.Properties.Commands.AddProperty
{
    public record AddPropertyCommand(
        Guid officeId, 
        String City, 
        [Range(1, 1000)]int NoOfBedroom, 
        [Range(1, 1000)] int NoOfBathroom,
        [Range(0, double.MaxValue -1)] double PriceFrom,
        [Range(0, double.MaxValue - 1)] double PriceTo) : IRequest<APIResponse<AddPropertyCommandResponse>>
    {
        public Property MapToDb()
        {
            return new Property
            {
                City = City,
                IsDeleted = false,
                NoBathrooms = NoOfBathroom,
                NoBedrooms = NoOfBedroom,
                PriceFrom = (decimal)PriceFrom,
                PriceTo = (decimal)PriceTo,
                OfficeID = officeId,
                Timestamp = DateTime.UtcNow,
                Status = PropStatus.Active,
            };
        }
    }
}
