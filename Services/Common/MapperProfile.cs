using AutoMapper;
using Repositories.Entities;
using Repositories.Models.AccountModels;
using Repositories.Models.BookingModels;
using Repositories.Models.DeviceModels;
using Repositories.Models.LocationModels;
using Repositories.Models.PodModels;
using Repositories.Models.RatingModels;
using Repositories.Models.ServiceModels;
using Services.Models.AccountModels;
using Services.Models.DeviceModels;
using Services.Models.LocationModels;
using Services.Models.PodModels;
using Services.Models.RatingModels;
using Services.Models.RewardPointModels;
using Services.Models.ServiceModels;


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
            CreateMap<RatingCommentCreateModel, RatingComment>().ReverseMap();
            CreateMap<RatingComment, RatingCommentModel>()
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account != null ? src.Account.FirstName + " " + src.Account.LastName : string.Empty))
                .ForMember(dest => dest.ChildComments, opt => opt.MapFrom(src => src.ChildComments))
                .ReverseMap();


            //Pod
            CreateMap<Pod, PodModel>()
           .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location != null ? src.Location.Name : string.Empty))
           .ForMember(dest => dest.DeviceType, opt => opt.MapFrom(src => src.Device != null ? src.Device.RoomType : string.Empty))
           .ForMember(dest => dest.Floor, opt => opt.MapFrom(src => src.Device != null ? src.Device.Floor : string.Empty));
            CreateMap<Pod, PodCreateModel>().ReverseMap();
            CreateMap<PodUpdateModel, Pod>().ReverseMap();
            //Location
            CreateMap<Location, LocationCreateModel>().ReverseMap();
            CreateMap<Location, LocationModel>().ReverseMap();
            CreateMap<Location, LocationUpdateModel>().ReverseMap();
            // Device
            CreateMap<Device, DeviceCreateModel>().ReverseMap();
            CreateMap<Device, DeviceUpdateModel>().ReverseMap();
            CreateMap<Device, DeviceModel>().ReverseMap();
            //Service
            CreateMap<Service, ServiceCreateModel>().ReverseMap();
            CreateMap<Service, ServiceUpdateModel>().ReverseMap();
            CreateMap<Service, ServiceModel>().ReverseMap();
            // Booking
            CreateMap<Booking, BookingModel>()
                .ForMember(dest => dest.PodName, opt => opt.MapFrom(src => src.Pod != null ? src.Pod.Name : string.Empty))
                .ForMember(dest => dest.LocationAddress, opt => opt.MapFrom(src => src.Pod.Location != null ? src.Pod.Location.Address : string.Empty))
                .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Pod.Capacity))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Pod.Area))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus.ToString()))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod.ToString()))
                .ForMember(dest => dest.BookingServices, opt => opt.MapFrom(src => src.BookingServices));

            // BookingService
            CreateMap<BookingService, BookingServiceModel>()
                .ForMember(dest => dest.NameService, opt => opt.MapFrom(src => src.Service != null ? src.Service.Name : string.Empty));
            //RewardPoints
            CreateMap<Repositories.Entities.RewardPoints, Repositories.Models.RewardPointModels.RewardPointModel>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) 
           .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            CreateMap<RewardPointUpdateModel, Repositories.Entities.RewardPoints>()
                .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
    }



