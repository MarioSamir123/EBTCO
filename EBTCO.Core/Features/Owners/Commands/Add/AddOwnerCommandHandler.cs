using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Domain;
using MediatR;

namespace EBTCO.Core.Features.Owners.Commands.Add
{
    public class AddOwnerCommandHandler : IRequestHandler<AddOwnerCommand, APIResponse<AddOwnerCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddOwnerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<AddOwnerCommandResponse>> Handle(AddOwnerCommand request, CancellationToken cancellationToken)
        {
            var owner = request.Map();
            
            await _unitOfWork.GetRepository<Owner>().AddAsync(owner);
            await _unitOfWork.SaveChangesAsync();

            return new APIResponse<AddOwnerCommandResponse>()
            {
                Data = new AddOwnerCommandResponse(owner.ID), 
                HttpStatusCode = System.Net.HttpStatusCode.Created,
            };
        }
    }
}
