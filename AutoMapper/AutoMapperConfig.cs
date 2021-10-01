using AutoMapper;
using SitePet.Mvc.Models;
using SitePet.Mvc.ViewModels;

namespace SitePet.Mvc.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Pet, PetViewModel>().ReverseMap();
        }
    }
}
