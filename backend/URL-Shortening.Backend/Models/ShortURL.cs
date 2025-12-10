// <copyright file="ShortURL.cs" company="Cole Clark">
// Copyright (c) Cole Clark. Licensed under the MIT License.
// </copyright>

namespace URL_Shortening_Service.Backend.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// ShortURL class used to represent a row in the database's table.
    /// </summary>
    public class ShortURL
    {
        /// <summary>
        /// Gets or Sets the ID for the shortURL.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the url of the shortURL.
        /// </summary>
        required public string Url { get; set; }

        /// <summary>
        /// Gets or Sets the code for the shortURL.
        /// </summary>
        required public string ShortCode { get; set; }

        /// <summary>
        /// Gets or Sets the time the shortURL was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or Sets the time the shortURL was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or Sets the number of times the shortURL was accessed.
        /// </summary>
        public int AccessCount { get; set; }
    }
}