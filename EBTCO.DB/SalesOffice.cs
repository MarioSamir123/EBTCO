using EBTCO.DB.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBTCO.DB
{
    public class SalesOffice : Entity
    {
        public required String OfficeName { get; set; }
        public Guid AddressID { get; set; }
        [ForeignKey("AddressID")]
        public Address? Address { get; set; }
        public Guid? ManagerEmpID { get; set; }
        [ForeignKey("ManagerEmpID")]
        public Employee? ManagerEmp { get; set; }
    }
}