using EBTCO.Core.Api;
using EBTCO.Core.Contract.DBRepo;
using EBTCO.Core.Features.Employees.Commands.Add;
using EBTCO.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EBTCO.Core.Features.Properties.Commands.OwnProperty
{
    record PropertyPercentage(int RemainingPer);
    public class OwnPropertyCommandHandler : IRequestHandler<OwnPropertyCommand, APIResponse<OwnPropertyCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public OwnPropertyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<APIResponse<OwnPropertyCommandResponse>> Handle(OwnPropertyCommand request, CancellationToken cancellationToken)
        {
            var ownerName = await GetOwnerName(request.ownerId);

            if (ownerName.IsNullOrEmpty())
            {
                return new APIResponse<OwnPropertyCommandResponse>
                {
                    Errors = new List<string> { "This Owner is not found!" },
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound
                };
            }
            var propertyQuery = _unitOfWork.GetRepository<Property>()
                .GetSource()
                .AsNoTracking()
                .Where(row => !row.IsDeleted && row.ID.Equals(request.propId));
            
            var property = await propertyQuery
                .Select(row => new PropertyPercentage(100 - row.OwningProgress))
                .FirstOrDefaultAsync();

            if (property == null)
            {
                return new APIResponse<OwnPropertyCommandResponse>
                {
                    Errors = new List<string> { "This Property is not found!" },
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            var propOwnerRepo = _unitOfWork.GetRepository<PropOwner>();
            if (request.percentage == 0)
            {
                await propOwnerRepo.GetSource()
                    .AsNoTracking()
                    .Where(row => row.PropertyID.Equals(request.propId) && row.OwnerID.Equals(request.ownerId))
                    .ExecuteDeleteAsync();
            }
            else
            {
                if (property.RemainingPer < request.percentage)
                {
                    return new APIResponse<OwnPropertyCommandResponse>
                    {
                        Errors = new List<string> { "Sorry, but this Property not have the Percentage you required" },
                        HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                    };
                }

                int rowEffected = await propOwnerRepo.GetSource()
                    .AsNoTracking()
                    .Where(row => row.PropertyID.Equals(request.propId) && row.OwnerID.Equals(request.ownerId))
                    .ExecuteUpdateAsync(row => row.SetProperty(p => p.PercentOwned, p => p.PercentOwned + request.percentage));

                int perSum = await propOwnerRepo.GetSource()
                    .AsNoTracking()
                    .Where(row => row.PropertyID.Equals(request.propId))
                    .SumAsync(row => row.PercentOwned);

                await _unitOfWork.GetRepository<Property>()
                    .GetSource()
                    .AsNoTracking()
                    .Where(row => row.ID.Equals(request.propId))
                    .ExecuteUpdateAsync(row => row.SetProperty(p => p.OwningProgress, perSum));

                if (perSum == 100)
                {
                    await _unitOfWork.GetRepository<Property>()
                    .GetSource()
                    .AsNoTracking()
                    .Where(row => row.ID.Equals(request.propId))
                    .ExecuteUpdateAsync(row => row.SetProperty(p => p.Status, PropStatus.Sold));
                }
                if (rowEffected == 0)
                {
                    await propOwnerRepo.AddAsync(new PropOwner
                    {
                        PercentOwned = request.percentage,
                        PropertyID = request.propId,
                        OwnerID = request.ownerId
                    });
                    await _unitOfWork.SaveChangesAsync();
                }
            }

            return new APIResponse<OwnPropertyCommandResponse>
            {
                Data = new OwnPropertyCommandResponse($"Now, Mr.{ownerName} has {request.percentage}% From this Property"),
                HttpStatusCode = System.Net.HttpStatusCode.OK
            };
        }
        private async Task<String?> GetOwnerName(Guid Id)
        {
            return await  _unitOfWork.GetRepository<Owner>()
                .GetSource()
                .AsNoTracking()
                .Where(row => !row.IsDeleted && row.ID.Equals(Id))
                .Select(row => $"{row.Name.FirstName} {row.Name.LastName}")
                .FirstOrDefaultAsync();
        }
    }
}
