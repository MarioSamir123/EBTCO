using EBTCO.Domain.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBTCO.Domain
{
    public class PropOwner : Entity
    {
        public Guid PropertyID { get; set; }
        [ForeignKey("PropertyID")]
        public Property? Property { get; set; }
        public Guid OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public Owner? Owner { get; set; }
        public int PercentOwned { get; set; }
    }
}
