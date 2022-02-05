using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Database.Context;
using Models.Database.Entities;
using Models.Request;
using Models.Response.Auth;
using Newtonsoft.Json;

namespace Ecom.Controllers
{
    public class AuthController : Controller
    {
        private DatabaseContext _context;
        public AuthController(DatabaseContext _context)
        {
            this._context = _context;
        }

        [Route("/Auth/Login")]
        public IActionResult Login()
        {
            LoginRequest loginRequest = new LoginRequest();
            return View(loginRequest);
        }

        [Route("/Auth/Register")]
        public IActionResult Register()
        {
            RegisterRequest request = new RegisterRequest();
            return View(request);
        }

        [Route("/Auth/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Auth/Login");
        }



        [Route("/Auth/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Fields required";
                return View("Login", loginRequest);
            }

            var user = await (
                        from users in _context.users
                        join userRoles in _context.userRoles
                        on users.userId equals userRoles.userId

                        join roles in _context.roles
                        on userRoles.roleId equals roles.roleId

                        where (users.email == loginRequest.email && users.password == hashPassword(loginRequest.password))

                        select new LoginResponse()
                        {
                            userId = users.userId,
                            name = users.name,
                            email = users.email,
                            roleId = roles.roleId,
                            roleName = roles.roleName
                        })
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Incorrect credentials";
                return View("Login", loginRequest);

            }
            else
            {
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                HttpContext.Session.SetString("userId", user.userId.ToString());
                return Redirect("/Home");
            }
        }


        [Route("/Auth/Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Fields required";
                return View("Register", request);
            }

            var user = new Users()
            {
                email = request.email,
                name = request.email,
                password = hashPassword(request.password)
            };

            await _context.users.AddAsync(user).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            var role = await _context.roles.Where(x => x.roleName.ToLower() == "user").SingleOrDefaultAsync().ConfigureAwait(false);
            await _context.userRoles.AddAsync(new UserRoles()
            {
                userId = user.userId,
                roleId = role.roleId
            }).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            var loginResponse = new LoginResponse()
            {
                userId = user.userId,
                name = user.name,
                email = user.email,
                roleId = role.roleId,
                roleName = role.roleName
            };
            HttpContext.Session.SetString("user", JsonConvert.SerializeObject(loginResponse));
            HttpContext.Session.SetString("userId", user.userId.ToString());
            return Redirect("/Home");
        }



        private string hashPassword(string password)
        {
            try
            {
                string hashedPassword = "";
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }

                return hashedPassword;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
