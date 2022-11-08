using AutoMapper;
using Pokedex.Core.Constants;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pokedex.Core.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entity.Pokemon, DTO.Pokemon>()
                .ForMember(prop => prop.Habitat, opt => opt.MapFrom(src => src.Habitat.Name))
                .ForMember(prop => prop.Description,
                    opt => opt.MapFrom(src =>
                        Regex.Replace(src.Descriptions.FirstOrDefault(x => x.Language.Name.Equals("en", StringComparison.InvariantCultureIgnoreCase)).FlavorText,
                            RegexExpressions.RemoveSpecialEscapedCharacters, " ")));
        }
    }
}
