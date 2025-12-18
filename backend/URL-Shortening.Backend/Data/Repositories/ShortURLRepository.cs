// <copyright file="ShortURLRepository.cs" company="Cole Clark">
// Copyright (c) Cole Clark. Licensed under the MIT License.
// </copyright>

namespace URL_Shortening_Service.Backend.Data
{
    using Microsoft.EntityFrameworkCore;
    using URL_Shortening_Service.Backend.DTOs.Requests;
    using URL_Shortening_Service.Backend.Exceptions;
    using URL_Shortening_Service.Backend.Models;

    /// <summary>
    /// The repository class that implements methods in IShortURLRepository.
    /// These are used by the controller to access the database.
    /// </summary>
    public class ShortURLRepository(AppDbContext inputContext)
    : IShortURLRepository
    {
        private readonly AppDbContext context = inputContext;

        /// <summary>
        /// Adds a new ShortURL to the database.
        /// </summary>
        /// <param name="createRequest">The request containing the url that will be shortened.</param>
        /// <returns>The new ShortURL.</returns>
        public async Task<ShortURL> Post(CreateShortURLRequest createRequest)
        {
            string shortCode = await this.GenerateUniqueShortCodeAsync();
            ShortURL tempShortURL = new ShortURL(createRequest.Url, shortCode);

            await this.context.ShortURLs.AddAsync(tempShortURL);
            await this.context.SaveChangesAsync();

            return tempShortURL;
        }

        /// <summary>
        /// Gets the shortURL with the given shortCode.
        /// </summary>
        /// <param name="shortCode">The shortcode of the ShortURL that will be returned.</param>
        /// <param name="incrementAccess">Indicates whether the ShortURL accessCount should be incremented.</param>
        /// <returns>The ShortURL with the given shortCode.</returns>
        /// <exception cref="NotFoundException">Thrown if the id was not found in the database.</exception>
        public async Task<ShortURL> Get(string shortCode, bool incrementAccess = false)
        {
            var tempShortURL = await this.context.ShortURLs.FirstOrDefaultAsync(x => x.ShortCode == shortCode);
            if (tempShortURL is null)
            {
                throw new NotFoundException("The short code of the shorturl was not Found");
            }

            if (incrementAccess)
            {
                tempShortURL.IncrementAccessCount();
                await this.context.SaveChangesAsync();
            }

            return tempShortURL;
        }

        /// <summary>
        /// Updates the url in a shortURL.
        /// </summary>
        /// <param name="editRequest">The request containing the Id of the shortURL to be updated and the updated URL.</param>
        /// <returns>The updated ShortURL.</returns>
        /// <exception cref="NotFoundException">Thrown if the id was not found in the database.</exception>
        public async Task<ShortURL> Put(EditShortURLRequest editRequest)
        {
            var tempShortURL = await this.context.ShortURLs.FindAsync(editRequest.Id);
            if (tempShortURL is null)
            {
                throw new NotFoundException("The Id of the shorturl was not Found");
            }

            tempShortURL.UpdateURL(editRequest.Url);
            await this.context.SaveChangesAsync();

            return tempShortURL;
        }

        /// <summary>
        /// Deletes a ShortURL.
        /// </summary>
        /// <param name="id">The Id of the shortURL to be deleted.</param>
        /// <returns>True if deleted.</returns>
        /// <exception cref="NotFoundException">Thrown if the id was not found in the database.</exception>
        public async Task<bool> Delete(int id)
        {
            var tempShortURL = await this.context.ShortURLs.FindAsync(id);
            if (tempShortURL is null)
            {
                throw new NotFoundException("The Id of the shorturl was not Found");
            }

            this.context.ShortURLs.Remove(tempShortURL);
            await this.context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Generates a shortcode for the shortURL.
        /// </summary>
        /// <returns>The shortcode that was generated with 10 lowercase letters or numbers.</returns>
        private static string GenerateShortCode()
        {
            string characters = "abcdefghijklmnopqrstuvwxyz1234567890";
            Random random = new Random();
            int charIndex = 0;

            string result = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                charIndex = random.Next(characters.Length);
                result += characters[charIndex];
            }

            return result;
        }

        /// <summary>
        /// Generates short codes to see if they would be unique in the database.
        /// </summary>
        /// <param name="maxAttempts">The maximum number of shortcodes that will be generated and checked for uniqueness.</param>
        /// <returns>A short code that will be unique in the database.</returns>
        /// <exception cref="Exception">Thrown when the max attemps are used without finding a unique code.</exception>
        private async Task<string> GenerateUniqueShortCodeAsync(int maxAttempts = 10)
        {
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                string shortCode = GenerateShortCode();

                var exists = await this.context.ShortURLs.AnyAsync(x => x.ShortCode == shortCode);

                if (!exists)
                {
                    return shortCode;
                }
            }

            throw new Exception($"Could not create a unique shortcode after {maxAttempts} attempts");
        }
    }
}