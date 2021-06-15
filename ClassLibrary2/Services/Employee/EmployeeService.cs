using Clases;
using Clases.Core.Interfaces.Repositories;
using Clases.Models;
using Servicios.Core.Generics;
using Servicios.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Services.EmployeeSer
{
    class EmployeeService : GenericService<Employee>, IEmployeeService
    {
        public EmployeeService(IRepository<Employee> genericRepository) : base(genericRepository)
        {
        }
    }

}
