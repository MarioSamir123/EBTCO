using EBTCO.Domain;

namespace EBTCO.Core.Features.SalesOffices.DTO
{
    public record AddressDto(String BuildingNo, String Street, String City, String State, String ZipCode)
    {
        public Address Map()
        {
            return new Address
            {
                BuildingNo = BuildingNo.Trim(),
                Street = Street.Trim(),
                City = City.Trim(),
                State = State.Trim(),
                ZipCode = ZipCode.Trim(),
                Timestamp = DateTime.UtcNow,
            };
        }
    }
}