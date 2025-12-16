// <copyright file="IShortURLRepository.cs" company="Cole Clark">
// Copyright (c) Cole Clark. Licensed under the MIT License.
// </copyright>

namespace URL_Shortening_Service.Backend.Data
{
    using URL_Shortening_Service.Backend.Models;

    /// <summary>
    /// An interface that ensures the ShortURLRepository implements the necessary methods.
    /// </summary>
    public interface IShortURLRepository
    {
        /// <summary>
        /// Creates a new ShortURL.
        /// </summary>
        /// <param name="shortURL">The ShortURL to be created.</param>
        /// <returns>The ShortURL that was created.</returns>
        ShortURL Post(ShortURL shortURL);

        /// <summary>
        /// Gets the ShortURL with the given shortCode.
        /// </summary>
        /// <param name="shortCode">The shortCode for the ShortURL that will be returned.</param>
        /// <returns>The ShortURL with the given shortCode.</returns>
        ShortURL Get(string shortCode);

        /// <summary>
        /// Updates the given ShortURL.
        /// </summary>
        /// <param name="shortURL">The ShortURL that needs to be changed.</param>
        /// <returns>The updated shortURL.</returns>
        ShortURL Put(ShortURL shortURL);

        /// <summary>
        /// Deletes one ShortURL.
        /// </summary>
        /// <param name="shortCode">The shortCode for the ShortURL that will be deleted.</param>
        /// <returns>A bool that is True if deletion was successful, false otherwise.</returns>
        bool Delete(string shortCode);
    }
}