using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using aronportal.DTOS;
using aronportal.Models;

namespace aronportal.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
           
            CreateMap<LoginDTO, TblUser>();
            CreateMap<RegisterDTO, TblUser>();
        }
    }
}
