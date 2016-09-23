using System;
using AcklenAvenue.Commands;

namespace Rewardle.Boilerplate.Api.Accounts
{
    public class VisitorUserSession : IUserSession
    {
        public VisitorUserSession()
        {
            Id = new Guid();
            Visiting = true;
        }

        public bool Visiting { get; private set; }
        public Guid Id { get; private set; }
    }
}