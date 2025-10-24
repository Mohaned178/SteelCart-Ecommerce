using Ecom.Core.DTO;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRating _rating;
        private readonly IConfiguration _config;

        public RatingsController(IRating rating, IConfiguration config)
        {
            _rating = rating;
            _config = config;
        }

        [HttpGet("get-rating/{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int productId)
        {
            var result = await _rating.GetAllRatingForProductAsync(productId);
            return Ok(new { success = true, data = result });
        }

        [HttpPost("add-rating")]
        [AllowAnonymous]
        public async Task<IActionResult> Add(RatingDTO ratings)
        {
            string? email = null;

         
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length);

                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_config["JWT:Key"]);

                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    email = jwtToken.Claims.FirstOrDefault(c =>
                        c.Type == ClaimTypes.Email ||
                        c.Type == "email" ||
                        c.Type == ClaimTypes.NameIdentifier)?.Value;
                }
                catch
                {
           
                }
            }

            try
            {
                var result = await _rating.AddRatingAsync(ratings, email);

                if (!result)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "You have already rated this product"
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = email != null
                        ? $"Rating added successfully by {email}"
                        : "Rating added successfully as Anonymous"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while adding rating",
                    error = ex.Message
                });
            }
        }
    }
}
