namespace CookingBook.Services.Data
{
    using System.Collections.Generic;

    public interface IRecipesService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetByCategoryId<T>(int categoryId);
    }
}