using EntityFramework.BulkInsert.Extensions;
using AccessoADatos.Core.Interfaces;
using Clases.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AccessoADatos.Core.Generics
{
    public class GenericRepository<T> : IRepository<T> where T : class, new()
    {
        internal IContext _context;

        internal IDbSet<T> _dbSet;

        public GenericRepository(IContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public virtual T Add(T item)
        {
            T itemResult = _dbSet.Add(item);
            return itemResult;
        }

        public virtual T AddOrUpdate(T item)
        {
            return null;
        }

        public virtual void BulkInsert(ICollection<T> items)
        {
            ((DbContext)_context).BulkInsert(items);
        }

        public virtual IRepositoryQuery<T> Query()
        {
            var repositoryGetFluentHelper = new RepositoryQuery<T>(this);

            return repositoryGetFluentHelper;
        }


        public virtual T Delete(params object[] keyValues)
        {
            T entity = _dbSet.Find(keyValues);
            return _dbSet.Remove(entity);
        }

        public virtual T Edit(T item, params object[] keyValues)
        {
            if (item != null)
            {
                T element = _dbSet.Find(keyValues);
                if (element != null)
                {
                    ((DbContext)_context).Entry(element).CurrentValues.SetValues(item);
                }
            }
            return item;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual IQueryable<T> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>,
            IOrderedQueryable<T>> orderBy = null,
        List<Expression<Func<T, object>>>
            includeProperties = null,
        int? page = null,
        int? pageSize = null)
        {
            IQueryable<T> query = _dbSet;

            if (includeProperties != null)
                includeProperties.ForEach(i => { query = query.Include(i); });

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return query;
        }


        public virtual IQueryable<TElement> ExecuteParticularRawSQLQuery<TElement>(string query, object[] parameters)
        {
            var queryResult = ((DbContext)_context).Database.SqlQuery<TElement>(query, parameters);
            return queryResult.AsQueryable();
        }

        public virtual void ExecuteStoredProcedure(string procedureNameParam, object[] parametersStored)
        {
            try
            {
                var parameters = new object[parametersStored.Length];
                for (int i = 0; i < parametersStored.Length; i++)
                {
                    parameters[i] = new SqlParameter
                    {
                        ParameterName = (string)((object[])parametersStored[i])[0],
                        Value = ((object[])parametersStored[i])[1]
                    };
                }
                ((DbContext)_context).Database.ExecuteSqlCommand(procedureNameParam, parameters[0], parameters[1], parameters[2]);
            }
            catch (Exception)
            {

            }
        }

        public virtual IQueryable<T> ExecuteRawSQLQuery(string query, object[] parameters)
        {
            return _context.Set<T>().SqlQuery(query, parameters).AsQueryable();
        }

        public virtual T SingleExecuteRawSQLQuery(string query, object[] parameters)
        {
            var listElements = _context.Set<T>().SqlQuery(query, parameters);
            return listElements.Any() ? listElements.ToList().First() : null;
        }

        public virtual void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual T GetById(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }        
    }
}
