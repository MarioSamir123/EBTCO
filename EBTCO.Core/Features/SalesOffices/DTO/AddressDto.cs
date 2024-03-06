using EBTCO.Domain;

namespace EBTCO.Core.Features.SalesOffices.DTO
{
    public record AddressDto(String BuildingNo, String Street, String City, String State, String ZipCode)
    {
        public Address Map()
        {
            return new Address
            {
                BuildingNo = BuildingNo,
                Street = Street,
                City = City,
                State = State,
                ZipCode = ZipCode,
                Timestamp = DateTime.UtcNow,   
            };
        }
    }
}