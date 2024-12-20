﻿using WalletAspNetCore.Models.Entities;

namespace WalletAspNetCore.Services.Interfaces
{
    public interface IUsersService
    {
        Task<User> GetUserByIdAsync(Guid userId);
    }
}