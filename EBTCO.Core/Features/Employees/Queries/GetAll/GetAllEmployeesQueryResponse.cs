namespace EBTCO.Core.Features.Employees.Queries.GetAll
{

    public record EmployeesDto(Guid Id, Guid OfficeId, String FirstName, String LastName, String OfficeName, DateOnly Birthday);
    public record GetAllEmployeesQueryResponse(List<EmployeesDto> Employees);
}
