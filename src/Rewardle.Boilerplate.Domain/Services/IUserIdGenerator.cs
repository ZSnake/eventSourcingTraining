using System;

namespace Rewardle.Boilerplate.Domain.Services
{
    public interface IUserIdGenerator
    {
        Guid Generate();
    }
}