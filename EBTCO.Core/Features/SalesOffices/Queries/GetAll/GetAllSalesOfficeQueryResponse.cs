using EBTCO.Core.Features.SalesOffices.DTO;

namespace EBTCO.Core.Features.SalesOffices.Queries.GetAll
{
    public record GetAllSalesOfficeQueryResponse(List<SalesOfficeDto> SalesOffices);
}
