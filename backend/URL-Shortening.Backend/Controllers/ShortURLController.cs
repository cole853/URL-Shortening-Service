// <copyright file="ShortURLController.cs" company="Cole Clark">
// Copyright (c) Cole Clark. Licensed under the MIT License.
// </copyright>

namespace URL_Shortening_Service.Backend.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using URL_Shortening_Service.Backend.Data;
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
        /// <param name="shortURL">The shortURL to be added to the database.</param>
        /// <returns>The new shortURL if successful and a status code.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShortURL shortURL)
        {
            return this.BadRequest("Test message");
        }

        /// <summary>
        /// This Get endpoint is used to get the information from one shortcode.
        /// </summary>
        /// <param name="shortCode">The shortCode to return information for.</param>
        /// <returns>The shortURL with the given shortCode if successful and a status code.</returns>
        [HttpGet("{shortCode}")]
        public async Task<IActionResult> Get(string shortCode)
        {
            return this.NotFound("Test message");
        }

        /// <summary>
        /// The Put endpoint updates a shortURL.
        /// </summary>
        /// <param name="shortURL">The shortURL to be updated with its change.</param>
        /// <returns>The updated shortURL if successful with a status code.</returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ShortURL shortURL)
        {
            return this.NotFound("Test message");
        }

        /// <summary>
        /// The Delete endpoint deletes one of the shortURLs.
        /// </summary>
        /// <param name="shortCode">The shortCode to be deleted.</param>
        /// <returns>A bool indicating whether the deletion was successful and a status code.</returns>
        [HttpDelete("{shortCode}")]
        public async Task<IActionResult> Delete(string shortCode)
        {
            return this.NotFound("Test message");
        }
    }
}