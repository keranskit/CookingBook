﻿namespace CookingBook.Web.ViewModels.Recipes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class RecipeCreateProductsViewModel : IMapTo<Product>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal Quantity { get; set; }
    }
}
