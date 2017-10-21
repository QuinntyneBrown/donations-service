using MediatR;
using DonationsService.Data;
using DonationsService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace DonationsService.Features.Donors
{
    public class GetDonorsQuery
    {
        public class Request : BaseRequest, IRequest<Response> { }

        public class Response
        {
            public ICollection<DonorApiModel> Donors { get; set; } = new HashSet<DonorApiModel>();
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
                var donors = await _context.Donors
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new Response()
                {
                    Donors = donors.Select(x => DonorApiModel.FromDonor(x)).ToList()
                };
            }

            private readonly DonationsServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
