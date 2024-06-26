﻿using AutoMapper;
using NoSolo.Contracts.Dtos.Organizations.Offers;
using NoSolo.Contracts.Dtos.Users.Offers;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Services.Offers.Mapping;

public class OfferMappingProfile : Profile
{
    public OfferMappingProfile()
    {
        CreateMap<UserOfferEntity, UserOfferDto>();
        CreateMap<NewUserOfferDto, UserOfferEntity>();
        CreateMap<UserOfferDto, UserOfferEntity>();
        
        CreateMap<OrganizationOfferEntity, OrganizationOfferDto>();
        CreateMap<OrganizationOfferDto, OrganizationOfferEntity>();
    }
}