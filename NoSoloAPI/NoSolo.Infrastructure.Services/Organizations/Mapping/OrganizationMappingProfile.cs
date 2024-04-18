using AutoMapper;
using NoSolo.Contracts.Dtos.Organizations.Organizations;
using NoSolo.Core.Entities.Organization;

namespace NoSolo.Infrastructure.Services.Organizations.Mapping;

public class OrganizationMappingProfile : Profile
{
    public OrganizationMappingProfile()
    {
        // Organizations
        CreateMap<OrganizationEntity, OrganizationDto>()
            .ForMember(dest => dest.PhotoUrl,
                opt => opt
                    .MapFrom(src => src.Photos
                        .FirstOrDefault(x => x.IsMain).Url));
        CreateMap<NewOrganizationDto, OrganizationEntity>();
        CreateMap<UpdateOrganizationDto, OrganizationEntity>();
    }
}