namespace CookingBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task Ban(string id, DateTime ban);

        Task Unban(string id);

        Task RemoveFromAdmins(string id);

        Task AddToAdmins(string id);

        int GetCount();

        IEnumerable<T> GetAll<T>();

        T GetById<T>(string id);
    }
}
