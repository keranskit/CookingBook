namespace CookingBook.Web.ViewModels.Administration.Main
{
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;

    public class CategoryInMain : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
