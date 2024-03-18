using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.SalesOffices.Queries.GetOfficeNames
{
    public class GetOfficeNamesQueryHandler : IRequestHandler<GetOfficeNamesQuery, APIResponse<GetOfficeNamesQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOfficeNamesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<APIResponse<GetOfficeNamesQueryResponse>> Handle(GetOfficeNamesQuery request, CancellationToken cancellationToken)
        {
            var offices = await _unitOfWork.GetRepository<SalesOffice>()
                .GetSource()
                .AsNoTracking()
                .ToDictionaryAsync(row => row.ID.ToString(), row => row.OfficeName);

            return new APIResponse<GetOfficeNamesQueryResponse>
            {
                Data = new GetOfficeNamesQueryResponse(offices),
                HttpStatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
