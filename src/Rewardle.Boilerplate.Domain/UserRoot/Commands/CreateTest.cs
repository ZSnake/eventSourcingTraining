namespace Rewardle.Boilerplate.Domain.UserRoot.Commands
{
    public class CreateTest
    {
        public CreateTest(string name)
        {
            Name = name;
        }

        public string Name { get; protected set; }
        
    }
}