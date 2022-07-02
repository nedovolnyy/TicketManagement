﻿using System.Threading.Tasks;

namespace TicketManagement.Common.DI
{
    public interface IAreaService : IService<IArea>
    {
        Task ValidateAsync(IArea entity);
    }
}
