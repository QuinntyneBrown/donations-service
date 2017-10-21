using MediatR;
using DonationsService.Data;
using DonationsService.Model;
using DonationsService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace DonationsService.Features.Donations
{
    public class RemoveDonationCommand
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
                var donation = await _context.Donations.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                donation.IsDeleted = true;
                await _context.SaveChangesAsync();
                _bus.Publish(new RemovedDonationMessage(request.Id, request.CorrelationId, request.TenantUniqueId));
                return new Response();
            }

            private readonly DonationsServiceContext _context;
            private readonly IEventBus _bus;
        }
    }
}
