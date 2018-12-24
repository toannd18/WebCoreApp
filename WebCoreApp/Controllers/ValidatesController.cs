using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.Interfaces.Pipes;

namespace WebCoreApp.Controllers
{
    public class ValidatesController : Controller
    {
        private readonly IIsoJointRepository _isoJointRepository;
        private readonly IWelderRepository _welderRepository;

        public ValidatesController(IIsoJointRepository isoJointRepository, IWelderRepository welderRepository)
        {
            _isoJointRepository = isoJointRepository;
            _welderRepository = welderRepository;
        }
        [AcceptVerbs("Get","Post")]
        public  JsonResult ExistJoint(string joint, string isoName)
        {
            bool result = _isoJointRepository.ExistJoint(joint, isoName);

            return Json(!result);
        }
        [AcceptVerbs("Get", "Post")]
        public async Task<JsonResult> ExistWelder(string welder1, string welder2, string welder3, string welder4)
        {
            string welder=null;
            if (!string.IsNullOrEmpty(welder1))
            {
                welder = welder1;
            }
            else if (!string.IsNullOrEmpty(welder2))
            {
                welder = welder2;
            }
            else if (!string.IsNullOrEmpty(welder3))
            {
                welder = welder3;
            }
            else if (!string.IsNullOrEmpty(welder4))
            {
                welder = welder4;
            }
            if (string.IsNullOrEmpty(welder))
            {
                return Json(true);
            }
            var result = await _welderRepository.GetByMa(welder);
            if(result == null)
            {
                return Json($"Thợ hàn {welder} không tồn tại");
            }

            return Json(true);
        }

    }
}