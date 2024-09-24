using AutoMapper;
using Repositories.Entities;
using Repositories.Models.AccountModels;
using Repositories.Models.LocationModels;
using Repositories.Models.PodModels;
using Repositories.Models.RatingModels;
using Services.Models.AccountModels;
using Services.Models.LocationModels;
using Services.Models.PodModels;
using Services.Models.RatingModels;


namespace Services.Common
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Account
            CreateMap<AccountRegisterModel, Account>();
            CreateMap<AccountModel, Account>().ReverseMap();

            //Rating
            CreateMap<Rating, RatingModel>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.FirstName + " " + src.Customer.LastName : string.Empty))
                .ForMember(dest => dest.PodName, opt => opt.MapFrom(src => src.Pod != null ? src.Pod.Name : string.Empty))
                .ForMember(dest => dest.CommentsList, opt => opt.MapFrom(src => src.CommentsList));
            CreateMap<RatingCommentCreateModel, RatingComment>();
            CreateMap<RatingComment, RatingCommentModel>().ReverseMap();

            //Pod
            CreateMap<Pod, PodModel>()
           .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location != null ? src.Location.Name : string.Empty))
           .ForMember(dest => dest.DeviceType, opt => opt.MapFrom(src => src.Device != null ? src.Device.RoomType : string.Empty));
            CreateMap<Pod,PodCreateModel>().ReverseMap();
            //Location
            CreateMap<Location, LocationCreateModel>().ReverseMap();
            CreateMap<Location,LocationModel>().ReverseMap();
            CreateMap<Location,LocationUpdateModel>().ReverseMap();

        }
    }


}
