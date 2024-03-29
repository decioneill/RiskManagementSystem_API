﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using RiskManagementSystem_API.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using RiskManagementSystem_API.Services;
using RiskManagementSystem_API.Entities;
using RiskManagementSystem_API.Models.Users;

namespace RiskManagementSystem_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Authenticates User of Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                RiskManager = user.RiskManager,
                Admin = user.Admin,
                Email = user.Email,
                Token = tokenString
            });
        }

        /// <summary>
        /// Registers new User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterModel model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {                
                // create user
                _userService.Create(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves all users if permitted
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll(string userId)
        {
            Guid id = Guid.Parse(userId);
            var user = _userService.GetById(id);
            if (user != null && (user.Admin || user.RiskManager))
            {
                var users = _userService.GetAll();
                var model = _mapper.Map<IList<UserModel>>(users);
                return Ok(model);
            }
            return null;
        }

        /// <summary>
        /// Gets user by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Guid userId = Guid.Parse(id);
            var user = _userService.GetById(userId);
            var model = _mapper.Map<UserModel>(user);
            return Ok(model);
        }

        /// <summary>
        /// Checks user id against role.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns>value of role</returns>
        [HttpGet("{id}/{role}")]
        public bool CheckRole(string id, Role role)
        {
            Guid userId = Guid.Parse(id);
            bool hasRole = _userService.CheckRole(userId, role);
            return hasRole;
        }

        /// <summary>
        /// Updates user with new user details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody]UpdateModel model)
        {
            // map model to entity and set id
            var user = _mapper.Map<User>(model);
            user.Id = Guid.Parse(id);

            try
            {
                // update user 
                _userService.Update(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Removes User from Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Guid userId = Guid.Parse(id);
            _userService.Delete(userId);
            return Ok();
        }
    }
}
