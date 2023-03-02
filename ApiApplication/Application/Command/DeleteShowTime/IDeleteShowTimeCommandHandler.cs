﻿using ApiApplication.Application.Commands;
using ApiApplication.Core.CQRS;

namespace ApiApplication.Application.Command
{
    public interface IDeleteShowTimeCommandHandler : IAsyncCommandHandler<DeleteShowTimeRequest, DeleteShowTimeResponse>
    {
    }
}
