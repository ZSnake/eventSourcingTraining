namespace Rewardle.Boilerplate.Domain.UserRoot
{
    public class CoolnessIndicatorViewModel
    {
        public int Coolness { get; protected set; }

        public CoolnessIndicatorViewModel(int coolness)
        {
            Coolness = coolness;
        }
    }
}