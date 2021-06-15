using AccessoADatos;
using Clases;
using Clases.Core.Interfaces.Repositories;
using Servicios.Core.Generics;
namespace Servicios.Services.User
{
    public class UserService : GenericService<Clases.User> , IUserService
    {
        public UserService(IRepository<Clases.User> genericRepository) : base(genericRepository)
        {
        }
    }

}
