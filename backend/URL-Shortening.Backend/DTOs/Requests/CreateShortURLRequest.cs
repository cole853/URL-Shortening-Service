// <copyright file="CreateShortURLRequest.cs" company="Cole Clark">
// Copyright (c) Cole Clark. Licensed under the MIT License.
// </copyright>

namespace URL_Shortening_Service.Backend.DTOs.Requests
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// This is a model to wrap the Url string that will be added to the database.
    /// </summary>
    public class CreateShortURLRequest
    {
        /// <summary>
        /// Gets or Sets the Url in the CreateShortURLRequest.
        /// This is the Url string that will be added to the database.
        /// </summary>
        [Required(ErrorMessage = "URL required to create ShortURL")]
        [Url(ErrorMessage = "Invalid URL format")]
        [MaxLength(2000, ErrorMessage = "Url must be under 2000 characters")]
        required public string Url { get; set; }
    }
}
