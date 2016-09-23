using System;
using System.Threading.Tasks;
using AcklenAvenue.EventSourcing;

namespace Rewardle.Boilerplate.Domain.Services.Interfaces
{
    public interface IAggregateRepository<in T> where T : AggregateRoot
    {
        Task<TOut> Get<TOut>(Guid id) where TOut : T;
        Task SaveChanges(T entity);
        Task SaveChanges(DateTime datetimestamp, T entity);
    }
}