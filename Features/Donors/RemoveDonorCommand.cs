using MediatR;
using DonationsService.Data;
using DonationsService.Model;
using DonationsService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace DonationsService.Features.Donors
{
    public class RemoveDonorCommand
    {
        public class Request : BaseRequest, IRequest<Response>
        {
            public int Id { get; set; }
            public Guid CorrelationId { get; set; }
        }

        public class Response { }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(DonationsServiceContext context, IEventBus bus)
            {
                _context = context;
                _bus = bus;
            }

            public async Task<Response> Handle(Request request)
            {
                var donor = await _context.Donors.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                donor.IsDeleted = true;
                await _context.SaveChangesAsync();
                _bus.Publish(new RemovedDonorMessage(request.Id, request.CorrelationId, request.TenantUniqueId));
                return new Response();
            }

            private readonly DonationsServiceContext _context;
            private readonly IEventBus _bus;
        }
    }
}
