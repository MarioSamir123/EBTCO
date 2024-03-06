namespace EBTCO.Core.Features.Owners.Queries.GetById
{
    public record GetOwnerByIdQueryResponse(Guid ownerId, String FirstName, String LastName);
}
