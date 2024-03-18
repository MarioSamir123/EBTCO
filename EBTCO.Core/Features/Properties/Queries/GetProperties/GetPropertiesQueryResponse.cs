using EBTCO.Domain;

namespace EBTCO.Core.Features.Properties.Queries.GetProperties
{
    public record PropertyItem(Guid Id, decimal PriceFrom, decimal PriceTo, int NoOfBedroom, int NoOfBathroom, PropStatus Status, String City, int OwningPer);
    public record GetPropertiesQueryResponse(List<PropertyItem> properties);
}
