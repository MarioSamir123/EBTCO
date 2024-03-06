using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EBTCO.Core.Features.Owners.Commands.Edit
{
    public class EditOwnerCommandHandler : IRequestHandler<EditOwnerCommand, APIResponse<EditOwnerCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public EditOwnerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<EditOwnerCommandResponse>> Handle(EditOwnerCommand request, CancellationToken cancellationToken)
        {
            var ownerRepo = _unitOfWork.GetRepository<Owner>();
            var owner = await ownerRepo.GetSource()
                .FirstOrDefaultAsync(row => !row.IsDeleted && row.ID.Equals(request.Id));

            if (owner == null)
            {
                return new APIResponse<EditOwnerCommandResponse>
                {
                    Errors = new List<string> { "This Owner is not Found!" },
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound
                };
            }
            owner.Name.FirstName = request.FirstName;
            owner.Name.LastName = request.LastName;
            owner.Timestamp = DateTime.UtcNow;
            ownerRepo.Update(owner);
            await _unitOfWork.SaveChangesAsync();
            return new APIResponse<EditOwnerCommandResponse>
            {
                Data = new EditOwnerCommandResponse(owner.ID),
                HttpStatusCode = System.Net.HttpStatusCode.OK,
            };
        }
    }
}