using EBTCO.DB.Abstracts;

namespace EBTCO.DB
{
    public class Owner : Entity
    {
        public required Name Name { get; set; }
    }
}
