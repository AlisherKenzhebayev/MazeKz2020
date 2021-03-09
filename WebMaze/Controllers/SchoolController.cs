using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model;
using WebMaze.DbStuff.Model.School;
using WebMaze.DbStuff.Repository;
using WebMaze.DbStuff.Repository.School;
using WebMaze.Models.Account;
using WebMaze.Models.School;
using WebMaze.Models.Users;

namespace WebMaze.Controllers
{
    [Authorize(AuthenticationSchemes = Startup.SchoolAuth)]
    public class SchoolController : Controller
    {
        private readonly IMapper mapper;
        private readonly CitizenUserRepository _citizenUserRepository;
        private readonly SchoolBuildingRepository _schoolBuildingRepository;
        private readonly SchoolCertificateRepository _schoolCertificateRepository;
        private readonly SchoolScheduleRepository _schoolScheduleRepository;
        private readonly SchoolStaffRepository _schoolStaffRepository;
        private readonly SchoolStudentRepository _schoolStudentRepository;
        private readonly SchoolSubjectRepository _schoolSubjectRepository;

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
            _citizenUserRepository = citizen;
            _schoolBuildingRepository = schoolBuilding;
            _schoolCertificateRepository = schoolCertificate;
            _schoolScheduleRepository = schoolSchedule;
            _schoolStaffRepository = schoolStaff;
            _schoolStudentRepository = schoolStudent;
            _schoolSubjectRepository = schoolSubject;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(Startup.SchoolAuth);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Redirects to one of ProfileViews, depending on who the person is, staff/student or guest
        /// </summary>
        [HttpGet]
        public IActionResult MyProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                return GetProfile(User.Identity.Name);
            }

            return View("GuestProfile", mapper.Map<ProfileViewModel>(_citizenUserRepository.GetUserByLogin(User.Identity.Name)));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("MyProfile/{loginString}")]
        public IActionResult MyProfile(string loginString)
        {
            return GetProfile(loginString);
        }

        #region Login
        
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

            var userExists = _citizenUserRepository.UserExists(loginDetails.Login);
            if (userExists == false)
            {
                loginDetails.Login = string.Empty;
                ModelState.AddModelError("Login", "Данный логин не существует");
            }
            
            var userDetails = _citizenUserRepository.GetUserByLogin(loginDetails.Login);
            if (userDetails == null || userDetails.Password != loginDetails.Password)
            {
                ModelState.AddModelError("Password", "Неправильный пароль");
            }

            if (!ModelState.IsValid || ModelState.ErrorCount > 0)
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
        
        #endregion


        #region SignUp

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Registration()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            return View("Registration");
        }
        
        /// <summary>
        /// Only registers new Citizen User. For more school related actions, need to pass custom authFilter
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Registration(RegistrationViewModel registrationDetails)
        {
            // Check model first, then request
            if (!ModelState.IsValid)
            {
                return View(registrationDetails);
            }

            var userExists = _citizenUserRepository.UserExists(registrationDetails.Login);
            if (userExists == true)
            {
                ModelState.AddModelError("Login", "Данный логин уже существует");
            }

            if (!ModelState.IsValid || ModelState.ErrorCount > 0)
            {
                return View(registrationDetails);
            }

            _citizenUserRepository.Save(mapper.Map<CitizenUser>(registrationDetails));

            return Redirect("Login");
        }
        
        #endregion

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("Index");
        }

        #region private
        
        private async Task AuthorizeUser(long userId, string login)
        {
            var claims = new List<Claim>()
            {
                new Claim("Id", userId.ToString()),
                new Claim(ClaimTypes.AuthenticationMethod, Startup.SchoolAuth),
                new Claim(ClaimTypes.Name, login)
            };

            var user = _citizenUserRepository.GetUserByLogin(login);
            foreach (var r in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, r.Name));
            }

            var id = new ClaimsIdentity(claims, Startup.SchoolAuth);
            await HttpContext.SignInAsync(Startup.SchoolAuth, new ClaimsPrincipal(id));
        }

        private IActionResult GetProfile(string loginString)
        {
            var user = _citizenUserRepository.GetUserByLogin(loginString);

            if (user == default)
            {
                return View("NotFound");
            }

            var staff = _schoolStaffRepository.GetByCitizenId(user.Id);
            var student = _schoolStudentRepository.GetByCitizenId(user.Id);
            if (staff != default)
            {
                var viewModel = mapper.Map<SchoolProfileViewModel>(staff);
                return View("MyProfile", viewModel);
            }

            if (student != default){
                var viewModel = mapper.Map<SchoolProfileViewModel>(student);
                return View("MyProfile", viewModel);
            }

            return View("GuestProfile", mapper.Map<ProfileViewModel>(_citizenUserRepository.GetUserByLogin(User.Identity.Name)));
        }
        
        #endregion

    }
}
