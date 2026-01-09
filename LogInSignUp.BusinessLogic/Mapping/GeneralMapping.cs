using AutoMapper;
using LogInSignUp.BusinessLogic.DTOs;
using LogInSignUp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInSignUp.BusinessLogic.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<CreateUserDto, User>();
        }
    }
}
