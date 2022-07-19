using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HMS_BE.Models;
using HMS_BE.Utils;
using AutoMapper;
using HMS_BE.Repository;
using HMS_BE.DTO.PagingModel;
using HMS_BE.DTO.SearchModel;
using HMS_BE.DTO.LoginModel;
using Microsoft.Extensions.Configuration;
using System.IO;
using Utilities;

namespace HMS_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            if (login.Username == config["Admin:Username"] &&
                login.Password == config["Admin:Password"])
            {
                string tokenString = CreateAuthenToken.GetToken("Admin");
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
