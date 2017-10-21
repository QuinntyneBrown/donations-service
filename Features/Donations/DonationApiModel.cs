using DonationsService.Model;

namespace DonationsService.Features.Donations
{
    public class DonationApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromDonation<TModel>(Donation donation) where
            TModel : DonationApiModel, new()
        {
            var model = new TModel();
            model.Id = donation.Id;
            model.TenantId = donation.TenantId;            
            return model;
        }

        public static DonationApiModel FromDonation(Donation donation)
            => FromDonation<DonationApiModel>(donation);

    }
}
