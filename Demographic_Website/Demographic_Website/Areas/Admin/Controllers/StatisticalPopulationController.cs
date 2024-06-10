using AspNetCoreHero.ToastNotification.Abstractions;
using Demographic_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demographic_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticalPopulationController : Controller
    {
        private readonly DemoGraphicContext _context;
        public INotyfService _notyfService { get; }
        public readonly IWebHostEnvironment _environment;
        public StatisticalPopulationController(DemoGraphicContext demographicContext, INotyfService notyfService, IWebHostEnvironment environment)
        {
            _context = demographicContext;
            _notyfService = notyfService;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var result = _context.Populations.ToList();
            return View(result);
        }
        [HttpPost]
        public List<object> ChartGender()
        {
            List<object> data = new List<object>();

            List<string> labels = new List<string>();
            labels.Add("Nam");
            labels.Add("Nữ");

            var totalMale = _context.Populations.Count(p => p.Gender == true);
            var totalFemale = _context.Populations.Count(p => p.Gender == false);

            //data.Add(labels);
            data.Add(totalMale);
            data.Add(totalFemale);

            return data;
        }



        [HttpPost]
        public List<object> ChartAge()
        {
            List<object> data = new List<object>();

            List<object> lstMale = new List<object>();

            List<object> lstFeMale = new List<object>();

            var totalUnder18Male = _context.Populations.Count(p => p.Gender==true && (DateTime.Now.Year - p.DateOfBirth.Value.Year) < 18);
            lstMale.Add(totalUnder18Male);

            var totalBetween18And60Male = _context.Populations.Count(p => p.Gender == true && (DateTime.Now.Year - p.DateOfBirth.Value.Year) > 18 && (DateTime.Now.Year - p.DateOfBirth.Value.Year) < 60);
            lstMale.Add(totalBetween18And60Male);

            var totalOver60Male = _context.Populations.Count(p => p.Gender == true && (DateTime.Now.Year - p.DateOfBirth.Value.Year) > 60);
            lstMale.Add(totalOver60Male);

            var totalUnder18FeMale = _context.Populations.Count(p => p.Gender == false && (DateTime.Now.Year - p.DateOfBirth.Value.Year) < 18);
            lstFeMale.Add(totalUnder18FeMale);

            var totalBetween18And60FeMale = _context.Populations.Count(p => p.Gender == false && (DateTime.Now.Year - p.DateOfBirth.Value.Year) > 18 && (DateTime.Now.Year - p.DateOfBirth.Value.Year) < 60);
            lstFeMale.Add(totalBetween18And60FeMale);

            var totalOver60FeMale = _context.Populations.Count(p => p.Gender == false && (DateTime.Now.Year - p.DateOfBirth.Value.Year) > 60);
            lstFeMale.Add(totalOver60FeMale);
           
            data.Add(lstMale);
            data.Add(lstFeMale);

            return data;
        }

        [HttpPost]
        public List<object> ChartEthnicity()
        {
            List<object> data = new List<object>();

            List<object> EthnicityMale = new List<object>();
            List<object> EthnicityFeMale = new List<object>();

            string[] danTocVietNam = { "Kinh", "Tày", "Thái", "Mường", "Hoa", "Hmông", "Khơ Mú" };

            int countKinhMale = _context.Populations.Count(p => p.Ethnicity == danTocVietNam[0] && p.Gender==true);
            EthnicityMale.Add(countKinhMale);
            int countKinhFeMale = _context.Populations.Count(p => p.Ethnicity == danTocVietNam[0] && p.Gender == false);
            EthnicityFeMale.Add(countKinhFeMale);

            int countTayMale = _context.Populations.Count(p => p.Ethnicity == danTocVietNam[1] && p.Gender == true);
            EthnicityMale.Add(countTayMale);
            int countTayFeMale = _context.Populations.Count(p => p.Ethnicity == danTocVietNam[1] && p.Gender == false);
            EthnicityFeMale.Add(countTayFeMale);

            int countThaiMale = _context.Populations.Count(p => p.Ethnicity == danTocVietNam[2] && p.Gender == true);
            EthnicityMale.Add(countThaiMale);
            int countThaiFeMale = _context.Populations.Count(p => p.Ethnicity == danTocVietNam[2] && p.Gender == false);
            EthnicityFeMale.Add(countThaiFeMale);

            int countMuongMale = _context.Populations.Count(p => p.Ethnicity == danTocVietNam[3] && p.Gender == true);
            EthnicityMale.Add(countMuongMale);
            int countMuongFeMale = _context.Populations.Count(p => p.Ethnicity == danTocVietNam[3] && p.Gender == false);
            EthnicityFeMale.Add(countMuongFeMale);

            int countHoaMale = _context.Populations.Count(p => p.Ethnicity == danTocVietNam[4] && p.Gender==true);
            EthnicityMale.Add(countHoaMale);
            int countHoaFeMale = _context.Populations.Count(p => p.Ethnicity == danTocVietNam[4] && p.Gender == false);
            EthnicityFeMale.Add(countHoaFeMale);

            int countKhoMuMale = _context.Populations.Count(p => p.Ethnicity == danTocVietNam[5] && p.Gender == true);
            EthnicityMale.Add(countKhoMuMale);
            int countKhoMuFeMale = _context.Populations.Count(p => p.Ethnicity == danTocVietNam[5] && p.Gender == false);
            EthnicityFeMale.Add(countKhoMuFeMale);

            int countHmongMale = _context.Populations.Count(p => p.Ethnicity == danTocVietNam[6] && p.Gender == true);
            EthnicityMale.Add(countHmongMale);
            int countHmongFeMale = _context.Populations.Count(p => p.Ethnicity == danTocVietNam[6] && p.Gender == false);
            EthnicityFeMale.Add(countHmongFeMale);

            int countOtherMale = _context.Populations.Count(p => !danTocVietNam.Contains(p.Ethnicity) && p.Gender==true);
            EthnicityMale.Add(countOtherMale);
            int countOtherFeMale = _context.Populations.Count(p => !danTocVietNam.Contains(p.Ethnicity) && p.Gender == false);
            EthnicityFeMale.Add(countOtherFeMale);

            data.Add(EthnicityMale);
            data.Add(EthnicityFeMale);

            return data;
        }

        [HttpPost]
        public List<object> ChartBirthRateDeathRate()
        {
            List<object> data = new List<object>();

            var BirthRateYear = _context.Populations.Where(p => p.DateOfBirth.Value.Year >= 2010 && p.DateOfBirth.Value.Year <= DateTime.Now.Year) // tìm những người có năm sinh từ 2010 đến hiện tại
                .GroupBy(p => p.DateOfBirth.Value.Year) //nhóm theo năm
                .Select(p => new
                {
                    Year = p.Key,
                    BirthRate = (double)p.Count() / (_context.Populations.ToList().Count()) * 100
                });//lấy ra năm và tính tỷ lệ sinh

            var DeathRateYear = _context.Populations.Where(p => p.DateOfDeath.Value.Year >= 2010 && p.DateOfDeath.Value.Year <= DateTime.Now.Year && p.Alive==false) // tìm những người có năm sinh từ 2010 đến hiện tại
                .GroupBy(p => p.DateOfDeath.Value.Year) //nhóm theo năm
                .Select(p => new
                {
                    Year = p.Key,
                    DeathRate = (double)p.Count() / (_context.Populations.ToList().Count()) * 100
                });//lấy ra năm và tính tỷ tử


            List<string> listYear = new List<string>();
            List<double> resultBirthRate = new List<double>();

            foreach (var item in BirthRateYear)
            {
                listYear.Add(item.Year.ToString());
                resultBirthRate.Add(item.BirthRate);
            }

            List<double> resultDeathRate = new List<double>();

            foreach (var item in DeathRateYear)
            {
                listYear.Add(item.Year.ToString());
                resultDeathRate.Add(item.DeathRate);
            }

            data.Add(listYear);
            data.Add(resultBirthRate);
            data.Add(resultDeathRate);

            return data;
        }

    }
}
