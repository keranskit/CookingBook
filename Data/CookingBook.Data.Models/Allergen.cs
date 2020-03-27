namespace CookingBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CookingBook.Data.Common.Models;

    public class Allergen : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
