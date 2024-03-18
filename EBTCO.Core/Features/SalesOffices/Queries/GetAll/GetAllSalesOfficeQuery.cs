using EBTCO.Core.Api;
using EBTCO.Domain;
using MediatR;

namespace EBTCO.Core.Features.SalesOffices.Queries.GetAll
{
    public record GetAllSalesOfficeQuery(SalesOfficeSortingBy SortingBy, SortingDir SortingDir) : IRequest<APIResponse<GetAllSalesOfficeQueryResponse>> 
    { 
        public IQueryable<SalesOffice> Order(IQueryable<SalesOffice> salesOffices)
        {
            return SortingBy switch
            {
                SalesOfficeSortingBy.OfficeName => SortingDir switch
                {
                    SortingDir.DESC => salesOffices.OrderByDescending(row => row.OfficeName),
                    SortingDir.ASC => salesOffices.OrderBy(row => row.OfficeName),
                    _ => salesOffices,
                },

                SalesOfficeSortingBy.Address => SortingDir switch
                {
                    SortingDir.DESC => salesOffices
                        .OrderByDescending(row => row.Address.State)
                        .ThenByDescending(row => row.Address.City)
                        .ThenByDescending(row => row.Address.Street)
                        .ThenByDescending(row => row.Address.BuildingNo),
                    
                    SortingDir.ASC => salesOffices
                        .OrderBy(row => row.Address.State)
                        .ThenBy(row => row.Address.City)
                        .ThenBy(row => row.Address.Street)
                        .ThenBy(row => row.Address.BuildingNo),
                    _ => salesOffices,
                },
                SalesOfficeSortingBy.NoOfProperties => SortingDir switch
                {
                    SortingDir.DESC => salesOffices.OrderByDescending(row => row.NoOfProperty),
                    SortingDir.ASC => salesOffices.OrderBy(row => row.NoOfProperty),
                    _ => salesOffices,
                },
                SalesOfficeSortingBy.ManagerName => SortingDir switch
                {
                    SortingDir.DESC => salesOffices.OrderByDescending(row => row.ManagerName),
                    SortingDir.ASC => salesOffices.OrderBy(row => row.ManagerName),
                    _ => salesOffices,
                },
                _ => salesOffices
            };
        }
    }
}
