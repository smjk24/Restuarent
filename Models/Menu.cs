using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restuarent.Models
{
    public class Menu
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)] //This also can do by Flurnt api
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public double? Discount { get; set; }
        public string Image { get; set; }
        public DateTime CDate { get; set; }
        public int CLoginId { get; set; }
        public DateTime? MDate { get; set; }
        public int? MLoginId { get; set; }
        [NotMapped]
        public IFormFile iformfile { get; set; }
        public virtual Login CLogin { get; set; }
        public virtual Login MLogin { get; set; }

    }
}
