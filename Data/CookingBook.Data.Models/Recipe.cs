﻿namespace CookingBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CookingBook.Data.Common.Models;

    public class Recipe : BaseDeletableModel<string>
    {
        public Recipe()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Reviews = new HashSet<Review>();
            this.CookedBy = new HashSet<UserCookedRecipe>();
            this.FavoriteBy = new HashSet<UserFavoriteRecipe>();
            this.Products = new HashSet<Product>();
        }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string Photo { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }

        [Required]
        public string CookProcedure { get; set; }

        public int CookTime { get; set; }

        public int Serving { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string NutritionValueId { get; set; }

        public virtual NutritionValue NutritionValue { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<UserFavoriteRecipe> FavoriteBy { get; set; }

        public virtual ICollection<UserCookedRecipe> CookedBy { get; set; }
        
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
