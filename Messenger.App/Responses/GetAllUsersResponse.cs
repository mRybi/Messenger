using Messenger.App.Dtos;

namespace Messenger.App.Responses
{
    public class GetAllUsersResponse
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}
