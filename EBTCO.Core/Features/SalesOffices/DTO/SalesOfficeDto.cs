namespace EBTCO.Core.Features.SalesOffices.DTO
{
    public record SalesOfficeDto(Guid ID, String OfficeName, AddressDto Address, int NoOfProperties, String ManagerName);
}
