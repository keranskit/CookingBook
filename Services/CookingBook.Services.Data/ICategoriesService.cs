namespace CookingBook.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        Task DeleteById(int id);

        Task EditById(int id, string newName);
    }
}
