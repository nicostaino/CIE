using Clases.Core.Interfaces.Repositories;
using Clases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases.Repositories
{
 
    public interface IEmployeeRepository : IRepository<Employee>
    {
    }
}
