namespace EBTCO.Core.Features.Owners.Queries.GetAll
{
    public record OwnerDto(Guid Id, String FirstName, String LastName);
    public record GetAllOwnersQueryResponse(List<OwnerDto> Owners);
}
