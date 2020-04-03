namespace CookingBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CookingBook.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public string Title { get; set; }
    }
}
