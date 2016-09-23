namespace Rewardle.Boilerplate.Domain.UserRoot.Commands
{
    public class AddCoolness
    {
        public int Coolness { get; private set; }

        public AddCoolness(int coolness)
        {
            Coolness = coolness;
        }
    }
}