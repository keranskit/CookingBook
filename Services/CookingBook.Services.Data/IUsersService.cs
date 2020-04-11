namespace CookingBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task ToggleBan(string id, DateTime ban);

        Task RemoveFromAdmins(string id);

        Task AddToAdmins(string id);

        int GetCount();

        IEnumerable<T> GetAll<T>();

        T GetById<T>(string id);
    }
}
