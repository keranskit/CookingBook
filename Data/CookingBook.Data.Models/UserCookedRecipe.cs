namespace CookingBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserCookedRecipe
    {
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
