// <copyright file="NotFoundException.cs" company="Cole Clark">
// Copyright (c) Cole Clark. Licensed under the MIT License.
// </copyright>

namespace URL_Shortening_Service.Backend.Exceptions
{
    /// <summary>
    /// NotFoundException is thrown when the database can't find a shortURL.
    /// </summary>
    public class NotFoundException(string message)
    : Exception(message)
    {
    }
}
