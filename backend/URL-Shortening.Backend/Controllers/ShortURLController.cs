// <copyright file="ShortURLController.cs" company="Cole Clark">
// Copyright (c) Cole Clark. Licensed under the MIT License.
// </copyright>

namespace URL_Shortening_Service.Backend.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using URL_Shortening_Service.Backend.Models;

    /// <summary>
    /// Controller that contains the endpoints for the api.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ShortURLController(AppDbContext context)
    : ControllerBase
    {
        private readonly AppDbContext context = context;

        /// <summary>
        /// The function that is called when the endpoint /api/ShortUrl/GetShortURLs is accessed.
        /// </summary>
        /// <returns>returns the ok status with a list of shortURLs in the database.</returns>
        [HttpGet("GetShortURLs")]
        public async Task<IActionResult> GetShortURL()
        {
            var result = await this.context.ShortURLs.Select(x => new ShortURL
            {
                Id = x.Id,
                Url = x.Url,
                ShortCode = x.ShortCode,
            }).ToListAsync();

            return this.Ok(result);
        }
    }
}