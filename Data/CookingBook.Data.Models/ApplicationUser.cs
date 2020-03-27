// ReSharper disable VirtualMemberCallInConstructor
namespace CookingBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CookingBook.Data.Common.Models;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.Recipes = new HashSet<Recipe>();
            this.Reviews = new HashSet<Review>();
            this.Allergens = new HashSet<UserAllergen>();
            this.CookedRecipes = new HashSet<UserCookedRecipe>();
            this.FavoriteRecipes = new HashSet<UserFavoriteRecipe>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        [Required]
        public string Name { get; set; }

        public string ProfilePhoto { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }

        public virtual ICollection<UserAllergen> Allergens { get; set; }

        public virtual ICollection<UserCookedRecipe> CookedRecipes { get; set; }

        public virtual ICollection<UserFavoriteRecipe> FavoriteRecipes { get; set; }
    }
}
