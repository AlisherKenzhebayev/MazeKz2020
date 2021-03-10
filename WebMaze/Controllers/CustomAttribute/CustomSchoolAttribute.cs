using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model.School.Enums;
using WebMaze.DbStuff.Repository;
using WebMaze.DbStuff.Repository.School;
using WebMaze.Services;

namespace WebMaze.Controllers.CustomAttribute
{
    public class CustomSchoolAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var citizenUserRepository = context.HttpContext.RequestServices.GetService(typeof(CitizenUserRepository)) as CitizenUserRepository;
            var schoolStaffRepository = context.HttpContext.RequestServices.GetService(typeof(SchoolStaffRepository)) as SchoolStaffRepository;
            var uName = context.HttpContext.User.Identity.Name;
            var user = citizenUserRepository?.GetUserByLogin(uName);
            var staff = schoolStaffRepository.GetByCitizenId(user.Id);
            if (staff == default)
            {
                context.Result = new ForbidResult();
            }

            if (staff.StaffMember != StaffMember.Dean &&
                staff.StaffMember != StaffMember.ViceDean &&
                staff.StaffMember != StaffMember.President &&
                staff.StaffMember != StaffMember.TeachingStaff)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
