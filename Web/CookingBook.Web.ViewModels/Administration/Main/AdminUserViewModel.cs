namespace CookingBook.Web.ViewModels.Administration.Main
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    using CookingBook.Data.Models;
    using CookingBook.Services.Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class AdminUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public bool IsAdmin => this.Roles.Count > 0;

        public bool IsBanned => this.LockoutEnabled && (this.LockoutEnd > DateTime.Now);

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool LockoutEnabled { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
