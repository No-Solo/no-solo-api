﻿using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserProfile, UserProfileDto>();
        CreateMap<UserProfileDto, UserProfile>();
    }
}