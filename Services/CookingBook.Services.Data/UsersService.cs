namespace CookingBook.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CookingBook.Common;
    using CookingBook.Data.Common.Repositories;
    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IRepository<ApplicationRole> rolesRepository;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IRepository<ApplicationRole> rolesRepository)
        {
            this.userRepository = userRepository;
            this.rolesRepository = rolesRepository;
        }

        public async Task Ban(string id, DateTime ban)
        {
            var user = this.userRepository.All().FirstOrDefaultAsync(x => x.Id == id).Result;
            user.LockoutEnabled = true;
            user.LockoutEnd = ban.ToUniversalTime();

            this.userRepository.Update(user);

            await this.userRepository.SaveChangesAsync();
        }

        public async Task Unban(string id)
        {
            var user = this.userRepository.All().FirstOrDefaultAsync(x => x.Id == id).Result;
            user.LockoutEnabled = false;
            user.LockoutEnd = null;

            this.userRepository.Update(user);

            await this.userRepository.SaveChangesAsync();
        }

        public async Task RemoveFromAdmins(string id)
        {
            var roleId = this.rolesRepository.All().FirstOrDefaultAsync(x => x.Name == GlobalConstants.AdministratorRoleName)
                .Result.Id;
            var user = this.userRepository.All().FirstOrDefaultAsync(x => x.Id == id).Result;
            var collection = this.userRepository.All().Where(x => x.Id == id).SelectMany(x => x.Roles)
                .ToList();
            var itemToRemove = collection.Find(x => x.RoleId == roleId);
            if (itemToRemove != null)
            {
                collection.Remove(itemToRemove);
                this.userRepository.All().FirstOrDefaultAsync(x => x.Id == id).Result.Roles = collection;
            }

            await this.userRepository.SaveChangesAsync();
        }

        public async Task AddToAdmins(string id)
        {
            var roleId = this.rolesRepository.All().FirstOrDefaultAsync(x => x.Name == GlobalConstants.AdministratorRoleName)
                .Result.Id;
            this.userRepository.All().FirstOrDefaultAsync(x => x.Id == id).Result.Roles
                .Add(new IdentityUserRole<string> { UserId = id, RoleId = roleId });

            await this.userRepository.SaveChangesAsync();
        }

        public int GetCount()
        {
            return this.userRepository.All().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.userRepository.All().Include(x => x.Roles).To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var user = this.userRepository.All()
                .Where(x => x.Id == id && x.IsDeleted == false)
                .Include(x => x.Roles)
                .To<T>()
                .FirstOrDefault();

            return user;
        }
    }
}
