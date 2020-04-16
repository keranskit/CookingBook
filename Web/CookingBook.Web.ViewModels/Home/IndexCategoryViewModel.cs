namespace CookingBook.Web.ViewModels.Home
{
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class IndexCategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }
    }
}
