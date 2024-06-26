﻿using AutoMapper;
using NoSolo.Contracts.Dtos.Auth;
using NoSolo.Contracts.Dtos.Base;
using NoSolo.Contracts.Dtos.Users;
using NoSolo.Core.Entities.User;

namespace NoSolo.Infrastructure.Services.Users.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserEntity, UserDto>();
        CreateMap<UpdateUserDto, UserEntity>();
        CreateMap<UserEntity, UserAuthDto>();
        CreateMap<MemberEntity, MemberDto>();
    }
}