using System.ComponentModel;

namespace FMAPI.Helpers
{
    public abstract class SoftDelete
    {
        [DefaultValue(null)]
        public DateTime? Deleted_at { get; set; }

        public DateTime Create_at { get; set; }

        public DateTime? Update_at { get; set; }
    }
}
