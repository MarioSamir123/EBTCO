using EBTCO.DB.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBTCO.DB
{
    public class PropOwner : Entity
    {
        public Guid PropertyID { get; set; }
        [ForeignKey("PropertyID")]
        public Property? Property { get; set; }
        public int PercentOwned { get; set; }
    }
}
