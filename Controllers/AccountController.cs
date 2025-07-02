using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using API.DTOs;
using API.Interface;
using API.Models;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(UserManager<User> userManager, ITokenService tokenService, IMapper mapper) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await UserExist(registerDTO.UserName)) return BadRequest("Usuario já cadastrado");

            using var hmac = new HMACSHA512();

            var user = mapper.Map<User>(registerDTO);

            user.UserName = registerDTO.UserName.ToUpper();

            var result = await userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return new UserDTO
            {
                UserName = user.UserName,
                Token = await tokenService.CreateToken(user),
                Aniversario = registerDTO.Aniversario,
                Cargo = registerDTO.Cargo,
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == loginDTO.UserName.ToUpper());

            if (user == null || user.UserName == null) return Unauthorized("Invalid Username");

            var result = await userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (!result) return Unauthorized();

            return new UserDTO
            {
                UserName = user.UserName,
                Aniversario = user.Aniversario,
                Cargo = user.Cargo,
                Token = await tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExist(string userName)
        {
            return await userManager.Users.AnyAsync(x => x.NormalizedUserName == userName.ToUpper());
        }
    }
}