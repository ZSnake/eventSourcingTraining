using System.Threading.Tasks;
using Rewardle.Boilerplate.Domain.Services.Interfaces;

namespace Rewardle.Boilerplate.Domain
{
    public interface IViewModelWriter : IViewStore
    {
        Task UpdateModel();
        Task Add<T>(T model) where T: class, IViewModel;
    }
}