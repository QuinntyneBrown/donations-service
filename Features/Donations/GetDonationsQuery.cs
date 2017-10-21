using MediatR;
using DonationsService.Data;
using DonationsService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace DonationsService.Features.Donations
{
    public class GetDonationsQuery
    {
        public class Request : BaseRequest, IRequest<Response> { }

        public class Response
        {
            public ICollection<DonationApiModel> Donations { get; set; } = new HashSet<DonationApiModel>();
        }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(DonationsServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var donations = await _context.Donations
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new Response()
                {
                    Donations = donations.Select(x => DonationApiModel.FromDonation(x)).ToList()
                };
            }

            private readonly DonationsServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
