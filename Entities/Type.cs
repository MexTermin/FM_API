using System.ComponentModel.DataAnnotations;

namespace FMAPI.Entities
{
    public class Type
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
