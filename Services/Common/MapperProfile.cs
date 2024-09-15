using AutoMapper;
using Repositories.Entities;
using Repositories.Models.AccountModels;
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
            CreateMap<AccountModel, Account>().ReverseMap();
        }
    }
}
