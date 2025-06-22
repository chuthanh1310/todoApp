using AutoMapper;

namespace AutoMapperMVC.Models
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Todo, TodoViewModels>().ReverseMap();
        }
    }
}
