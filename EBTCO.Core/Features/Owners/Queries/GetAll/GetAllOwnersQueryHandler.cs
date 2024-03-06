using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.Owners.Queries.GetAll
{
    public class GetAllOwnersQueryHandler : IRequestHandler<GetAllOwnersQuery, APIResponse<GetAllOwnersQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllOwnersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<GetAllOwnersQueryResponse>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
        {
            var owners = await _unitOfWork.GetRepository<Owner>()
                .GetSource()
                .AsNoTracking()
                .Where(row => !row.IsDeleted)
                .Select(row => new OwnerDto(row.ID, row.Name.FirstName, row.Name.LastName))
                .ToListAsync();

            return new APIResponse<GetAllOwnersQueryResponse>
            {
                Data = new GetAllOwnersQueryResponse(owners),
                HttpStatusCode = owners.Count == 0 ? System.Net.HttpStatusCode.NoContent : System.Net.HttpStatusCode.OK
            };
        }
    }
}
