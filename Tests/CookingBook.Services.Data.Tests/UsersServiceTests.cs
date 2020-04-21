namespace CookingBook.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using CookingBook.Common;
    using CookingBook.Data;
    using CookingBook.Data.Models;
    using CookingBook.Data.Repositories;
    using CookingBook.Services.Mapping;
    using CookingBook.Web.ViewModels.Administration.Main;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class UsersServiceTests
    {
        [Fact]
        public async Task GetCountShouldReturnProperCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            await userRepo.AddAsync(new ApplicationUser
            {
                Name = "Trayan Keranov",
                Id = "trk",
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
            await userRepo.SaveChangesAsync();
            var rolesRepo = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(userRepo, rolesRepo);

            Assert.Equal(1, service.GetCount());
        }

        [Fact]
        public async Task BanShouldBanUserAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            await userRepo.AddAsync(new ApplicationUser
            {
                Name = "Trayan Keranov",
                Id = "trk",
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
            await userRepo.SaveChangesAsync();
            var rolesRepo = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(userRepo, rolesRepo);
            DateTime time = DateTime.MaxValue;

            await service.Ban("trk", time);
            Assert.True(DateTime.Now < dbContext.Users.Find("trk").LockoutEnd);
            Assert.True(dbContext.Users.Find("trk").LockoutEnabled);
        }

        [Fact]
        public async Task UnBanShouldUnBanUserAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            await userRepo.AddAsync(new ApplicationUser
            {
                Name = "Trayan Keranov",
                Id = "trk",
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
                LockoutEnabled = true,
                LockoutEnd = DateTimeOffset.MaxValue,
            });
            await userRepo.SaveChangesAsync();
            var rolesRepo = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(userRepo, rolesRepo);

            await service.Unban("trk");
            Assert.Null(dbContext.Users.Find("trk").LockoutEnd);
            Assert.False(dbContext.Users.Find("trk").LockoutEnabled);
        }

        [Fact]
        public async Task AddToAdminsShouldCreateNewUserRole()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            await userRepo.AddAsync(new ApplicationUser
            {
                Name = "Trayan Keranov",
                Id = "trk",
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
                LockoutEnabled = true,
                LockoutEnd = DateTimeOffset.MaxValue,
            });
            await userRepo.SaveChangesAsync();
            var rolesRepo = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            await rolesRepo.AddAsync(new ApplicationRole { Name = GlobalConstants.AdministratorRoleName, Id = "admin", CreatedOn = DateTime.MinValue, ConcurrencyStamp = "asdfghjkl", NormalizedName = "ADMIN" });
            await rolesRepo.SaveChangesAsync();
            var service = new UsersService(userRepo, rolesRepo);

            await service.AddToAdmins("trk");
            Assert.Equal(1, dbContext.Users.Find("trk").Roles.Count);
        }

        [Fact]
        public async Task RemoveFromAdminsShouldRemoveRoleFromUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            await userRepo.AddAsync(new ApplicationUser
            {
                Name = "Trayan Keranov",
                Id = "trk",
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
                LockoutEnabled = true,
                LockoutEnd = DateTimeOffset.MaxValue,
            });
            await userRepo.SaveChangesAsync();
            var rolesRepo = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            await rolesRepo.AddAsync(new ApplicationRole { Name = GlobalConstants.AdministratorRoleName, Id = "admin", CreatedOn = DateTime.MinValue, ConcurrencyStamp = "asdfghjkl", NormalizedName = "ADMIN" });
            await rolesRepo.SaveChangesAsync();
            var service = new UsersService(userRepo, rolesRepo);

            await service.AddToAdmins("trk");
            Assert.Equal(1, dbContext.Users.Find("trk").Roles.Count);

            await service.RemoveFromAdmins("trk");
            Assert.Equal(0, dbContext.Users.Find("trk").Roles.Count);
        }

        [Fact]
        public async Task GetAllShouldReturnCollectionFromUsers()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            await userRepo.AddAsync(new ApplicationUser
            {
                Name = "Trayan Keranov",
                Id = "trk",
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
                LockoutEnabled = true,
                LockoutEnd = DateTimeOffset.MaxValue,
            });
            await userRepo.SaveChangesAsync();
            var rolesRepo = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(userRepo, rolesRepo);

            Assert.Single(service.GetAll<AdminUserViewModel>());
        }

        [Fact]
        public async Task GetByIdShouldReturnUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            await userRepo.AddAsync(new ApplicationUser
            {
                Name = "Trayan Keranov",
                Id = "trk",
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
                LockoutEnabled = true,
                LockoutEnd = DateTimeOffset.MaxValue,
            });
            await userRepo.SaveChangesAsync();
            var rolesRepo = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            var service = new UsersService(userRepo, rolesRepo);

            var result = service.GetById<AdminUserViewModel>("trk");
            Assert.IsType<AdminUserViewModel>(result);
            Assert.NotNull(result);

            var falseResult = service.GetById<AdminUserViewModel>("asdfgh");
            Assert.IsNotType<AdminUserViewModel>(falseResult);
            Assert.Null(falseResult);
        }
    }
}
