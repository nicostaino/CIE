using AccessoADatos.Core.Generics;
using AccessoADatos.Core.Interfaces; 
using Clases;
using Clases.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoADatos.Implementaciones
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IContext context) : base(context)
        {
        }
    }
}
