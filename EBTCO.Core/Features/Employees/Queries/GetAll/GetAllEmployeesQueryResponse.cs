namespace EBTCO.Core.Features.Employees.Queries.GetAll
{

    public record EmployeesDto(Guid Id, Guid OfficeId, String FirstName, String LastName, String OfficeName, DateTime Birthday);
    public record GetAllEmployeesQueryResponse(List<EmployeesDto> Employees);
}
