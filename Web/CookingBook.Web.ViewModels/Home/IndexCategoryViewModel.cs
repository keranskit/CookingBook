namespace CookingBook.Web.ViewModels.Home
{
    using Data.Models;
    using Services.Mapping;

    public class IndexCategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
