using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DonationsService.Features.Core;

namespace DonationsService.Features.Donations
{
    [Authorize]
    [RoutePrefix("api/donations")]
    public class DonationController : BaseApiController
    {
        public DonationController(IMediator mediator)
            :base(mediator) { }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateDonationCommand.Response))]
        public async Task<IHttpActionResult> Add(AddOrUpdateDonationCommand.Request request) => Ok(await Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateDonationCommand.Response))]
        public async Task<IHttpActionResult> Update(AddOrUpdateDonationCommand.Request request) => Ok(await Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetDonationsQuery.Response))]
        public async Task<IHttpActionResult> Get() => Ok(await Send(new GetDonationsQuery.Request()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetDonationByIdQuery.Response))]
        public async Task<IHttpActionResult> GetById([FromUri]GetDonationByIdQuery.Request request) => Ok(await Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveDonationCommand.Response))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveDonationCommand.Request request) => Ok(await Send(request));

    }
}
