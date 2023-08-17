﻿using MediatR;
using Messenger.App.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.App.Queries
{
    public class GetAllUsersQuery : IRequest<GetAllUsersResponse>
    {
    }
}
