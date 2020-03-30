namespace CookingBook.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Web.ViewModels.Categories;

    public interface ICategoriesService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();

        Task AddAsync(CategoriesViewModel categoriesViewModel);
    }
}
