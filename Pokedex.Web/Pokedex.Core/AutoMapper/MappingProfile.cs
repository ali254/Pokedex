using System;
using System.Collections.Generic;
using System.Text;
using Entity = Pokedex.Core.Entity;
using DTO = Pokedex.Core.DTO;
using AutoMapper;
using System.Linq;

namespace Pokedex.Core.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entity.Pokemon, DTO.Pokemon>()
                .ForMember(prop => prop.Habitat, opt => opt.MapFrom(src => src.Habitat.Name))
                .ForMember(prop => prop.Description, 
                    opt => opt.MapFrom(src => src.Descriptions.FirstOrDefault(x => x.Language.Name.Equals("en", StringComparison.OrdinalIgnoreCase)).FlavorText));
        }
    }
}
