using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.Owners.Queries.GetById
{
    public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, APIResponse<GetOwnerByIdQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetOwnerByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<GetOwnerByIdQueryResponse>> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            var owner = await _unitOfWork.GetRepository<Owner>()
                .GetSource()
                .AsNoTracking()
                .Where(row => !row.IsDeleted && row.ID.Equals(request.Id))
                .Select(row => new GetOwnerByIdQueryResponse(row.ID, row.Name.FirstName, row.Name.LastName))
                .FirstOrDefaultAsync();

            if (owner == null)
            {
                return new APIResponse<GetOwnerByIdQueryResponse>
                {
                    Errors = new List<string> { "This Owner is not Found!" },
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound
                };
            }
            return new APIResponse<GetOwnerByIdQueryResponse>
            {
                Data = owner,
                HttpStatusCode = System.Net.HttpStatusCode.OK,
            };
        }
    }
}
