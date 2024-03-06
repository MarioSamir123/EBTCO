namespace EBTCO.Core.Features.Employees.Queries.GetById
{
    public record GetEmployeeByIdQueryResponse(Guid Id, Guid OfficeId, String FirstName, String LastName, DateTime Birthday);
}
