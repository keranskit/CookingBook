namespace CookingBook.Web.ViewModels.Profile
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class RecipeToDelete : IMapFrom<Recipe>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Photo { get; set; }

        public string CookProcedure { get; set; }

        public int CookTime { get; set; }

        public int Serving { get; set; }

        public int CategoryId { get; set; }

        public string NutritionValueId { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}
