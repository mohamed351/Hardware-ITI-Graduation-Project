using GraduationProject.Areas.Api.VIewModels;
using GraduationProject.Models;
using GraduationProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GraduationProject.Areas.Api.ViewModels;
using GraduationProject.ExtenstionMethods;
using GraduationProject.Repositry;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace GraduationProject.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configration;
        private readonly IEmailSender _emailSender;
        private readonly IOptions<AuthMessageSenderOptions> _AuthMessageSenderOptions;
        private readonly IUsersRepository usersRepository;
        private readonly IMapper mapper;
        private readonly IReviewRepository _reviewRepository;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, IConfiguration configration,
            IUsersRepository usersRepository,
            IMapper mapper,
            IReviewRepository reviewRepository, IEmailSender emailSender, IOptions<AuthMessageSenderOptions> AuthMessageSenderOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configration = configration;
            _emailSender = emailSender;
            _AuthMessageSenderOptions = AuthMessageSenderOptions;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
            this._reviewRepository = reviewRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Name = "Account" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Find login user by userName
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    //if user found Check the password 
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        //create token
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Id),
                            new Claim(JwtRegisteredClaimNames.Acr, user.Id),
                             new Claim(JwtRegisteredClaimNames.Actort, user.Id)
                        };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(null, null,
                            claims, expires: DateTime.Now.AddDays(60),
                            signingCredentials: credentials);

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            userId = user.Id,
                            userName = user.UserName
                        });
                    }
                }
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationViewModel model)
        {
            //If the user-entered data is validated, Create the user in the database.
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new ApplicationUser()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Address = model.Address,
                        UserName = model.UserName,
                        PhoneNumber = model.Phonenumber,
                        AreaID = model.AreaID

                    };

                    var result = await _userManager.CreateAsync(user, model.Password);
                    //If the user is successfully created, asign them to the role of "User"
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                        return Ok(result);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return BadRequest("Unable to register with the entered credentials.");
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);
                    // email is sent
                    string subject = "Welcome to App-Name! Confirm Your Email";
                    string message = "<table border = '0' cellpadding = '0' cellspacing = '0' style = 'margin: 0' width = '100%' >" +
                                     "<tr>" +
                                     "<td height = '30' ></td>" +
                                     "</tr>" +
                                     "<tr>" +
                                     "<td align = 'center' valign = 'top' >" +
                                     "<table width = '640' cellspacing = '0' cellpadding = '0' bgcolor = '#ffffff' class='100p' style='border-radius: 8px; border: 1px solid #E2E5E7; overflow:hidden;'>" +
                                     "<tr>" +
                                     "<td height = '20' ></td>" +
                                     "</tr>" +
                                     "<tr>" +
                                     "<td width='640' valign='top' class='100p'>" +
                                     "<!-- Header -->" +
                                     "<table border = '0' cellspacing='0' cellpadding='0' width='640' class='100p'>" +
                                     "<tr>" +
                                     "<!-- Logo -->" +
                                     "<td align = 'left' width='50%' class='100padtopbottom' style='padding-left: 20px'>" +
                                     "<img alt = 'Logo' src='https://s3-us-west-2.amazonaws.com/descript-public/email/logo%402x.jpg' width='112' style='width: 100%; max-width: 112px; font-family: Arial, sans-serif; color: #ffffff; font-size: 20px; display: block; border: 0px;' border='0'>" +
                                     "</td>" +
                                     "</tr>" +
                                     "<tr>" +
                                     "<td colspan = '2' width='640' height='160' class='100p'>" +
                                     "<img alt = 'Logo' src='https://s3-us-west-2.amazonaws.com/descript-public/email/bg-pwd%402x.jpg' width='640' style='width: 100%; max-width: 640px; font-family: Arial, sans-serif; color: #ffffff; font-size: 20px; display: block; border: 0px; margin-top:0px;' border='0'>" +
                                     "</td>" +
                                     "</tr>" +
                                     "<tr>" +
                                     "<td colspan = '2' align='left' valign='center' width='640' height='40' class='100p center' style='font-family: Arial, sans-serif; font-weight: bold; font-size:14px;padding: 0px 20px;'>" +
                                     "<font face = 'Arial, sans-serif' ></b> Hi " + user.FirstName! + "</b></font>" +
                                     "</td>" +
                                     "</tr>" +
                                     "<tr>" +
                                     "<td colspan = '2' align='left' valign='center' width='640' class='100p' style='font-family: Arial, sans-serif; font-size:14px; padding: 0px 20px; line-height: 18px;'>" +
                                     "<font face = 'Arial, sans-serif' >" +
                                     "Seems like you forgot your Descript password.To reset it, just click the button below.<br>" +
                                     "</br>" +
                                     "</br>" +
                                     "Thanks,<br>" +
                                     "Hardware Shed Team" +
                                     "</font>" +
                                     "</td>" +
                                     "</tr>" +
                                     "<tr>" +
                                     "<td colspan = '2' align= 'center' valign= 'center' width= '640' height= '20' class='100p center' style='font-family: Arial, sans-serif; font-weight: bold; font-size:1px;padding: 0px 20px;'>" +
                                     "</td>" +
                                     "</tr>" +
                                     "</table>" +
                                     "</td>" +
                                     "</tr>" +
                                     "<!-- Footer -->" +
                                     "<tr>" +
                                     "<td width = '640' class='100p center' height='80' align='center' valign='top'>" +
                                     "<table border = '0' cellspacing='0' cellpadding='0'>" +
                                     "<tr>" +
                                     "<td align = 'center' style='border-radius: 18px;' bgcolor='#0062FF'>" +
                                     "<a href = '" + passwordResetLink + "' style='font-size: 14px; font-family: sans-serif; color: #ffffff; text-decoration: none; border-radius: 18px; padding: 5px 16px; border: 1px solid #0062FF; display: inline-block; box-shadow: 0 2px 3px 0 rgba(0,0,0,0.10);'>" +
                                     "<!--[if mso]> <![endif]-->" +
                                     "Reset Password" +
                                     "<!--[if mso]> <![endif]-->" +
                                     "</a>" +
                                     "</td>" +
                                     "</tr>" +
                                     "</table>" +
                                     "</td>" +
                                     "</tr>" +
                                     "</table>" +
                                     "</td>" +
                                     "</tr>" +
                                     "</table>";


                    await _emailSender.SendEmailAsync(model.Email, subject, message);
                    //Logger.Log(LogLevel.Warning, passwordResetLink);
                    return Ok();
                }
                return BadRequest("Please ReEnter your Email");
            }
            return BadRequest("Invalid Email");
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            if (token == null && email == null)
            {
                ModelState.AddModelError("", "Invalid Password reset token");
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RresetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return BadRequest("Please check the entered data.");
                }
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("UserInfo")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<UserInformationViewModel> GetUserInformation()
        {


            string userId = User.GetUserIdToken();
            ApplicationUser applicationUser = usersRepository.GetUserInformation(userId);

            if (applicationUser == null)
            {
                return NotFound("Specified user does not exist.");
            }
            UserInformationViewModel model = mapper.Map<ApplicationUser, UserInformationViewModel>(applicationUser);
            model.Rating = usersRepository.GetAverageRating(userId);
            return Ok(model);

        }
        [HttpGet]
        [Route("UserReviews")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<UsersReviews> GetReviews()
        {
            string userId = User.GetUserIdToken();
            List<UsersReviews> usersReviews = this._reviewRepository.GetReviewsByUser(userId).ToList();
            if (usersReviews == null)
            {
                return NotFound("No Reviews Found.");

            }
            return Ok(usersReviews);

        }



    }
}
