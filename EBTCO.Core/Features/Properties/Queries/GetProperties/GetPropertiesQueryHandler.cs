using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.Properties.Queries.GetProperties
{
    public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, APIResponse<GetPropertiesQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPropertiesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<GetPropertiesQueryResponse>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = await _unitOfWork.GetRepository<Property>()
                .GetSource()
                .AsNoTracking()
                .Where(request.GetFilter())
                .Select(row => new PropertyItem(row.ID,
                    row.PriceFrom,
                    row.PriceTo,
                    row.NoBedrooms,
                    row.NoBathrooms,
                    row.Status,
                    row.City,
                    row.OwningProgress))
                .ToListAsync();
            return new APIResponse<GetPropertiesQueryResponse>()
            {
                Data = new GetPropertiesQueryResponse(properties),
                HttpStatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
