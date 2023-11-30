using AutoMapper;
using NoSolo.Contracts.Dtos.Organizations.Offers;
using NoSolo.Contracts.Dtos.User;
using NoSolo.Contracts.Dtos.User.Create;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Services.Offers.Mapping;

public class OfferMappingProfile : Profile
{
    public OfferMappingProfile()
    {
        CreateMap<UserOffer, UserOfferDto>();
        CreateMap<NewUserOfferDto, UserOffer>();
        
        CreateMap<OrganizationOffer, OrganizationOfferDto>();
        CreateMap<OrganizationOfferDto, OrganizationOffer>();
    }
}