using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Core.Features.SalesOffices.DTO;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.SalesOffices.Queries.GetAll
{
    public class GetAllSalesOfficeQueryHandler : IRequestHandler<GetAllSalesOfficeQuery, APIResponse<GetAllSalesOfficeQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllSalesOfficeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<GetAllSalesOfficeQueryResponse>> Handle(GetAllSalesOfficeQuery request, CancellationToken cancellationToken)
        {
            var salesOffices = await _unitOfWork.GetRepository<SalesOffice>()
                .GetSource()
                .AsNoTracking()
                .Include(row => row.Address)
                .Where(row => !row.IsDeleted)
                .Select(row => new SalesOfficeDto(
                    row.ID,
                    row.OfficeName,
                    new AddressDto(
                        row.Address.BuildingNo,
                        row.Address.Street,
                        row.Address.City,
                        row.Address.State,
                        row.Address.ZipCode)
                )).ToListAsync();

            return new APIResponse<GetAllSalesOfficeQueryResponse>
            {
                Data = new GetAllSalesOfficeQueryResponse(salesOffices),
                HttpStatusCode = salesOffices.Count == 0 ? System.Net.HttpStatusCode.NoContent : System.Net.HttpStatusCode.OK
            };
        }
    }
}