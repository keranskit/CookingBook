namespace CookingBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using CookingBook.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        [Required]
        public string Title { get; set; }

        public string Image { get; set; }
    }
}
