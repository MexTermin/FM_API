#nullable disable

namespace FMAPI.DTOS
{
    public class CategoryDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Deleted { get; set; }
    }
}
