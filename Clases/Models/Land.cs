using Clases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Clases
{
    public class Land : BaseEntity
    {
     
        public string Name { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
