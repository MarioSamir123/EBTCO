using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Core.Features.SalesOffices.DTO;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.SalesOffices.Queries.GetById
{
    public class GetSalseOfficeByIdQueryHandler : IRequestHandler<GetSalseOfficeByIdQuery, APIResponse<GetSalseOfficeByIdQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetSalseOfficeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<GetSalseOfficeByIdQueryResponse>> Handle(GetSalseOfficeByIdQuery request, CancellationToken cancellationToken)
        {
            var salesOffice = await _unitOfWork.GetRepository<SalesOffice>()
                .GetSource()
                .AsNoTracking()
                .Include(row => row.Address)
                .Where(row => row.ID.Equals(request.Id) && !row.IsDeleted)
                .Select(row => new SalesOfficeDto(
                    row.ID,
                    row.OfficeName,
                    new AddressDto(
                        row.Address.BuildingNo,
                        row.Address.Street,
                        row.Address.City,
                        row.Address.State,
                        row.Address.ZipCode), 
                    row.NoOfProperty,
                    row.ManagerName)).FirstOrDefaultAsync();

            if (salesOffice == null)
            {
                return new APIResponse<GetSalseOfficeByIdQueryResponse>
                {
                    Errors = new List<string> { "This office is not found!" },
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound,
                };
            }

            return new APIResponse<GetSalseOfficeByIdQueryResponse>
            {
                Data = new GetSalseOfficeByIdQueryResponse(salesOffice),
                HttpStatusCode = System.Net.HttpStatusCode.OK,
            };
        }
    }
}
