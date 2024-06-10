using AspNetCoreHero.ToastNotification.Abstractions;
using Demographic_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demographic_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TemporaryStayController : Controller
    {
        private readonly DemoGraphicContext _context;
        public INotyfService _notyfService { get; }
        public readonly IWebHostEnvironment _environment;

        public TemporaryStayController(DemoGraphicContext demographicContext, INotyfService notyfService, IWebHostEnvironment environment)
        {
            _context = demographicContext;
            _notyfService = notyfService;
            _environment = environment;
        }
        [Route("/dang-ky-tam-tru/{id}")]
        public IActionResult Index(int id)
        {
            
            //tìm 1 population theo id
            var findPopulationName=_context.Populations.FirstOrDefault(f=>f.PopulationId==id);
            //gán dữ liệu qua view
            ViewData["findPopulationId"] = findPopulationName.PopulationId;
            ViewData["findPopulationName"] = findPopulationName.PopulationName.ToString();
            ViewData["findPopulationDob"] = findPopulationName.DateOfBirth;
            ViewData["findPopulationCCCD"] = findPopulationName.CitizenIdentificationCard.ToString();
            ViewData["findPopulationAddress"] = findPopulationName.BirthPlace.ToString();
            ViewData["findPopulationGender"] = findPopulationName.Gender;

            //khởi tạo lưu trữ cho giấy vừa đăng ký và giấy hết hạn
            var lstTempStayFalse = new List<TemporarilyStaying>();
            var lstTempStayTrue = new List<TemporarilyStaying>();

            var findTempStay = _context.TemporarilyStayings.Where(s => s.PopulationId == id).ToList();
            //duyệt qua và thêm những bản quá hạn vào danh sách đã khởi tạo
            foreach (var item in findTempStay)
            {
                if (item.IsNewStay == false)
                {
                    lstTempStayFalse.Add(item);
                }
                lstTempStayTrue.Add(item);
            }

            //truyền dữ liệu hiển thị
            ViewData["lstTempStayFalse"] = lstTempStayFalse;
            ViewData["lstTempStayTrue"] = lstTempStayTrue;

            return View();
        }
        
        [Route("/dang-ky-tam-tru/{id}")]
        [HttpPost]
        public IActionResult Index(int id,TemporarilyStaying temporarilyStaying)
        {
            try
            {
                var findPopulation =_context.Populations.FirstOrDefault(f=>f.PopulationId==id);
                //tìm kiếm những giấy đã quá hạn sử dụng
                var lstTempStay = _context.TemporarilyStayings.Where(s=>s.PopulationId==id).ToList();
                foreach (var item in lstTempStay)
                {
                    if (item.IsNewStay == true)
                    {
                        item.IsNewStay = false;
                        _context.Update(item);
                        _context.SaveChanges();
                    }
                }

                string url = "/chi-tiet-so-ho-khau/" + findPopulation.ResidenceBookletId;

                temporarilyStaying.DateOfBirth = findPopulation.DateOfBirth;
                temporarilyStaying.IsNewStay = true;
                _context.Add(temporarilyStaying);
                _context.SaveChanges();
                _notyfService.Success("Bạn vừa đăng ký tạm trú");

                

                return Redirect(url);
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        public IActionResult DangKyTamTru()
        {
            return View();
        }
    }
}
