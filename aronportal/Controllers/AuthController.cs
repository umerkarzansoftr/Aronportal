using aronportal.Auth;
using aronportal.DTOS;
using aronportal.Models;
using aronportal.Repositories;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace aronportal.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : System.Web.Http.ApiController
    {
        //private readonly IAuthRepository _AuthRepository;
        //private readonly IConfiguration _config;
        //private readonly IMapper _mapper;

        //public AuthController(IAuthRepository AuthRepository, IConfiguration config, IMapper mapper)
        //{
        //    _mapper = mapper;
        //    _config = config;
        //    _AuthRepository = AuthRepository;
        //}
        AronPortalDbContext _context = new AronPortalDbContext();
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                salt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }


        [HttpPost, Route("register")]
        public async Task<ActionResult> Register(RegisterDTO registerDto)
        {
            registerDto.Email = registerDto.Email.ToLower();
            if ( _context.TblUser.Any(x => x.Email == registerDto.Email))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Email already exists");
            byte[] passwordHash, salt;
            CreatePasswordHash(registerDto.Password, out passwordHash, out salt);
            TblUser user = new TblUser();
            user.Password = passwordHash;
            user.Salt = salt;
            user.Email = registerDto.Email;
            user.FullName = registerDto.FullName;
            _context.TblUser.Add(user);
            await _context.SaveChangesAsync();

            return new HttpStatusCodeResult(HttpStatusCode.Created, new { email = user.Email, fullname = user.FullName }.ToString());
        }
        [HttpPost, Route("login")]
        public async Task<ActionResult> Login(LoginDTO loginDto)
        {
            var user =  _context.TblUser.FirstOrDefault(x => x.Email == loginDto.Email);
            if (user == null)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            if (!VerifyPasswordHash(loginDto.Password, user.Password, user.Salt))
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
           


            return new HttpStatusCodeResult(HttpStatusCode.OK,
                JwtAuthManager.GenerateJWTToken(loginDto.Email));
        

           // return Ok(new { token = tokenHandler.WriteToken(token), email = userFromRepo.Email, fullname = userFromRepo.FullName });
        }
      
    }
}
