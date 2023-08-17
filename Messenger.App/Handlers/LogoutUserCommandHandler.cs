using MediatR;
using Messenger.App.Commands;
using Messenger.App.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;

namespace Messenger.App.Handlers
{
    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, Unit>
    {
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContext;
        private static string tokenRedisKey = "tokens";

        public LogoutUserCommandHandler(IDistributedCache cache, IHttpContextAccessor context)
        {
            _cache = cache;
            _httpContext = context;
        }
        public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            var tokenToBlackList = _httpContext.HttpContext.GetAuthToken();
            var alreadyBlacklisted = await _cache.GetRecordAsync<List<string>>(tokenRedisKey);

            if (tokenToBlackList is not null)
            {
                if(alreadyBlacklisted is not null)
                {
                    alreadyBlacklisted.Add(tokenToBlackList);   
                } else
                {
                    alreadyBlacklisted = new List<string>() { tokenToBlackList };
                }
                await _cache.SetRecordAsync(tokenRedisKey, alreadyBlacklisted);
            }

            return Unit.Value;
        }
    }
}
