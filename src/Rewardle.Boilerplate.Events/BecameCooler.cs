namespace Rewardle.Boilerplate.Events
{
    public class BecameCooler
    {
        public BecameCooler(int coolness)
        {
            Coolness = coolness;
        }

        public int Coolness { get; protected set; }
    }
}