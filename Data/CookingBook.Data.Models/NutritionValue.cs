namespace CookingBook.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CookingBook.Data.Common.Models;

    public class NutritionValue : BaseDeletableModel<string>
    {
        public NutritionValue()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        
        public int Calories { get; set; }

        public int Fats { get; set; }

        public int Carbohydrates { get; set; }

        public int Sugar { get; set; }

        public int Protein { get; set; }

        public int Fiber { get; set; }

        public int Salt { get; set; }

        [Required]
        public string RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
