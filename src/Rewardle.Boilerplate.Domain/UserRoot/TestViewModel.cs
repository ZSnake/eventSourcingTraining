using Rewardle.Boilerplate.Domain.Services.Interfaces;

namespace Rewardle.Boilerplate.Domain.UserRoot
{
    public class TestViewModel : IViewModel
    {
        protected TestViewModel()
        {
            
        }
        public TestViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public int Id { get; private set; }
    }
}