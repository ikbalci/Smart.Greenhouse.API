using AutoMapper;
using Smart.Greenhouse.API.Core.DTOs;
using Smart.Greenhouse.API.Core.Entities;

namespace Smart.Greenhouse.API.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Model to DTO
            CreateMap<SensorData, SensorDataDto>();
            
            // DTO to Model
            CreateMap<SensorDataDto, SensorData>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
} 