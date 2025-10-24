using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecom.API.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [Authorize]
        [HttpGet("get-address-for-user")]
        public async Task<IActionResult> getAddress()
        {
         
            var email = User.FindFirst(ClaimTypes.Email)?.Value
                       ?? User.FindFirst("email")?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized(new ResponseAPI(401, "User email not found. Please login again."));
            }

            var address = await work.Auth.getUserAddress(email);
            var result = mapper.Map<ShipAddressDTO>(address);
            return Ok(result);
        }

        [HttpGet("Logout")]
        public IActionResult logout()
        {
         
            Response.Cookies.Append("token", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Domain = "localhost",
                Expires = DateTime.Now.AddDays(-1)
            });

            return Ok(new ResponseAPI(200, "Logged out successfully"));
        }

        [Authorize]
        [HttpGet("get-user-name")]
        public IActionResult GetUserName()
        {
            var name = User.Identity?.Name
                      ?? User.FindFirst("name")?.Value
                      ?? User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(name))
            {
                return Ok(new ResponseAPI(200, "Anonymous"));
            }

            return Ok(new ResponseAPI(200, name));
        }

        [HttpGet("IsUserAuth")]
        public IActionResult IsUserAuth()
        {
            return User.Identity?.IsAuthenticated == true
                ? Ok(new ResponseAPI(200, "Authenticated"))
                : Unauthorized(new ResponseAPI(401, "Not authenticated"));
        }

        [Authorize]
        [HttpPut("update-address")]
        public async Task<IActionResult> updateAddress(ShipAddressDTO addressDTO)
        {
            
            var email = User.FindFirst(ClaimTypes.Email)?.Value
                       ?? User.FindFirst("email")?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized(new ResponseAPI(401, "User email not found. Please login again."));
            }

            var address = mapper.Map<Address>(addressDTO);
            var result = await work.Auth.UpdateAddress(email, address);

            return result
                ? Ok(new ResponseAPI(200, "Address updated successfully"))
                : BadRequest(new ResponseAPI(400, "Failed to update address"));
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterDTO>> register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseAPI(400, "Invalid registration data"));
            }

            string result = await work.Auth.RegisterAsync(registerDTO);

            if (result != "done")
            {
                return BadRequest(new ResponseAPI(400, result));
            }

            return Ok(new ResponseAPI(200, "Registration successful"));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseAPI(400, "Invalid login data"));
            }

            string result = await work.Auth.LoginAsync(loginDTO);

            if (result.StartsWith("please"))
            {
                return BadRequest(new ResponseAPI(400, result));
            }

            
            Response.Cookies.Append("token", result, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Domain = "localhost",
                Expires = DateTime.Now.AddDays(7) 
            });

            
            return Ok(new
            {
                statusCode = 200,
                msg = "Login successful",
                token = result,
                expiresIn = 7 * 24 * 60 * 60 
            });
        }

        [HttpPost("active-account")]
        public async Task<ActionResult<ActiveAccountDTO>> active(ActiveAccountDTO accountDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseAPI(400, "Invalid data"));
            }

            var result = await work.Auth.ActiveAccount(accountDTO);

            return result
                ? Ok(new ResponseAPI(200, "Account activated successfully"))
                : BadRequest(new ResponseAPI(400, "Failed to activate account"));
        }

        [HttpGet("send-email-forget-password")]
        public async Task<IActionResult> forget(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new ResponseAPI(400, "Email is required"));
            }

            var result = await work.Auth.SendEmailForForgetPassword(email);

            return result
                ? Ok(new ResponseAPI(200, "Password reset email sent"))
                : BadRequest(new ResponseAPI(400, "Failed to send email"));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> reset(ResetPasswordDTO restPasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseAPI(400, "Invalid data"));
            }

            var result = await work.Auth.ResetPassword(restPasswordDTO);

            if (result == "done")
            {
                return Ok(new ResponseAPI(200, "Password reset successful"));
            }

            return BadRequest(new ResponseAPI(400, result));
        }

        
        [Authorize]
        [HttpGet("debug-claims")]
        public IActionResult DebugClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();

            return Ok(new
            {
                isAuthenticated = User.Identity?.IsAuthenticated,
                authenticationType = User.Identity?.AuthenticationType,
                name = User.Identity?.Name,
                claims = claims
            });
        }
    }
}