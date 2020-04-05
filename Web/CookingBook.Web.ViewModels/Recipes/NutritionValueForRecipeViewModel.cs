namespace CookingBook.Web.ViewModels.Recipes
{
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class NutritionValueForRecipeViewModel : IMapFrom<NutritionValue>
    {
        public int Calories { get; set; }

        public int Fats { get; set; }

        public int Carbohydrates { get; set; }

        public int Sugar { get; set; }

        public int Protein { get; set; }

        public int Fiber { get; set; }

        public int Salt { get; set; }
    }
}
