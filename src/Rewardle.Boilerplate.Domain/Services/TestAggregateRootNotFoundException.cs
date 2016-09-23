using System;

namespace Rewardle.Boilerplate.Domain.Services
{
    public class TestAggregateRootNotFoundException : Exception
    {
        public TestAggregateRootNotFoundException(Guid testId)
            :base(string.Format("TestAggregateRoot not found with id '{0}'.", testId))
        {            
        }
        public TestAggregateRootNotFoundException(string name)
            : base(string.Format("TestAggregateRoot not found with code '{0}'.", name))
        {
        }
    }
}