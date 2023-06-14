using AutoMapper;
using Messenger.App.Commands;
using Messenger.App.Responses;
using Messenger.Persistence.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.App
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<User, AddMessageCommand>().ReverseMap();
            CreateMap<User, AuthenticateUserResponse>();
        }
    }
}
