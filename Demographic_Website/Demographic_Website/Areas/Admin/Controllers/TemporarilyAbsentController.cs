using AspNetCoreHero.ToastNotification.Abstractions;
using Demographic_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demographic_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TemporarilyAbsentController : Controller
    {
        private readonly DemoGraphicContext _context;
        public INotyfService _notyfService { get; }
        public readonly IWebHostEnvironment _environment;

        public TemporarilyAbsentController(DemoGraphicContext demographicContext, INotyfService notyfService, IWebHostEnvironment environment)
        {
            _context = demographicContext;
            _notyfService = notyfService;
            _environment = environment;
        }
        [Route("/dang-ky-tam-vang/{id}")]
        public IActionResult Index(int id)
        {
            //tìm 1 population theo id
            var findPopulationName = _context.Populations.FirstOrDefault(f => f.PopulationId == id);
            if (findPopulationName != null)
            {
                //gán dữ liệu qua view
                ViewData["findPopulationId"] = findPopulationName.PopulationId;
                ViewData["findPopulationName"] = findPopulationName.PopulationName;
                ViewData["findPopulationDob"] = findPopulationName.DateOfBirth;
                ViewData["findPopulationCCCD"] = findPopulationName.CitizenIdentificationCard;
                ViewData["findPopulationAddress"] = findPopulationName.BirthPlace;
                ViewData["findPopulationGender"] = findPopulationName.Gender;
            }
            //khởi tạo lưu trữ cho giấy vừa đăng ký và giấy hết hạn
            var lstTempAbsentFalse = new List<TemporarilyAbsent>();
            var lstTempAbsentTrue = new List<TemporarilyAbsent>();

            var findTempStay = _context.TemporarilyAbsents.Where(s => s.PopulationId == id).ToList();
            //duyệt qua và thêm những bản quá hạn vào danh sách đã khởi tạo
            foreach (var item in findTempStay)
            {
                if (item.IsNewAbsent == false)
                {
                    lstTempAbsentFalse.Add(item);
                }
                lstTempAbsentTrue.Add(item);
            }

            //truyền dữ liệu hiển thị
            if(lstTempAbsentTrue.Count > 0)
            {
                ViewData["lstTempAbsentTrue"] = lstTempAbsentTrue;
            }
            ViewData["lstTempAbsentFalse"] = lstTempAbsentFalse;
            

            return View();
        }
        [Route("/dang-ky-tam-vang/{id}")]
        [HttpPost]
        public IActionResult Index(int id, TemporarilyAbsent temporarilyAbsent)
        {
            try
            {
                var findPopulation = _context.Populations.FirstOrDefault(f => f.PopulationId == id);
                //tìm kiếm những giấy đã quá hạn sử dụng
                var lstTempAbsent = _context.TemporarilyAbsents.Where(s => s.PopulationId == id).ToList();
                foreach (var item in lstTempAbsent)
                {
                    if (item.IsNewAbsent == true)
                    {
                        item.IsNewAbsent = false;
                        _context.Update(item);
                        _context.SaveChanges();
                    }
                }
                string url="";
                if (findPopulation != null)
                {
                    url = "/chi-tiet-so-ho-khau/" + findPopulation.ResidenceBookletId;
                }

                temporarilyAbsent.DateOfBirth = findPopulation.DateOfBirth;
                temporarilyAbsent.IsNewAbsent = true;
                _context.Add(temporarilyAbsent);
                _context.SaveChanges();
                _notyfService.Success("Bạn vừa đăng ký tạm vắng");



                return Redirect(url);
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
    }
}
