using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebMaze.DbStuff.Repository;
using WebMaze.DbStuff.Repository.School;
using WebMaze.Models.School;

namespace WebMaze.Controllers
{
    [Authorize(AuthenticationSchemes = Startup.SchoolAuth)]
    [ApiController]
    public class SchoolApiController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly CitizenUserRepository _citizenUserRepository;
        private readonly SchoolBuildingRepository _schoolBuildingRepository;
        private readonly SchoolCertificateRepository _schoolCertificateRepository;
        private readonly SchoolScheduleRepository _schoolScheduleRepository;
        private readonly SchoolStaffRepository _schoolStaffRepository;
        private readonly SchoolStudentRepository _schoolStudentRepository;
        private readonly SchoolSubjectRepository _schoolSubjectRepository;

        public SchoolApiController(IMapper mapper,
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

        [HttpGet]
        public ActionResult<IEnumerable<SchoolSubjectViewModel>> AllSubjects()
        {
            var retEnum = mapper.Map<List<SchoolSubjectViewModel>>(_schoolSubjectRepository.GetAll());
            return retEnum;
        }
    }
}
