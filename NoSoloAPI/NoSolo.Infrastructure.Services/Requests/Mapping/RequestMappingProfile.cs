﻿using AutoMapper;
using NoSolo.Contracts.Dtos.Organizations.Requests;
using NoSolo.Contracts.Dtos.Users.Requests;
using NoSolo.Core.Entities.Base;
using NoSolo.Core.Entities.Organization;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Services.Requests.Mapping;

public class RequestMappingProfile : Profile
{
    public RequestMappingProfile()
    {
        CreateMap<RequestEntity<OrganizationEntity, UserOfferEntity>, OrganizationRequestDto>();
        CreateMap<RequestEntity<UserEntity, OrganizationOfferEntity>, UserRequestDto>();
    }
}