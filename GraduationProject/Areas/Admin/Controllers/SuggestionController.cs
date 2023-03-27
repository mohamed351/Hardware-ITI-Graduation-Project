using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationProject.Repositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class SuggestionController : Controller
    {
        private readonly ISuggestionRepository suggestionRepository;

        public SuggestionController(ISuggestionRepository suggestionRepository)
        {
            this.suggestionRepository = suggestionRepository;
        }
        public IActionResult Index()
        {
            var suggestion = suggestionRepository.GetAll();
            return View(suggestion);
        }
    }
}
