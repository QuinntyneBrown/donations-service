using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DonationsService.Features.Core;

namespace DonationsService.Features.Donors
{
    [Authorize]
    [RoutePrefix("api/donors")]
    public class DonorController : BaseApiController
    {
        public DonorController(IMediator mediator)
            :base(mediator) { }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateDonorCommand.Response))]
        public async Task<IHttpActionResult> Add(AddOrUpdateDonorCommand.Request request) => Ok(await Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateDonorCommand.Response))]
        public async Task<IHttpActionResult> Update(AddOrUpdateDonorCommand.Request request) => Ok(await Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetDonorsQuery.Response))]
        public async Task<IHttpActionResult> Get() => Ok(await Send(new GetDonorsQuery.Request()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetDonorByIdQuery.Response))]
        public async Task<IHttpActionResult> GetById([FromUri]GetDonorByIdQuery.Request request) => Ok(await Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveDonorCommand.Response))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveDonorCommand.Request request) => Ok(await Send(request));

    }
}
