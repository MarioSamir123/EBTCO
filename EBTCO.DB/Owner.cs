using EBTCO.Domain.Abstracts;

namespace EBTCO.Domain
{
    public class Owner : Entity
    {
        public required Name Name { get; set; }
    }
}
