using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clases.Core.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        TEntity Add(TEntity item);

        TEntity Delete(params object[] keyValues);

        TEntity Edit(TEntity item, params object[] keyValues);

        IQueryable<TEntity> GetAll();

        void Save();

        IRepositoryQuery<TEntity> Query();

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null, int? pageSize = null);

        TEntity GetById(params object[] keyValues);

        IQueryable<TEntity> ExecuteRawSQLQuery(string query, object[] parameters);

        IQueryable<TElement> ExecuteParticularRawSQLQuery<TElement>(string query, object[] parameters);

        TEntity SingleExecuteRawSQLQuery(string query, object[] parameters);

        void BulkInsert(ICollection<TEntity> items);
    }
}
