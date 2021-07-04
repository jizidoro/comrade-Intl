// <copyright file="IUserService.cs" company="Ivan Paulovich">
// Copyright © Ivan Paulovich. All rights reserved.
// </copyright>

namespace Comrade.Application.Interfaces
{
    /// <summary>
    ///     User Service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        ///     Gets the Current User Id.
        /// </summary>
        /// <returns>User.</returns>
        string GetCurrentUserId();
    }
}
