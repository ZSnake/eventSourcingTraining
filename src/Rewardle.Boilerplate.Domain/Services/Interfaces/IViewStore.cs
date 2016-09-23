using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rewardle.Boilerplate.Domain.Services.Interfaces
{
    public interface IViewStore
    {
        Task<IEnumerable<T>> Query<T>(Expression<Func<T, bool>> expression) where T : class, IViewModel;        
    }
}