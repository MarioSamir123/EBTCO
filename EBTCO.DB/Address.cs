using EBTCO.Domain.Abstracts;

namespace EBTCO.Domain
{
    public class Address : Entity
    {
        public required String BuildingNo { get; set; }
        public required String Street { get; set; }
        public required String City { get; set; }
        public required String State { get; set; }
        public required String ZipCode { get; set; }
    }
}
