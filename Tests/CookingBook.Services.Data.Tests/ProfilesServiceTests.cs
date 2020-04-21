namespace CookingBook.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using CookingBook.Data;
    using CookingBook.Data.Models;
    using CookingBook.Data.Repositories;
    using CookingBook.Services.Mapping;
    using CookingBook.Web.ViewModels.Profile;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ProfilesServiceTests
    {
        [Fact]
        public async System.Threading.Tasks.Task GetAllShouldReturnAllRecipesFromUserAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var recipeRepo = new EfDeletableEntityRepository<Recipe>(dbContext);
            var service = new ProfilesService(recipeRepo, userRepo);
            await dbContext.Users.AddAsync(new ApplicationUser
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
            await dbContext.Recipes.AddAsync(new Recipe { Id = "first", UserId = "trk", Title = "firstTitle", });
            await dbContext.Recipes.AddAsync(new Recipe { Id = "second", UserId = "trk", Title = "secondTitle", });
            await dbContext.SaveChangesAsync();

            Assert.Equal(2, service.GetByUserId<RecipeByUserViewModel>("trk").Count());
            Assert.Empty(service.GetByUserId<RecipeByUserViewModel>("sdfghhjkjhgf"));
        }

        [Fact]
        public async System.Threading.Tasks.Task GetFavoriteByUserIdShouldReturnAllFavorites()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var recipeRepo = new EfDeletableEntityRepository<Recipe>(dbContext);
            var service = new ProfilesService(recipeRepo, userRepo);
            await dbContext.Users.AddAsync(new ApplicationUser
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
            await dbContext.Recipes.AddAsync(new Recipe { Id = "first", UserId = "trk", Title = "firstTitle", });
            await dbContext.Recipes.AddAsync(new Recipe { Id = "second", UserId = "trk", Title = "secondTitle", });
            await dbContext.UserFavoriteRecipes.AddAsync(new UserFavoriteRecipe { UserId = "trk", RecipeId = "first" });
            await dbContext.UserFavoriteRecipes.AddAsync(new UserFavoriteRecipe { UserId = "trk", RecipeId = "second" });
            await dbContext.SaveChangesAsync();

            var result = service.GetFavoriteByUserId<RecipeByUserFavoriteViewModel>("trk");
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async System.Threading.Tasks.Task RemoveFromFavoritesShouldRemoveFromUserFavorites()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            AutoMapperConfig.RegisterMappings(Assembly.Load("CookingBook.Web.ViewModels"));
            var dbContext = new ApplicationDbContext(options);
            var userRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var recipeRepo = new EfDeletableEntityRepository<Recipe>(dbContext);
            var service = new ProfilesService(recipeRepo, userRepo);
            await dbContext.Users.AddAsync(new ApplicationUser
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
            await dbContext.Recipes.AddAsync(new Recipe { Id = "first", UserId = "trk", Title = "firstTitle", });
            await dbContext.Recipes.AddAsync(new Recipe { Id = "second", UserId = "trk", Title = "secondTitle", });
            await dbContext.UserFavoriteRecipes.AddAsync(new UserFavoriteRecipe { UserId = "trk", RecipeId = "first" });
            await dbContext.UserFavoriteRecipes.AddAsync(new UserFavoriteRecipe { UserId = "trk", RecipeId = "second" });
            await dbContext.SaveChangesAsync();

            await service.RemoveFromFavorites("trk", "first");

            Assert.Single(service.GetFavoriteByUserId<RecipeByUserFavoriteViewModel>("trk"));
        }
    }
}
