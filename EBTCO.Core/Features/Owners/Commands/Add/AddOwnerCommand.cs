using EBTCO.Core.Api;
using EBTCO.Domain;
using MediatR;

namespace EBTCO.Core.Features.Owners.Commands.Add
{
    public record AddOwnerCommand(String FirstName, String LastName) : IRequest<APIResponse<AddOwnerCommandResponse>>
    {
        public Owner Map()
        {
            return new Owner
            {
                Name = new Domain.Abstracts.Name
                {
                    FirstName = FirstName,
                    LastName = LastName
                },
                Timestamp = DateTime.UtcNow,
            };
        }
    }
}
