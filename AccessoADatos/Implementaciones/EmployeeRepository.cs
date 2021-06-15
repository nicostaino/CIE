using AccessoADatos.Core.Generics;
using AccessoADatos.Core.Interfaces;
using Clases.Models;
using Clases.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoADatos.Implementaciones
{
  

    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IContext context) : base(context)
        {
        }
    }
}
