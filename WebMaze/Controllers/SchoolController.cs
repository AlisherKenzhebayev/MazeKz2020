using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using WebMaze.DbStuff.Repository;
using WebMaze.DbStuff.Repository.School;
using WebMaze.Models.Account;
using WebMaze.Models.School;

namespace WebMaze.Controllers
{
    [Authorize(AuthenticationSchemes = Startup.SchoolAuth)]
    public class SchoolController : Controller
    {
        private readonly IMapper mapper;
        private readonly CitizenUserRepository citizenUserRepository;
        private readonly SchoolBuildingRepository schoolBuildingRepository;
        private readonly SchoolCertificateRepository schoolCertificateRepository;
        private readonly SchoolScheduleRepository schoolScheduleRepository;
        private readonly SchoolStaffRepository schoolStaffRepository;
        private readonly SchoolStudentRepository schoolStudentRepository;
        private readonly SchoolSubjectRepository schoolSubjectRepository;

        public SchoolController(IMapper mapper, 
            CitizenUserRepository citizen,
            SchoolBuildingRepository schoolBuilding,
            SchoolCertificateRepository schoolCertificate,
            SchoolScheduleRepository schoolSchedule,
            SchoolStaffRepository schoolStaff,
            SchoolStudentRepository schoolStudent,
            SchoolSubjectRepository schoolSubject) 
        {
            this.mapper = mapper;
            citizenUserRepository = citizen;
            schoolBuildingRepository = schoolBuilding;
            schoolCertificateRepository = schoolCertificate;
            schoolScheduleRepository = schoolSchedule;
            schoolStaffRepository = schoolStaff;
            schoolStudentRepository = schoolStudent;
            schoolSubjectRepository = schoolSubject;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(Startup.SchoolAuth);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult MyProfile(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            return View("MyProfile", new LoginViewModel { ReturnUrl = returnUrl });
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            { 
                return RedirectToAction("Index"); 
            }

            return View("Login", new LoginViewModel { ReturnUrl = returnUrl });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginDetails)
        {
            // Check model first, then request
            if (!ModelState.IsValid)
            {
                return View(loginDetails);
            }

            var userDetails = citizenUserRepository.GetUserByLogin(loginDetails.Login);
            if (userDetails == null)
            {
                loginDetails.Login = string.Empty;
                ModelState.AddModelError("Login", "Данный логин не существует");
            }
            else if (userDetails.Password != loginDetails.Password)
            {
                ModelState.AddModelError("Password", "Неправильный пароль");
            }

            if (!ModelState.IsValid)
            {
                return View(loginDetails);
            }

            await AuthorizeUser(userDetails.Id, userDetails.Login);

            if (string.IsNullOrEmpty(loginDetails.ReturnUrl))
            {
                return RedirectToAction("Index");
            }

            return Redirect(loginDetails.ReturnUrl);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Registration(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            return View("Registration", new LoginViewModel { ReturnUrl = returnUrl });
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("Index");
        }

        private async Task AuthorizeUser(long userId, string login)
        {
            var claims = new List<Claim>()
            {
                new Claim("Id", userId.ToString()),
                new Claim(ClaimTypes.AuthenticationMethod, Startup.SchoolAuth),
                new Claim(ClaimTypes.Name, login)
            };

            var id = new ClaimsIdentity(claims, Startup.SchoolAuth);
            await HttpContext.SignInAsync(Startup.SchoolAuth, new ClaimsPrincipal(id));
        }
    }
}
