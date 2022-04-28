﻿using System.ComponentModel.DataAnnotations;

namespace FMAPI.Entities
{
    public class Category
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
