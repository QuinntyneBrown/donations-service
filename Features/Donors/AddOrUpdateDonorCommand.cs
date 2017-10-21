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
    public class AddOrUpdateDonorCommand
    {
        public class Request : BaseRequest, IRequest<Response>
        {
            public DonorApiModel Donor { get; set; }            
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
                var entity = await _context.Donors
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Donor.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Donors.Add(entity = new Donor() { TenantId = tenant.Id });
                }

                entity.Name = request.Donor.Name;
                
                await _context.SaveChangesAsync();

                _bus.Publish(new AddedOrUpdatedDonorMessage(entity, request.CorrelationId, request.TenantUniqueId));

                return new Response();
            }

            private readonly DonationsServiceContext _context;
            private readonly IEventBus _bus;
        }
    }
}
