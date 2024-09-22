using AutoMapper;
using Repositories.Entities;
using Repositories.Models.AccountModels;
using Repositories.Models.RatingModels;
using Services.Models.AccountModels;
using Services.Models.RatingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        }
    }
}
