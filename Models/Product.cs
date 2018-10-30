using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Products_and_Catagories.Models
{
    public class Product
    {
        [Key]
        public int ProductId {get;set;}
        [Required]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage="Name cannot be gibberish!")]
        public string Name {get;set;}
        [Required]
        [MinLength(5)]
        public string Description {get;set;}
        [Required]
        public decimal Price {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
        public List<Association> Associations {get;set;}
    }
}