using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Database.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected Table<T> dataTable;

        public Repository(DataContext dataContext)
        {
            dataTable = dataContext.GetTable<T>();
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return dataTable.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return dataTable;
        }

        public T GetById(int id)
        {
            return dataTable.Single(e => e.ID.Equals(id));
        }
    }
}
