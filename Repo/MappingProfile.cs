using AutoMapper;
using MessengerApplication.Models.DTO;
using MessengerApplication.Models;

namespace MessengerApplication.Repo
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Email, opts => opts.MapFrom(y => y.Email))
                //.ForMember(dest => dest.FirstName, opts => opts.MapFrom(y => y.FirstName))
                //.ForMember(dest => dest.LastName, opts => opts.MapFrom(y => y.LastName))
                .ForMember(dest => dest.Password, opts => opts.MapFrom(y => y.Password))

                .ForMember(dest => dest.Id, opts => opts.Ignore())
                //.ForMember(dest => dest.Registered, opts => opts.Ignore())
                //.ForMember(dest => dest.Active, opts => opts.Ignore()).ReverseMap();
            ;
        }
    }
}
