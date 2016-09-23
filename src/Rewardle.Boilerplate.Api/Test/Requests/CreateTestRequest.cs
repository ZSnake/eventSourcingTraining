using System;

namespace Rewardle.Boilerplate.Api.Accounts.Requests
{
    public class CreateTestRequest : IBaseRequest
    {
        public string Name { get; set; }
        public DateTime DateTimeStamp { get; set; }
    }
}