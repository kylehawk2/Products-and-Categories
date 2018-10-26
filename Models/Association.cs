using System;
using System.ComponentModel.DataAnnotations;

namespace Products_and_Catagories.Models
{
    public class Association
    {
        [Key]
        public int AssociationId {get;set;}
        public int ProductId {get;set;}
        public Product Products {get;set;}
        public int CatagoryId {get;set;}
        public Catagory Catagories {get;set;}
    }
}