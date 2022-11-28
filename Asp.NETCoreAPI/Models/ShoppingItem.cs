using System;
using System.ComponentModel.DataAnnotations;

namespace Asp.NETCoreAPI.Models
{
    public class ShoppingItem
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public string Manufacturing { get; set; }
    }
}
