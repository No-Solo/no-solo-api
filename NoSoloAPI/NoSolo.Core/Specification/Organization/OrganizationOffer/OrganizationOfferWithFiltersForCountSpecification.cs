using NoSolo.Core.Specification.BaseSpecification;

namespace NoSolo.Core.Specification.Organization.OrganizationOffer;

public class OrganizationOfferWithFiltersForCountSpecification : BaseSpecification<Entities.Organization.OrganizationOfferEntity>
{
    public OrganizationOfferWithFiltersForCountSpecification(OrganizationOfferParams organizationOfferParams)
        : base(x => (string.IsNullOrEmpty(organizationOfferParams.Search) || x.Description.ToLower().Contains(organizationOfferParams.Search))
                    && (!organizationOfferParams.OrganizationId.HasValue || x.OrganizationId == organizationOfferParams.OrganizationId))
    {
        
    }
}