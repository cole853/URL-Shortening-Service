// <copyright file="ShortURL.cs" company="Cole Clark">
// Copyright (c) Cole Clark. Licensed under the MIT License.
// </copyright>

namespace URL_Shortening_Service.Backend.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

    /// <summary>
    /// ShortURL class used to represent a row in the database's table.
    /// </summary>
    public class ShortURL
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShortURL"/> class.
        /// </summary>
        /// <param name="url">The url that will be shortened.</param>
        /// <param name="shortCode">The short code that will be used.</param>
        public ShortURL(string url, string shortCode)
        {
            this.Url = "tempURL";
            this.UpdateURL(url);
            this.ShortCode = shortCode;
            this.CreatedAt = DateTime.UtcNow;
            this.AccessCount = 0;
        }

        private ShortURL()
        {
            this.Url = string.Empty;
            this.ShortCode = string.Empty;
        }

        /// <summary>
        /// Gets the ID for the shortURL.
        /// </summary>
        public int Id { get; private init; }

        /// <summary>
        /// Gets the url of the shortURL.
        /// </summary>
        [Required(ErrorMessage = "URL required to create ShortURL")]
        [Url(ErrorMessage = "Invalid URL format")]
        [MaxLength(2000, ErrorMessage = "Url must be under 2000 characters")]
        public string Url { get; private set; }

        /// <summary>
        /// Gets the code for the shortURL.
        /// </summary>
        [Required(ErrorMessage = "Short code required to Get or Delete ShortURL")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Short code must be 10 characters long")]
        [RegularExpression(pattern: "^[a-z0-9]+$")]
        public string ShortCode { get; private init; }

        /// <summary>
        /// Gets the time the shortURL was created.
        /// </summary>
        public DateTime CreatedAt { get; private init; }

        /// <summary>
        /// Gets the time the shortURL was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// Gets the number of times the shortURL was accessed.
        /// </summary>
        public int AccessCount { get; private set; }

        /// <summary>
        /// Increments the AccessCount by 1.
        /// </summary>
        public void IncrementAccessCount()
        {
            this.AccessCount++;
        }

        /// <summary>
        /// updates the url.
        /// </summary>
        /// <param name="updatedURL">The updated url that will replace the old url.</param>
        public void UpdateURL(string updatedURL)
        {
            if (!string.IsNullOrEmpty(updatedURL) && updatedURL.Length <= 2000)
            {
                this.Url = updatedURL;
                this.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                throw new ArgumentException("URL cannot be null, empty, or exceed 2000 characters");
            }
        }
    }
}