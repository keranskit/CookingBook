namespace CookingBook.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Product : BaseDeletableModel<string>
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Product1 { get; set; }

        public string Product2 { get; set; }

        public string Product3 { get; set; }
        
        public string Product4 { get; set; }
        
        public string Product5 { get; set; }
        
        public string Product6 { get; set; }
        
        public string Product7 { get; set; }
        
        public string Product8 { get; set; }
        
        public string Product9 { get; set; }
        
        public string Product10 { get; set; }
        
        public string Product11 { get; set; }
        
        public string Product12 { get; set; }
        
        public string Product13 { get; set; }
        
        public string Product14 { get; set; }
        
        public string Product15 { get; set; }

        [Required]
        public string RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
