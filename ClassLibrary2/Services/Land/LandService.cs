 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Servicios.Core.Generics;
using Clases;
using Clases.Core.Interfaces.Repositories;

namespace Servicios.Services
{
    public class LandService : GenericService<Land>, ILandService
    {
        public LandService(IRepository<Land> genericRepository) : base(genericRepository)
        {
        }
    }
}
