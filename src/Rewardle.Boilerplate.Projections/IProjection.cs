using BlingBag;

namespace Rewardle.Boilerplate.Projections
{
    public interface IProjection<in T> : IBlingHandler<T>
    {
    }
}