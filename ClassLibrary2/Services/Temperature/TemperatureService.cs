
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.Core.Generics;
using Clases;
using Clases.Core.Interfaces.Repositories;
namespace Servicios.Services.TemperatureSer
{
  public  class TemperatureService : GenericService<Temperature>, ITemperatureService
    {
        public TemperatureService(IRepository<Temperature> genericRepository) : base(genericRepository)
        {
        }
    }
}
