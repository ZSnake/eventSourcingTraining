using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Rewardle.Boilerplate.Api.Specs
{
    public class TestUser : ClaimsPrincipal
    {
        public TestUser(Guid emailUserId)
            : base(new ClaimsIdentity(new List<Claim>
                                      {
                                          new Claim("aggregateId",
                                              emailUserId.ToString())
                                      }))
        {            
        }
    }
}