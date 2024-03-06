using System.ComponentModel.DataAnnotations;

namespace EBTCO.Domain.Abstracts
{
    public abstract class Entity
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
