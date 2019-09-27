using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        //GET : /api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.FullName,
                user.Email,
                user.UserName
            };
        }

        [HttpGet]
        [Authorize(Roles = "Menjacnica")]
        [Route("ForMenjacnica")]
        public string GetForMenjacnica()
        {
            return "Web method for Menjačnica";
        }

        [HttpGet]
        [Authorize(Roles = "Korisnik")]
        [Route("ForKorisnik")]
        public string GetForKorisnik()
        {
            return "Web method for Korisnik";
        }

        [HttpGet]
        [Authorize(Roles = "Menjacnica,Korisnik")]
        [Route("ForMenjačnicaOrKorisnik")]
        public string GetForMenjačnicaOrKorisnik()
        {
            return "Web method for Menjacnica or Korisnik";
        }
    }
}