using Demographic_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demographic_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class APITinhThanhController : Controller
    {
        private readonly DemoGraphicContext _context;

        public APITinhThanhController(DemoGraphicContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult GetProvince()
        {
            var result = _context.Provinces.ToList();
            return Json(result);
        }
        [HttpPost]
        public IActionResult GetDistricts(string id)
        {
            string[] IdSplit=id.Split(',');
            string ResultId=IdSplit[0];
            var result = _context.Districts.Where(d => d.ProvinceId == Convert.ToInt32(ResultId)).ToList();
            return Json(result);
        }
        [HttpPost]
        public IActionResult GetWards(string IdWard)
        {
            string[] IdWardSplit = IdWard.Split(",");
            string ResultId = IdWardSplit[0];
            var result = _context.Wards.Where(d => d.DistrictId == Convert.ToInt32(ResultId)).ToList();
            return Json(result);
        }
    }
}
