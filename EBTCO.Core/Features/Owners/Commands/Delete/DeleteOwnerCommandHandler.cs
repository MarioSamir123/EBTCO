using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.Owners.Commands.Delete
{
    public class DeleteOwnerCommandHandler : IRequestHandler<DeleteOwnerCommand, APIResponse<DeleteOwnerCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteOwnerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<DeleteOwnerCommandResponse>> Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
        {
            var ownerRepo = _unitOfWork.GetRepository<Owner>();
            var owner = await ownerRepo.GetSource()
                .FirstOrDefaultAsync(row => !row.IsDeleted && row.ID.Equals(request.ownerId));

            if (owner == null)
            {
                return new APIResponse<DeleteOwnerCommandResponse>
                {
                    Errors = new List<string> { "This Owner is not Found!" },
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound
                };
            }
            owner.IsDeleted = true;
            owner.Timestamp = DateTime.UtcNow;
            ownerRepo.Update(owner);
            await _unitOfWork.SaveChangesAsync();

            return new APIResponse<DeleteOwnerCommandResponse>
            {
                Data = new DeleteOwnerCommandResponse("Owner Deleted Successfully!"),
                HttpStatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
