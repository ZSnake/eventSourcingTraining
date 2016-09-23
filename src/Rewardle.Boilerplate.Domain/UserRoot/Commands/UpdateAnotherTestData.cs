using System;

namespace Rewardle.Boilerplate.Domain.UserRoot.Commands
{
    public class UpdateAnotherTestData
    {
        public UpdateAnotherTestData(string name)
        {
            Name = name;
        }

        public string Name { get; protected set; }
    }
}