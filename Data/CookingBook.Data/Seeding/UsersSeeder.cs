namespace CookingBook.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CookingBook.Common;

    using CookingBook.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class UsersSeeder : ISeeder
    {
        public UsersSeeder()
        {
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            await dbContext.Users.AddAsync(new ApplicationUser()
            {
                Name = "Trayan Keranov",
                Id = "22e1f0c0-a6ff-4864-b939-b0c570f2d51d",
                UserName = "keranski@abv.bg",
                NormalizedUserName = "KERANSKI@ABV.BG",
                Email = "keranski@abv.bg",
                NormalizedEmail = "KERANSKI@ABV.BG",
                PasswordHash = "AQAAAAEAACcQAAAAEKDTOW0hiJThqFCz2cTS+wMBN2HthJkHT1jCsqVhgYwc0XikiVo0ESJYcqs8yrZkgg==",
                SecurityStamp = "5ZMAFTFQEDOZPXC573KIOV5B56KVMHKS",
                ConcurrencyStamp = "5ed4afbd-318e-456b-8d1b-19a36f2d82f1",
                CreatedOn = DateTime.Parse("1993-03-21 08:10:00.2228617"),
                EmailConfirmed = true,
                Roles = new List<IdentityUserRole<string>>(),
            });
            await dbContext.SaveChangesAsync();

            var adminRoleId = dbContext.Roles.FirstOrDefault(x => x.Name == GlobalConstants.AdministratorRoleName).Id;
            dbContext.Users.FirstOrDefault(x => x.Id == "22e1f0c0-a6ff-4864-b939-b0c570f2d51d").Roles.Add(new IdentityUserRole<string>()
            {
                RoleId = adminRoleId,
                UserId = "22e1f0c0-a6ff-4864-b939-b0c570f2d51d",
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
