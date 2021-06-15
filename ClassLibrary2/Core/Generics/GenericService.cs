using Clases.Core.Interfaces.Repositories;
using Servicios.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Core.Generics
{
    public class GenericService<T> : IService<T> where T : class, new()
    {
        protected IRepository<T> _genericRepository;

        public GenericService(IRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public virtual T Add(T t)
        {
            T itemResult = _genericRepository.Add(t);
            _genericRepository.Save();
            return itemResult;
        }

        public virtual T Delete(params object[] keyValues)
        {
            T itemResult = _genericRepository.Delete(keyValues);
            _genericRepository.Save();
            return itemResult;
        }

        public virtual T Edit(T t, params object[] keyValues)
        {
            _genericRepository.Edit(t, keyValues);
            _genericRepository.Save();
            return t;
        }

        public ICollection<T> Filter(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
                return new List<T>();

            return _genericRepository.Query().Filter(filter).Get().ToList();
        }

        public virtual ICollection<T> GetAll()
        {
            return _genericRepository.GetAll().ToList();
        }

        public T GetById(params object[] keyValues)
        {
            return _genericRepository.GetById(keyValues);
        }       
    }
}
