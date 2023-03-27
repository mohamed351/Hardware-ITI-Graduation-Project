using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationProject.Models;
using GraduationProject.Repositry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GraduationProject.ExtenstionMethods;
using GraduationProject.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace GraduationProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        public IUsersRepository _usersRepository { get; }
        public UserManager<ApplicationUser> _userManager { get; }
        public IAreaRepositry _areas { get; }

        public UsersController(IUsersRepository users, UserManager<ApplicationUser> userManager, IAreaRepositry areas)
        {
            _usersRepository = users;
            _userManager = userManager;
            _areas = areas;
        }
        // GET: Users
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetUsers(int start = 0, int length = 10)
        {
            string search = HttpContext.Request.Query["search[value]"];
            return Json(_usersRepository.GetDataTable(start, length, a => a.UserName.Contains(search), a => a.UserName));
        }

        [HttpGet]
        public IActionResult Create()
        {
            UserViewModel model = new UserViewModel()
            {
                Areas = new SelectList(_areas.GetAll(), "ID", "AreaName")
            };
            return PartialView("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel Usermodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = new ApplicationUser()
                    {
                        FirstName = Usermodel.FirstName,
                        LastName = Usermodel.LastName,
                        Email = Usermodel.Email,
                        Address = Usermodel.Address,
                        UserName = Usermodel.UserName,
                        PhoneNumber = Usermodel.Phonenumber,
                        AreaID = Usermodel.AreaID
                    };

                    var result = await _userManager.CreateAsync(user, Usermodel.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                        return Ok("User Successfully Added.");
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return BadRequest("An error occurred while adding a new user, Please recheck the entered data.");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            UserViewModel userModel = new UserViewModel()
            {
                ID = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address,
                UserName = user.UserName,
                Phonenumber = user.PhoneNumber,
                Areas = new SelectList(_areas.GetAll(), "ID", "AreaName"),
                AreaID = user.AreaID
            };

            return PartialView("Edit", userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = await _userManager.FindByIdAsync(userModel.ID);
                    
                    await _userManager.RemovePasswordAsync(user);
                    var result1 = await _userManager.AddPasswordAsync(user, userModel.Password);
                    if (!result1.Succeeded)
                        return BadRequest("An Error Occurred while changing the Password.");

                    var result2 = await _userManager.SetEmailAsync(user, userModel.Email);
                    if (!result2.Succeeded)
                        return BadRequest("An Error Occurred while changing the Email.");

                    var result3 = await _userManager.SetPhoneNumberAsync(user, userModel.Phonenumber);
                    if (!result3.Succeeded)
                        return BadRequest("An Error Occurred while changing the Phone Number.");

                    var result4 = await _userManager.SetUserNameAsync(user, userModel.UserName);
                    if (!result4.Succeeded)
                        return BadRequest("An Error Occurred while changing the Username.");

                    user.FirstName = userModel.FirstName;
                    user.LastName = userModel.LastName;
                    user.Address = userModel.Address;
                    user.AreaID = userModel.AreaID;

                    _usersRepository.Edit(user);
                    _usersRepository.SaveAll();

                    return Ok("User Successfully Edited.");
                }

                return BadRequest("An error occurred while editing the selected user, Please recheck the entered data.");

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound("User Not Found.");
                }

                await _userManager.DeleteAsync(user);
                return Ok($"{user.UserName} has been Deleted");
            }
            catch
            {
                return BadRequest("An Error Has Occurred.");
            }
        }
    }
}