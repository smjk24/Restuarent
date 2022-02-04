using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restuarent.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Menu> CreatedMenu { get; set; }
        public virtual ICollection<Menu> ModifiedMenu { get; set; }
    }
}
