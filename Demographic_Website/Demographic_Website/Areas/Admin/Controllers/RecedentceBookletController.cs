using AspNetCoreHero.ToastNotification.Abstractions;
using Demographic_Website.Helper;
using Demographic_Website.Models;
using Demographic_Website.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Net;
using X.PagedList;

namespace Demographic_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authentication]
    public class RecedentceBookletController : Controller
    {
        private readonly DemoGraphicContext _context;
        public INotyfService _notyfService { get; }
        public readonly IWebHostEnvironment _environment;
        public RecedentceBookletController(DemoGraphicContext demographicContext, INotyfService notyfService, IWebHostEnvironment environment)
        {
            _context = demographicContext;
            _notyfService = notyfService;
            _environment = environment;
        }

        //[Route("/danh-sach-so-ho-khau")]
        public IActionResult Index(int page = 1, string? MaHK = "", string CCCD = "", string? province = "", string? district = "", string? ward = "")
        {
            page = page < 1 ? 1 : page;
            int pagesize = 6;
            var query = _context.ResidenceBooklets.Join(_context.Populations, r => r.ResidenceBookletId, p => p.ResidenceBookletId, (r, p) => new BookLet()
            {
                ResidenceBooklet = r,
                Population = p
            }).Where(r => r.Population.Relationship == "Chủ hộ").OrderByDescending(p => p.ResidenceBooklet.CreateDate);

            //Tìm kiếm theo mã
            if (!String.IsNullOrEmpty(MaHK))
            {
                query = (IOrderedQueryable<BookLet>)query.Where(p => p.ResidenceBooklet.ResidenceBookletCode.ToLower().Contains(MaHK.ToLower()) || p.Population.CitizenIdentificationCard == MaHK);
            }
            else if (!String.IsNullOrEmpty(CCCD))
            {
                query = (IOrderedQueryable<BookLet>)query.Where(p => p.Population.CitizenIdentificationCard == CCCD);
            }

            //Tìm kiếm theo tỉnh thành
            if (!String.IsNullOrEmpty(province) && !String.IsNullOrEmpty(district) && !String.IsNullOrEmpty(ward))
            {
                string[] ProvinceSplit = province.Split(',');
                string Province = ProvinceSplit[1];

                int StartIndexDistricts;
                string districts = "";
                if (district.Contains("Huyện"))
                {
                    StartIndexDistricts = district.IndexOf("Huyện");
                    districts = district.Substring(StartIndexDistricts + 6);
                }
                else if (district.Contains("Quận"))
                {
                    StartIndexDistricts = district.IndexOf("Quận");
                    districts = district.Substring(StartIndexDistricts + 5);
                }
                else if (district.Contains("Thành phố"))
                {
                    StartIndexDistricts = district.IndexOf("Thành phố");
                    districts = district.Substring(StartIndexDistricts + 10);
                }


                int StartIndexWard;
                string wards = "";

                if (ward.Contains("Xã"))
                {
                    StartIndexWard = ward.IndexOf("Xã");
                    wards = ward.Substring(StartIndexWard + 3);
                }
                else if (ward.Contains("Phường"))
                {
                    StartIndexWard = ward.IndexOf("Phường");
                    wards = ward.Substring(StartIndexWard + 7);
                }
                else if (ward.Contains("Thị trấn"))
                {
                    StartIndexWard = ward.IndexOf("Thị trấn");
                    wards = ward.Substring(StartIndexWard + 9);
                }

                string DiaChi = wards + " - " + districts + " - " + Province;
                query = (IOrderedQueryable<BookLet>)query.
                    Where(p => p.ResidenceBooklet.Address.ToLower().Contains(Province) && p.ResidenceBooklet.Address.Contains(districts) && p.ResidenceBooklet.Address.Contains(wards));
            }
            //Tìm kiếm theo quận huyện
            //if (!String.IsNullOrEmpty(district))
            //{
            //    string[] districtSplit = district.Split(',');
            //    string districts = districtSplit[1];
            //    query = (IOrderedQueryable<BookLet>)query.Where(p => p.ResidenceBooklet.Address.ToLower().Contains(districts.ToLower()));
            //}
            ////Tìm kiếm theo phường xã
            //if (!String.IsNullOrEmpty(ward))
            //{
            //    string[] wardSplit = ward.Split(',');
            //    string wards = wardSplit[1];
            //    query = (IOrderedQueryable<BookLet>)query.Where(p => p.ResidenceBooklet.Address.ToLower().Contains(wards.ToLower()));
            //}

            ViewData["Query"] = query;
            return View(query.ToPagedList(page, pagesize));
        }
        public IActionResult GetData()
        {
            var query = _context.ResidenceBooklets.Join(_context.Populations, r => r.ResidenceBookletId, p => p.ResidenceBookletId, (r, p) => new BookLet()
            {
                ResidenceBooklet = r,
                Population = p
            }).Where(r => r.Population.Relationship == "Chủ hộ").OrderByDescending(p => p.ResidenceBooklet.CreateDate).ToList();
            return Json(new { success = true, data = query });
        }
        public IActionResult Create()
        {
            return View();
        }
        public bool CheckResidenceBookletCode(string code)
        {
            return _context.ResidenceBooklets.Any(r => r.ResidenceBookletCode == code);
        }
        public bool CheckCitizenIdentificationCard(string code)
        {
            return _context.Populations.Any(r => r.CitizenIdentificationCard == code);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormCollection form, List<IFormFile> Image, ResidenceBooklet residenceBooklet, Population population)
        {
            var ResidenceBookletCode = form["ResidenceBookletCode"].ToString();

            var Address = form["Address"].ToString();
            var BookletArea = form["BookletArea"].ToString();

            var PopulationName = form["PopulationName"].ToString();
            var PopulationNickName = form["PopulationNickName"].ToString();
            var GenderValue = form["Gender"].ToString();
            bool Gender;
            if (GenderValue == "1")
            {
                Gender = true;
            }
            else
            {
                Gender = false;
            }
            var DateOfBirth = form["DateOfBirth"];
            var BirthPlace = form["BirthPlace"].ToString();
            var Ethnicity = form["Ethnicity"].ToString();
            var Religion = form["Religion"].ToString();
            var WorkPlace = form["WorkPlace"].ToString();
            var Occupation = form["Occupation"].ToString();
            var CitizenIdentificationCard = form["CitizenIdentificationCard"].ToString();
            var DateOfIssue = form["DateOfIssue"];
            var PlaceOfIssue = form["PlaceOfIssue"].ToString();
            var EducationalLevels = form["EducationalLevels"].ToString();

            if (String.IsNullOrEmpty(ResidenceBookletCode))
            {
                ViewBag.ErrorResidenceBookletCode = "Mã hộ khẩu không thể để trống";
            }
            else if (String.IsNullOrEmpty(Address))
            {
                ViewBag.ErrorAddress = "Bạn chưa nhập địa chỉ không thể để trống";
            }
            else if (String.IsNullOrEmpty(PopulationName))
            {
                ViewBag.ErrorPopulationName = "Bạn chưa nhập họ tên";
            }
            else if (String.IsNullOrEmpty(DateOfBirth))
            {
                ViewBag.ErrorDateOfBirth = "Bạn chưa nhập ngày sinh";
            }
            else if (String.IsNullOrEmpty(BirthPlace))
            {
                ViewBag.ErrorBirthPlace = "Bạn chưa nhập nơi sinh";
            }
            else if (String.IsNullOrEmpty(Ethnicity))
            {
                ViewBag.ErrorEthnicity = "Bạn chưa nhập dân tộc";
            }
            else if (String.IsNullOrEmpty(CitizenIdentificationCard))
            {
                ViewBag.ErrorCitizenIdentificationCard = "Bạn chưa nhập mã CMND/CCCD";
            }
            else if (String.IsNullOrEmpty(DateOfIssue))
            {
                ViewBag.ErrorDateOfIssue = "Bạn chưa nhập ngày cấp CMND/CCCD";
            }
            else if (String.IsNullOrEmpty(PlaceOfIssue))
            {
                ViewBag.ErrorPlaceOfIssue = "Bạn chưa nhập nơi cấp CMND/CCCD";
            }
            else if (CheckResidenceBookletCode(ResidenceBookletCode))
            {
                ViewBag.ErrorResidenceBookletCode = "Mã hộ khẩu đã tồn tại trong hệ thống";
            }
            else if (CheckCitizenIdentificationCard(CitizenIdentificationCard))
            {
                ViewBag.ErrorCitizenIdentificationCard = "CMND/CCCD đã tồn tại";
            }
            else
            {
                residenceBooklet.ResidenceBookletCode = ResidenceBookletCode;
                residenceBooklet.Address = Address;
                residenceBooklet.BookletArea = BookletArea;
                residenceBooklet.CreateDate = DateTime.Now;

                population.PopulationName = PopulationName;
                population.PopulationNickName = PopulationNickName;
                population.Gender = Convert.ToBoolean(Gender);
                population.BirthPlace = BirthPlace;
                population.DateOfBirth = Convert.ToDateTime(DateOfBirth);
                population.Ethnicity = Ethnicity;
                population.Religion = Religion;
                population.WorkPlace = WorkPlace;
                population.Occupation = Occupation;
                population.CitizenIdentificationCard = CitizenIdentificationCard;
                population.DateOfIssue = Convert.ToDateTime(DateOfIssue);
                population.PlaceOfIssue = PlaceOfIssue;
                population.EducationalLevels = EducationalLevels;
                population.CreatedDate = DateTime.Now;
                population.Alive = true;
                population.Image = await Utilities.Upload(_environment, Image, "populations");
                population.ResidenceBooklet = residenceBooklet;
                population.Relationship = "Chủ hộ";

                _context.Add(residenceBooklet);
                _context.Add(population);
                _context.SaveChanges();

                _notyfService.Success("Thêm mới hộ khẩu thành công");
                return RedirectToAction("Index", "RecedentceBooklet");
            }
            return this.Index();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookLet bookLet, List<IFormFile> image)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                if (CheckResidenceBookletCode(bookLet.ResidenceBooklet.ResidenceBookletCode))
                {
                    ViewBag.Error = "Mã hộ khẩu đã tồn tại";
                }
                if (CheckCitizenIdentificationCard(bookLet.Population.CitizenIdentificationCard))
                {
                    ViewBag.Error = "Căn cước công dân đã tồn tại";
                }
                if (bookLet.ResidenceBooklet.ResidenceBookletCode == null)
                {
                    ViewBag.Error = "Bạn phải nhập mã hộ khẩu";
                }
                if (bookLet.ResidenceBooklet.Address == null)
                {
                    ViewBag.Error = "Bạn phải nhập địa chỉ hộ khẩu";
                }
                if (bookLet.Population.PopulationName == null)
                {
                    ViewBag.Error = "Bạn phải nhập họ tên";
                }
                if (bookLet.Population.CitizenIdentificationCard == null)
                {
                    ViewBag.Error = "Bạn phải nhập CCCD";
                }
                if (bookLet.Population.DateOfIssue == null)
                {
                    ViewBag.Error = "Bạn phải nhập ngày cấp CCCD";
                }
                if (bookLet.Population.PlaceOfIssue == null)
                {
                    ViewBag.Error = "Bạn phải nhập nơi cấp CCCD";
                }
                if (bookLet.Population.DateOfBirth == null)
                {
                    ViewBag.Error = "Bạn phải ngày tháng năm sinh";
                }
                if (bookLet.Population.Ethnicity == null)
                {
                    ViewBag.Error = "Bạn phải nhập dân tộc";
                }
                else
                {
                    ResidenceBooklet residenceBooklet = new ResidenceBooklet();
                    Population population = new Population();
                    residenceBooklet.ResidenceBookletCode = bookLet.ResidenceBooklet.ResidenceBookletCode;
                    residenceBooklet.Address = bookLet.ResidenceBooklet.Address;
                    residenceBooklet.CreateDate = DateTime.Now.Date;
                    residenceBooklet.BookletArea = bookLet.ResidenceBooklet.BookletArea;

                    population.PopulationName = bookLet.Population.PopulationName;
                    population.Gender = bookLet.Population.Gender;
                    population.Image = await Utilities.Upload(_environment, image, "populations");
                    population.DateOfBirth = bookLet.Population.DateOfBirth;
                    population.CitizenIdentificationCard = bookLet.Population.CitizenIdentificationCard;
                    population.DateOfIssue = bookLet.Population.DateOfIssue;
                    population.PlaceOfIssue = bookLet.Population.PlaceOfIssue;
                    population.Ethnicity = bookLet.Population.Ethnicity;
                    population.Religion = bookLet.Population.Religion;
                    population.Occupation = bookLet.Population.Occupation;
                    population.WorkPlace = bookLet.Population.WorkPlace;
                    string relationship = "Chủ hộ";
                    population.Relationship = bookLet.Population.Relationship;
                    population.CreatedDate = DateTime.Now.Date;
                    population.ResidenceBooklet = residenceBooklet;
                    population.CreatedDate = DateTime.Now.Date;
                    population.Alive = true;
                    population.BirthPlace = bookLet.ResidenceBooklet.Address;


                    _context.Add(residenceBooklet);
                    _context.Add(population);
                    _context.SaveChanges();

                    _notyfService.Success("Thêm mới sổ hộ khẩu thành công");
                    return RedirectToAction("Index", "RecedentceBooklet");
                }
                return View("Index", "RecedentceBooklet");
                //}
                //return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Route("/di-chuyen-ho-khau/{id}")]
        public async Task<IActionResult> MoveRecedentceBooklet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residenceBooklet = await _context.ResidenceBooklets.FindAsync(id);
            if (residenceBooklet == null)
            {
                return NotFound();
            }
            return View(residenceBooklet);
        }
        [HttpPost]
        [Route("/di-chuyen-ho-khau/{id}")]
        public async Task<IActionResult> MoveRecedentceBooklet(int? id, ResidenceBooklet residenceBooklet)
        {
            if (id != residenceBooklet.ResidenceBookletId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //tìm sổ hộ khẩu theo id
                    var findRB = _context.ResidenceBooklets.FirstOrDefault(r => r.ResidenceBookletId == id);

                    if (findRB != null)
                    {
                        findRB.Address = residenceBooklet.Address;
                        findRB.MoveDate = DateTime.Now;
                        findRB.MoveReason = residenceBooklet.MoveReason;

                        _context.Update(findRB);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResidenceBookletExists(residenceBooklet.ResidenceBookletId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(residenceBooklet);
        }
        private bool ResidenceBookletExists(int id)
        {
            return _context.ResidenceBooklets.Any(e => e.ResidenceBookletId == id);
        }

        [Route("/tach-khau/{id}")]
        public IActionResult SpltRecedence(int id)
        {
            var query = _context.Populations.Where(p => p.ResidenceBookletId == id).ToList();
            ViewData["Population"] = query;

            return View();
        }
        //[HttpPost]
        //[Route("/add-residence-split")]
        //public IActionResult CreateResidentceSplit()
        //{
        //    try
        //    {

        //        return Json(new {results = true});
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { results = true });
        //        throw;
        //    }


        //}
        [Route("/tach-khau/{id}")]
        [HttpPost]
        public IActionResult SpltRecedence(int id, List<string> check, List<string> listRelationship, string ResidenceBookletCode, string BookletArea, string Address)
        {
            //tạo mới sổ hộ khẩu để tách
            ResidenceBooklet booklet = new ResidenceBooklet();

            booklet.ResidenceBookletCode = ResidenceBookletCode;
            booklet.Address = Address;
            booklet.BookletArea = BookletArea;
            booklet.CreateDate = DateTime.Now;

            _context.Add(booklet);
            _context.SaveChanges();

            //tìm thành viên để tách sổ
            var query = _context.Populations.Where(p => p.ResidenceBookletId == id && p.Relationship != "Chủ hộ").ToList();

            //xác định những thành viên được tách
            foreach (var item in query)
            {
                foreach (var choose in check)
                {
                    if (item.CitizenIdentificationCard == choose)
                    {
                        item.IsChoose = true;
                        _context.Update(item);
                        _context.SaveChanges();
                    }
                }
            }

            //xác định quan hệ của những thành viên vừa chọn
            foreach (var item in query)
            {
                if (listRelationship.Count > 0)
                {
                    for (int i = 0; i < listRelationship.Count; i++)
                    {
                        if (item.IsChoose == true && item.Relationship == "Chủ hộ") break;
                        if (item.IsChoose == true && item.Relationship != "Chủ hộ")
                        {
                            item.Relationship = listRelationship[i];//gán quan hệ được chọn
                            item.ResidenceBooklet = booklet;//thay đổi lại mã sổ
                            _context.Update(item);
                            _context.SaveChanges();
                            listRelationship.Remove(listRelationship[i]);//loại bỏ quan hệ đã được gán
                        }
                    }
                }
            }
            return RedirectToAction("Index", "RecedentceBooklet");
        }
    }
}
