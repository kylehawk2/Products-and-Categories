using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Products_and_Catagories.Models
{
    public class Catagory
    {
        [Key]
        public int CatagoryId {get;set;}
        [Required]
        public string Name {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
        public List<Association> Associations {get;set;}
    }
}