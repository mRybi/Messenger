using AutoMapper;
using Messenger.App.Commands;
using Messenger.App.Dtos;
using Messenger.App.Responses;
using Messenger.Persistence.EF.Models;

namespace Messenger.App
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<User, RegisterUserCommand>().ReverseMap();
            CreateMap<User, AuthenticateUserResponse>();
            CreateMap<User, UserDto>();
            CreateMap<Conversation, GetConversationsByUserIdResponse>().ReverseMap();
            CreateMap<Conversation, GetConversationMessagesResponse>().ReverseMap();
            
        }
    }
}
