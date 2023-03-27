using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GraduationProject.Repositry;
using GraduationProject.ExtenstionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.ComponentModel.DataAnnotations;
using GraduationProject.Areas.Api.ViewModels;

namespace GraduationProject.Areas.Api.Controllers
{
    [Route("api/[controller]/Edit/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUsersRepository _usersRepository;

        public ProfileController(UserManager<ApplicationUser> userManager, IUsersRepository usersRepository)
        {
            _userManager = userManager;
            _usersRepository = usersRepository;
        }


        [HttpPost]
        [Route("Email")]
        public async Task<IActionResult> ChangeEmail([FromBody] ProfileEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.Email != null)
                {
                    string userId = User.GetUserIdToken();
                    var user = await _userManager.FindByIdAsync(userId);
                    var result = await _userManager.SetEmailAsync(user, model.Email);
                
                    if (result.Succeeded)
                        return Ok(new { message = "Email Successfully Updated."});
                    else
                        return BadRequest( "Email Update was Unsuccessful." );
                }

                return BadRequest("Email change was unsuccessful.");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [Route("Password")]
        public async Task<IActionResult> ChangePassword([FromBody] ProfileEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.Password != null && model.CurrentPassword != null)
                {
                    //ID returns Email, This is a placeholder.
                    string userId = User.GetUserIdToken();
                    var user = await _userManager.FindByIdAsync(userId);
                    var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);

                    if (result.Succeeded)
                        return Ok(new { message = "Password Successfully Updated."});
                    else
                        return BadRequest(result.Errors.ToList()[0].Description);
                }

                return BadRequest("Password change was unsuccessful.");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [Route("Phone")]
        public async Task<IActionResult> ChangePhone([FromBody] ProfileEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.PhoneNumber != null)
                {
                    string userId = User.GetUserIdToken();
                    var user = await _userManager.FindByIdAsync(userId);
                    var result = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);

                    if (result.Succeeded)
                        return Ok(new {message = "Phone Number Successfully Updated." });
                    else
                        return BadRequest("Phone Number Update was Unsuccessful.");
                }

                return BadRequest("Phone Number change was unsuccessful.");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [Route("Username")]
        public async Task<IActionResult> ChangeUsername([FromBody] ProfileEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.Username != null)
                {
                    string userId = User.GetUserIdToken();
                    var user = await _userManager.FindByIdAsync(userId);
                    var result = await _userManager.SetUserNameAsync(user, model.Username);

                    if (result.Succeeded)
                        return Ok(new { message = "Username Successfully Updated." });
                    else
                        return BadRequest("Username Update was Unsuccessful.");
                }

                return BadRequest("Username change was unsuccessful.");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        [HttpPost]
        [Route("Address")]
        public async Task<IActionResult> ChangeAddress([FromBody] ProfileEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.Address != null)
                {
                    string userId = User.GetUserIdToken();
                    var user = await _userManager.FindByIdAsync(userId);
                    user.Address = model.Address;
                    _usersRepository.Edit(user);
                    _usersRepository.SaveAll();

                        return Ok(new { message = "Address Successfully Updated."});
                }

                return BadRequest("Address change was unsuccessful.");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        [HttpPost]
        [Route("Area")]
        public async Task<IActionResult> ChangeArea([FromBody] ProfileEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.AreaID != 0)
                {
                    string userId = User.GetUserIdToken();
                    var user = await _userManager.FindByIdAsync(userId);
                    user.AreaID = model.AreaID;
                    _usersRepository.Edit(user);
                    _usersRepository.SaveAll();

                        return Ok(new { message = "Area Successfully Updated."});
                }

                return BadRequest("Area change was unsuccessful.");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpGet]
        [Route("UserDetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            try
            {
                string userId = User.GetUserIdToken();
                var user = await _userManager.FindByIdAsync(userId);
                string cityName = _usersRepository.GetUserInformation(userId).Area.City.CityName;
                string areaName = _usersRepository.GetUserInformation(userId).Area.AreaName;
                var userModel = new UserDetailsViewModel()
                {
                    ID = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    Email = user.Email,
                    UserName = user.UserName,
                    Phonenumber = user.PhoneNumber,
                    CityName = cityName,
                    AreaName = areaName
                    
                };
                
                return Ok(userModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}