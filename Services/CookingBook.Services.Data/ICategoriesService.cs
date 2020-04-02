namespace CookingBook.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();
    }
}
