namespace CookingBook.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CookingBook.Data.Common.Models;

    public class Review : BaseDeletableModel<string>
    {
        public Review()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Comment { get; set; }

        public string RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
