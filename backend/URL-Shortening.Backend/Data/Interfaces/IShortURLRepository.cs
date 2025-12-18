// <copyright file="IShortURLRepository.cs" company="Cole Clark">
// Copyright (c) Cole Clark. Licensed under the MIT License.
// </copyright>

namespace URL_Shortening_Service.Backend.Data
{
    using URL_Shortening_Service.Backend.DTOs.Requests;
    using URL_Shortening_Service.Backend.Models;

    /// <summary>
    /// An interface that ensures the ShortURLRepository implements the necessary methods.
    /// </summary>
    public interface IShortURLRepository
    {
        /// <summary>
        /// Creates a new ShortURL.
        /// </summary>
        /// <param name="createRequest">The createRequest contains the link to be shortened and added to the database.</param>
        /// <returns>The ShortURL that was created.</returns>
        Task<ShortURL> Post(CreateShortURLRequest createRequest);

        /// <summary>
        /// Gets the ShortURL with the given shortCode.
        /// </summary>
        /// <param name="shortCode">The shortCode for the ShortURL that will be returned.</param>
        /// <param name="incrementAccess">Indicates whether the ShortURL accessCount should be incremented.</param>
        /// <returns>The ShortURL with the given shortCode.</returns>
        Task<ShortURL> Get(string shortCode, bool incrementAccess = false);

        /// <summary>
        /// Updates the given ShortURL.
        /// </summary>
        /// <param name="editRequest">The id and updated url that needs to be changed.</param>
        /// <returns>The updated shortURL.</returns>
        Task<ShortURL> Put(EditShortURLRequest editRequest);

        /// <summary>
        /// Deletes one ShortURL.
        /// </summary>
        /// <param name="id">The id of the ShortURL that will be deleted.</param>
        /// <returns>A bool that is True if deletion was successful, false otherwise.</returns>
        Task<bool> Delete(int id);
    }
}