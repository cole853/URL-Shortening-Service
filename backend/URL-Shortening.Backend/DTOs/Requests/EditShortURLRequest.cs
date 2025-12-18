// <copyright file="EditShortURLRequest.cs" company="Cole Clark">
// Copyright (c) Cole Clark. Licensed under the MIT License.
// </copyright>

namespace URL_Shortening_Service.Backend.DTOs.Requests
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// This is a model to wrap the Id of the shortURL that will be changed with the updated URL.
    /// The updated URL are inherited from the CreateShortURLRequest class.
    /// </summary>
    public class EditShortURLRequest
    : CreateShortURLRequest
    {
        /// <summary>
        /// Gets or Sets the Id of the shortURL that will be updated.
        /// </summary>
        [Required(ErrorMessage = "Id is required to edit the URL")]
        required public int Id { get; set; }
    }
}