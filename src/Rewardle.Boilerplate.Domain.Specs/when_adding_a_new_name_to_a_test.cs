using System;
using Machine.Specifications;
using Rewardle.Boilerplate.Domain.UserRoot;

namespace Rewardle.Boilerplate.Domain.Specs
{
    public class when_adding_a_new_name_to_a_test
    {
        static TestAggregateRoot _testAggregateRoot;

        Establish context =
            () =>
                {
                    _testAggregateRoot = new TestAggregateRoot(
                        Guid.NewGuid(),
                        "Test");
                };

        
        It should_have_name_field = () => _testAggregateRoot.Name.ShouldEqual("Test");
    }
}