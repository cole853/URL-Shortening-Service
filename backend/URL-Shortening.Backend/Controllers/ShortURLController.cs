// <copyright file="ShortURLController.cs" company="Cole Clark">
// Copyright (c) Cole Clark. Licensed under the MIT License.
// </copyright>

namespace URL_Shortening_Service.Backend.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using URL_Shortening_Service.Backend.Data;
    using URL_Shortening_Service.Backend.DTOs.Requests;
    using URL_Shortening_Service.Backend.Exceptions;
    using URL_Shortening_Service.Backend.Models;

    /// <summary>
    /// Controller that contains the endpoints for the api.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ShortURLController
    : ControllerBase
    {
        private readonly IShortURLRepository shortURLRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortURLController"/> class.
        /// </summary>
        /// <param name="shortURLRepository">The shortURLRepository that will be used to communicate with the database.</param>
        public ShortURLController(IShortURLRepository shortURLRepository)
        {
            this.shortURLRepo = shortURLRepository;
        }

        /// <summary>
        /// The Post endpoint used to create a new shortened URL.
        /// </summary>
        /// <param name="createRequest">The createRequest contains the link to be shortened and added to the database.</param>
        /// <returns>The new shortURL if successful and a status code.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateShortURLRequest createRequest)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = await this.shortURLRepo.Post(createRequest);

                    return this.StatusCode(201, result);
                }
                else
                {
                    return this.BadRequest("Invalid CreateShortURLRequest Model");
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// This Get endpoint is used to get the information from one shortcode.
        /// </summary>
        /// <param name="shortCode">The short code to return information for.</param>
        /// <returns>The shortURL with the given shortCode if successful and a status code.</returns>
        [HttpGet("{shortCode}")]
        public async Task<IActionResult> Get(string shortCode)
        {
            try
            {
                bool ne = string.IsNullOrEmpty(shortCode);
                bool lengthBad = shortCode.Length != 10;
                bool structureBad = !System.Text.RegularExpressions.Regex.IsMatch(shortCode, "^[a-z0-9]+$");
                if (ne || lengthBad || structureBad)
                {
                    return this.BadRequest("Short code is invalid");
                }

                var result = await this.shortURLRepo.Get(shortCode);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return this.NotFound(ex.Message);
                }
                else
                {
                    return this.BadRequest(ex.Message);
                }
            }
        }

        /// <summary>
        /// The Put endpoint updates a shortURL.
        /// </summary>
        /// <param name="editRequest">The Id of and updated url to be edited.</param>
        /// <returns>The updated shortURL if successful with a status code.</returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditShortURLRequest editRequest)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = await this.shortURLRepo.Put(editRequest);

                    return this.Ok(result);
                }
                else
                {
                    return this.BadRequest("Invalid EditShortURLRequest Model");
                }
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return this.NotFound(ex.Message);
                }
                else
                {
                    return this.BadRequest(ex.Message);
                }
            }
        }

        /// <summary>
        /// The Delete endpoint deletes one of the shortURLs.
        /// </summary>
        /// <param name="id">The id of the shortCode to be deleted.</param>
        /// <returns>A bool indicating whether the deletion was successful and a status code.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await this.shortURLRepo.Delete(id);

                return this.StatusCode(204, result);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return this.NotFound(ex.Message);
                }
                else
                {
                    return this.BadRequest(ex.Message);
                }
            }
        }
    }
}