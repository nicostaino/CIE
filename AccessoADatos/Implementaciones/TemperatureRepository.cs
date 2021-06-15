
using AccessoADatos.Core.Generics;
using AccessoADatos.Core.Interfaces;
using Clases;
using Clases.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoADatos.Implementaciones
{
    public class TemperatureRepository : GenericRepository<Temperature>, ITemperatureRepository
    {
        public TemperatureRepository(IContext context) : base(context)
        {
        }
    }
}
