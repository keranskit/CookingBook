namespace CookingBook.Web.ViewModels.Recipes
{
    using Data.Models;
    using Services.Mapping;

    public class ReviewForRecipeViewModel : IMapFrom<Review>
    {
        public string UserName { get; set; }

        public string Comment { get; set; }
    }
}