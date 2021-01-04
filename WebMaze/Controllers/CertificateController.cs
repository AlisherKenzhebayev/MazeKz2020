﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMaze.DbStuff.Model.Police;
using WebMaze.DbStuff.Repository;
using WebMaze.Models.Certificate;

namespace WebMaze.Controllers
{
    [Authorize]
    public class CertificateController : Controller
    {
        private readonly IMapper mapper;
        private readonly CertificateRepository certRepos;

        public CertificateController(CertificateRepository certRepos, IMapper mapper)
        {
            this.certRepos = certRepos;
            this.mapper = mapper;
        }

        public IActionResult Index(CertificateViewModel certificate)
        {
            if (string.IsNullOrEmpty(certificate.Speciality))
            {
                return NotFound();
            }

            return View(certificate);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult MakeCertificate(CertificateViewModel certificate)
        {
            var certItem = new Certificate
            {
                Speciality = certificate.Speciality,
                DateOfIssue = certificate.Starts,
                Validity = certificate.Expires
            };

            certRepos.MakeCertificate(certItem, User.Identity.Name);

            return RedirectToAction(certificate.RedirectToAction, certificate.RedirectToController);
        }

        public IActionResult Items()
        {
            var items = mapper.Map<CertificateItemViewModel[]>(certRepos.GetAllValidCertificates(User.Identity.Name));
            return View(items);
        }

        public IActionResult History(string speciality)
        {
            CertificateItemViewModel[] items;
            if (!string.IsNullOrEmpty(speciality))
            {
                items = mapper.Map<CertificateItemViewModel[]>(certRepos.GetCertificates(User.Identity.Name, speciality));
            }
            else
            {
                items = mapper.Map<CertificateItemViewModel[]>(certRepos.GetCertificates(User.Identity.Name));
            }

            return View("Items", items);
        }
    }
}
