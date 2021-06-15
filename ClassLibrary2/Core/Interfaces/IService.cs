using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Core.Interfaces
{
    public interface IService<TEntity> where TEntity : class, new()
    {
        TEntity Add(TEntity t);
        TEntity Edit(TEntity t, params object[] keyValues);
        TEntity Delete(params object[] keyValues);
        TEntity GetById(params object[] keyValues);
        ICollection<TEntity> GetAll();

        ICollection<TEntity> Filter(Expression<Func<TEntity, bool>> filter);
    }
}
