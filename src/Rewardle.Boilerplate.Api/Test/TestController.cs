using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AcklenAvenue.Commands;
using Rewardle.Boilerplate.Api.Accounts;
using Rewardle.Boilerplate.Api.Accounts.Requests;
using Rewardle.Boilerplate.Domain.Services.Interfaces;
using Rewardle.Boilerplate.Domain.UserRoot.Commands;

namespace Rewardle.Boilerplate.Api.Test
{
    [RoutePrefix("v1/users")]
    public class TestController : ApiController
    {
        readonly ICommandDispatcher _commandDispatcher;

        readonly IViewStore _viewStore;

        public TestController(
            IViewStore viewStore, ICommandDispatcher commandDispatcher)
        {
            _viewStore = viewStore;
            _commandDispatcher = commandDispatcher;
        }

        /// <summary>
        ///     Post new email users to Rewardle
        /// </summary>
        /// <param name="request">Send the data for the new email user</param>
        /// <response code="202">Correct response</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateTest(CreateTestRequest request)
        {
            await
                _commandDispatcher.Dispatch(
                    new VisitorUserSession(),
                    new CreateTest(request.Name));
            return Request.CreateResponse(HttpStatusCode.Accepted, "Accepted");
        }
    }
}