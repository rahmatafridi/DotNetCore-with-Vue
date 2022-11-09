using AdminSite.Framework;
using AdminSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdminSite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJWTManagerRepository _jWTManager;

        public AccountController(IJWTManagerRepository jWTManager)
        {
            _jWTManager = jWTManager;
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (login.Email == "admin@admin.com" && login.Password == "admin")
            {
                var user = new User
                {
                    Id = 1,
                    Email = login.Email,
                    Password = login.Password,
                    Firstname = "Admin",
                    Lastname = "Admin",
                    Username = "Admin",
                    UserRoleId = 1
                };

                var token = _jWTManager.Authenticate(user);

                user.Token = token.Token;

                return Ok(new { status = StatusCodes.Status200OK, obj = user });
            }
            return Ok(new { status = StatusCodes.Status401Unauthorized, obj = "Email or Password is not correct." });
        }

    }
}
