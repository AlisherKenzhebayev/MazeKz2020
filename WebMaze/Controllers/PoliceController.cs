﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMaze.DbStuff.Model.Police;
using WebMaze.DbStuff.Repository;
using WebMaze.Models.Account;
using WebMaze.Models.Police;
using WebMaze.Models.Police.Violation;

namespace WebMaze.Controllers
{
    [Authorize(AuthenticationSchemes = Startup.PoliceAuthMethod)]
    public class PoliceController : Controller
    {
        private readonly IMapper mapper;
        private readonly PolicemanRepository pmRepo;
        private readonly CitizenUserRepository cuRepo;

        public PoliceController(IMapper mapper,
            PolicemanRepository pmRepo,
            CitizenUserRepository cuRepo)
        {
            this.mapper = mapper;
            this.pmRepo = pmRepo;
            this.cuRepo = cuRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(Startup.PoliceAuthMethod);
            return RedirectToAction("Index");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Account()
        {
            var userItem = cuRepo.GetUserByLogin(User.Identity.Name);
            if (userItem == null)
            {
                throw new NotImplementedException();
            }

            if (!pmRepo.IsUserPoliceman(userItem, out Policeman policeItem))
            {
                userItem.AvatarUrl = ChangeNullPhotoToDefault(userItem.AvatarUrl);
                return View(new PolicemanViewModel { ProfileVM = mapper.Map<ProfileViewModel>(userItem) });
            }

            var result = mapper.Map<PolicemanViewModel>(policeItem);
            result.ProfileVM.AvatarUrl = ChangeNullPhotoToDefault(result.ProfileVM.AvatarUrl);

            return View(result);
        }

        [Route("[controller]/[action]/{id?}")]
        public IActionResult Criminal(int? id)
        {
            if(!pmRepo.IsUserPoliceman(cuRepo.GetUserByLogin(User.Identity.Name), out _))
            {
                return RedirectToAction("Account");
            }

            if (id == null)
            {
                return View();
            }

            return RedirectToAction("Account");
        }

        public IActionResult AddViolation()
        {
            var user = cuRepo.GetUserByLogin(User.Identity.Name);
            var result = new ViolationRegistrationViewModel()
            {
                UserName = $"{user.FirstName} {user.LastName}",
                UserLogin = user.Login,
                Date = DateTime.Today
            };

            return View(result);
        }

        // Anonymous methods ------------------------------------------------

        [AllowAnonymous]
        public IActionResult Index()
        {
            var card = new CardViewModel
            {
                SubTitle = "Внутри системы",
                Title = "Получить сертификат полицейского",
                Description = "У вас есть страсть бороться с преступностью? Тогда вам к нам."
                    + "Отправьте заявку на получение сертификата не выходя из дома!",
                Link = "#",
                LinkText = "Получить сейчас"
            };

            var result = new MainIndexInfoViewModel()
            {
                Cards = new List<CardViewModel> { card }
            };

            return View(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            var userItem = cuRepo.GetUserByLogin(user.Login);
            if (userItem == null)
            {
                user.Login = string.Empty;
                ModelState.AddModelError("Login", "Данный логин не существует");
            }
            else if (userItem.Password != user.Password)
            {
                ModelState.AddModelError("Password", "Неправильный пароль");
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            await AuthorizeUser(userItem.Id, userItem.Login);

            if (string.IsNullOrEmpty(user.ReturnUrl))
            {
                return RedirectToAction("Index");
            }

            return Redirect(user.ReturnUrl);
        }


        // Private methods ----------------------------------------------------

        private async Task AuthorizeUser(long userId, string login)
        {
            var claims = new List<Claim>()
            {
                new Claim("Id", userId.ToString()),
                new Claim(ClaimTypes.AuthenticationMethod, Startup.PoliceAuthMethod),
                new Claim(ClaimTypes.Name, login)
            };

            var id = new ClaimsIdentity(claims, Startup.PoliceAuthMethod);
            await HttpContext.SignInAsync(Startup.PoliceAuthMethod, new ClaimsPrincipal(id));
        }
        private static string ChangeNullPhotoToDefault(string urlPathString)
        {
            const string defaultUserLogoPath = "/image/Police/police_default_user_logo.png";
            if (string.IsNullOrEmpty(urlPathString))
            {
                urlPathString = defaultUserLogoPath;
            }

            return urlPathString;
        }
    }
}
