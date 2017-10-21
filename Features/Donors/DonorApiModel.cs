using DonationsService.Model;

namespace DonationsService.Features.Donors
{
    public class DonorApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromDonor<TModel>(Donor donor) where
            TModel : DonorApiModel, new()
        {
            var model = new TModel();
            model.Id = donor.Id;
            model.TenantId = donor.TenantId;
            model.Name = donor.Name;
            return model;
        }

        public static DonorApiModel FromDonor(Donor donor)
            => FromDonor<DonorApiModel>(donor);

    }
}
