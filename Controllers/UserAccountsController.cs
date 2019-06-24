using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kazidocs2.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Kazidocs2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAllOrigins")]
    public class UserAccountsController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly KazidocsContext _context;
        private DateTime TokenExpire { get; set; }

        /////Methods for User Account/////
        

        public UserAccountsController(KazidocsContext kazidocsContext, 
            UserManager<IdentityUser> userManager,
           SignInManager<IdentityUser> signInManager,
           IConfiguration configuration,
           RoleManager<IdentityRole> _roleManager)
        {
            _context = kazidocsContext;

            if (_context.UserAccounts.Count() == 0)
            {
                _context.UserAccounts.Add(new UserAccounts { Name = "Item1" });
                _context.SaveChanges();
            }

            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = kazidocsContext;
            roleManager = _roleManager;

        }

        //Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAccounts>>> GetUserAccounts(long id)
        {
            return await _context.UserAccounts.ToListAsync();
        }

        //Get with id
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAccounts>> GetUserAccount(long id)
        {
            var userAccount = await _context.UserAccounts.FindAsync(id);

            if (userAccount == null)
            {
                return NotFound();
            }

            return userAccount;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<object> AddUserAccount([FromBody]UserAccounts userAccount)
        {
            var getUser = _context.UserAccounts.SingleOrDefault(u => u.Email == userAccount.Email);
            if (getUser != null)
            {
                if (getUser.CanLogin == false)
                {
                    return StatusCode(200, new
                    {
                        status = 104,
                        message = "email or password incorrect"
                    });
                }
                if (getUser.AccountBlocked == true)
                {
                    return StatusCode(200, new
                    {
                        status = 104,
                        message = "account has been blocked, please contact admin"
                    });
                }
            }

            var result = await _signInManager.PasswordSignInAsync(userAccount.Email, userAccount.Password, false, false);
            if (result.Succeeded)
            {
                var user = _userManager.Users.SingleOrDefault(r => r.Email == userAccount.Email);
                var token = GenerateJwtToken(user.Email, user);
                var role = new object();

                try
                {
                    var getrole = _context.UserRoles.SingleOrDefault(u => u.UserId == user.Id);
                    role = _context.Roles.SingleOrDefault(u => u.Id == getrole.RoleId);

                    var checkPrevious = _context.Tokens.AsNoTracking().SingleOrDefault(u => u.AccountId == user.Id);
                    if (checkPrevious == null)
                    {
                        var newToken = new UserTokens
                        {
                            Id = Guid.NewGuid().ToString(),
                            AccountId = user.Id,
                            Expiry = TokenExpire,
                            Token = token.ToString()
                        };

                        await _context.AddAsync(newToken);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        checkPrevious.Token = token.ToString();
                        checkPrevious.Expiry = TokenExpire;

                        _context.Entry(checkPrevious).State = EntityState.Modified;
                        _context.Update(checkPrevious);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        status = 500,
                        message = ex.Message
                    });
                }
                try
                {
                    return StatusCode(200, new
                    {
                        status = 102,
                        message = "Log in successful",
                        data = new
                        {
                            user = new
                            {
                                id = user.Id,
                                username = user.UserName,
                                email = user.Email,
                            },
                            role,
                            accesstoken = new
                            {
                                token,
                                expire = TokenExpire
                            }
                        }
                    });


                }
                catch (Exception ex)
                {
                    return StatusCode(200, new
                    {
                        status = 104,
                        message = ex.Message
                    });
                }
            }
            else
            {
                return StatusCode(200, new
                {
                    status = 104,
                    message = "email or password incorrect"
                });
            }
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Register([FromRoute]string email, [FromRoute]string password)
        {
            var user = new IdentityUser { Email = email, UserName = email };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var token = GenerateJwtToken(user.Email, user);
                return StatusCode(200, new
                {
                    status = 102,
                    message = "Log in successful",
                    data = new
                    {
                        user = new
                        {
                            id = user.Id,
                            username = user.UserName,
                            email = user.Email,
                        },
                        accesstoken = new
                        {
                            token,
                            expire = TokenExpire
                        }
                    }
                });
            }
            else
            {
                return BadRequest();
            }
        }

        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAccount(long id, UserAccounts userAccount)
        {
            if (id != userAccount.Id)
            {
                return BadRequest();
            }

            _context.Entry(userAccount).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAccouunt(long id)
        {
            var todoItem = await _context.UserAccounts.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.UserAccounts.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private object GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            TokenExpire = DateTime.Now.AddDays(1);
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtIssuer"],
                audience: _configuration["JwtIssuer"],
                claims: claims,
                expires: TokenExpire,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}