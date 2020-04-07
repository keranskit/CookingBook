namespace CookingBook.Services.Data
{
    using System.Collections.Generic;

    public interface IProfilesService
    {

        IEnumerable<T> GetByUserId<T>(string userId);

        IEnumerable<T> GetFavoriteByUserId<T>(string userId);
    }
}
