using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RCNGCMembersManagementAppLogic.Database.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> SearchFor(Expression<Func<T, bool>> filter);
        IQueryable<T> GetAll();
        T GetById(int id);
    }
}
