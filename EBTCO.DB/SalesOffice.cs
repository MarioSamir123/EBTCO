using EBTCO.Domain.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBTCO.Domain
{
    public class SalesOffice : Entity
    {
        public required String OfficeName { get; set; }
        public Guid AddressID { get; set; }
        [ForeignKey("AddressID")]
        public required Address Address { get; set; }
        public Guid ManagerEmpID { get; set; }
        public int NoOfProperty { get; set; }
        public String ManagerName { get; set; } = String.Empty;
    }
}