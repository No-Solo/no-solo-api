namespace Core.Specification.Organization.OrganizationOffer;

public class OrganizationOfferWithSpecificationParams : BaseSpecification<Entities.OrganizationOffer>
{
    public OrganizationOfferWithSpecificationParams(OrganizationOfferParams organizationOfferParams)
        : base(x => (string.IsNullOrEmpty(organizationOfferParams.Search) || x.Description.ToLower().Contains(organizationOfferParams.Search))
                    && (!organizationOfferParams.OrganizationId.HasValue || x.OrganizationId == organizationOfferParams.OrganizationId))
    {
        ApplyPaging(organizationOfferParams.PageSize * (organizationOfferParams.PageNumber -1), organizationOfferParams.PageSize);
        
        if (!string.IsNullOrEmpty(organizationOfferParams.SortByAlphabetical))
        {
            switch (organizationOfferParams.SortByAlphabetical)
            {
                case "alphaAsc":
                    AddOrderBy(p => p.Name);
                    break;
                case "alphaDesc":
                    AddOrderByDescending(p => p.Name);
                    break;
                default:
                    AddOrderBy(p => p.Created);
                    break;
            }
        }

        if (!string.IsNullOrEmpty(organizationOfferParams.SortByDate))
        {
            switch (organizationOfferParams.SortByDate)
            {
                case "dateAsc":
                    AddOrderBy(p => p.Created);
                    break;
                case "dateDesc":
                    AddOrderByDescending(p => p.Created);
                    break;
                default:
                    AddOrderBy(p => p.Created);
                    break;
            }
        }
    }
}