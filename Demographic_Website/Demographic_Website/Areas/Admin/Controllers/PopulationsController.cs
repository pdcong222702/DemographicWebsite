using AspNetCoreHero.ToastNotification.Abstractions;
using Demographic_Website.Helper;
using Demographic_Website.Models;
using Demographic_Website.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Security.Policy;



namespace Demographic_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PopulationsController : Controller
    {

        private readonly DemoGraphicContext _context;
        public INotyfService _notyfService { get; }
        public readonly IWebHostEnvironment _environment;
        //private int ResidenceBookletID = 0;
        public PopulationsController(DemoGraphicContext demographicContext, INotyfService notyfService, IWebHostEnvironment environment)
        {
            _context = demographicContext;
            _notyfService = notyfService;
            _environment = environment;
        }
        [Route("/chi-tiet-so-ho-khau/{id}")]
        public IActionResult Details(int id)
        {
            var query =_context.Populations.ToList().Where(p=>p.ResidenceBookletId==id);
            //ViewData["Population"] = query;

            //var findResidenceBookletID = _context.Populations.Join(_context.ResidenceBooklets,p=>p.ResidenceBookletId,r=>r.ResidenceBookletId,(p,r)=> new BookLet
            //{
            //    Population=p,
            //    ResidenceBooklet=r
            //}).SingleOrDefault(p => p.Population.ResidenceBookletId == id && p.Population.Relationship == "Chủ hộ").Population.ResidenceBookletId;
            //ViewData["ResidenceBookletID"] = findResidenceBookletID;
            if (query == null)
            {
                return Json(new { success = 0, message = "Không có nhân khẩu" });
            }
            //return Json(new { success = 1, populations = result });
            return View(query);
        }


        public bool CheckCitizenIdentificationCard(string code)
        {
            return _context.Populations.Any(r => r.CitizenIdentificationCard == code);
        }
        [HttpPost]
        [Route("/chi-tiet-so-ho-khau/{id}")]
        public async Task<IActionResult> Details(Population population,List<IFormFile> Image, int id)
        {
            try
            {
                if (CheckCitizenIdentificationCard(population.CitizenIdentificationCard))
                {
                    ViewBag.Error = "CCCD đã tồn tại";
                }
                else
                {
                    string url = "/chi-tiet-so-ho-khau/" + id;
                    population.CreatedDate = DateTime.Now.Date;
                    population.ResidenceBookletId = id;
                    population.Alive = true;
                    population.Image = await Utilities.Upload(_environment, Image, "populations");
                    _context.Add(population);
                    _context.SaveChanges();

                    _notyfService.Success("Thêm nhân khẩu thành công");
                    return Redirect(url);
                }
                return this.Details(id);
            }
            catch (Exception)
            {
                return Json(new { result = false });
                throw;
            }
        }

        [HttpPost]
        [Route("/update-condition-alive/{id}")]
        public IActionResult Alive(int id)
        {
            try
            {
                var populationAlive = _context.Populations.FirstOrDefault(p => p.PopulationId == id && p.Alive == false);
                string url = "/chi-tiet-so-ho-khau/" + populationAlive.ResidenceBookletId;
                if (populationAlive == null)
                {
                    return NotFound();
                }
                else
                {
                    if(populationAlive.Alive == false)
                    {
                        populationAlive.Alive = true;
                        _context.Update(populationAlive);
                        _context.SaveChanges();
                    }
                    return Json(new { result = true, redirectUrl = url });
                }
            }
            catch (Exception)
            {
                return Json(new { result = false });
                throw;
            }
        }

        [HttpPost]
        [Route("/update-condition-dead/{id}")]
        public IActionResult Dead(int id)
        {
            try
            {
                var populationAlive=_context.Populations.FirstOrDefault(p=>p.PopulationId==id && p.Alive==true);
                string url = "/chi-tiet-so-ho-khau/" + populationAlive.ResidenceBookletId;
                if (populationAlive == null)
                {
                    return NotFound();
                }
                else
                {
                    populationAlive.Alive = false;
                    populationAlive.DateOfDeath = DateTime.Now;
                    _context.Update(populationAlive);
                    _context.SaveChanges();
                    _notyfService.Error("Người này hiện không còn sống");
                    return Json(new { result = true, redirectUrl = url });
                }
            }
            catch (Exception)
            {
                return Json(new { result = false});
                throw;
            }
        }
        [HttpGet]
        [Route("/get-data-edit-population")]
        public IActionResult GetDataEditPopulation(int id)
        {
            try
            {
                var result = _context.Populations.FirstOrDefault(p=>p.PopulationId==id);
                return Json(new { data = result });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var population = await _context.Populations.FindAsync(id);
            if (population == null)
            {
                return NotFound();
            }
            ViewData["ResidenceBookletId"] = new SelectList(_context.ResidenceBooklets, "ResidenceBookletId", "ResidenceBookletId", population.ResidenceBookletId);
            return View(population);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PopulationId,PopulationName,PopulationNickName,Gender,Image,DateOfBirth,BirthPlace,Ethnicity,Religion,WorkPlace,Occupation,alive,CitizenIdentificationCard,DateOfIssue,PlaceOfIssue,CreatedDate,ResidenceBookletId,Relationship")] Population population)
        {
            if (id != population.PopulationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    population.Alive = true;
                    _context.Update(population);
                    await _context.SaveChangesAsync();
                    string url = "/chi-tiet-so-ho-khau/" + population.ResidenceBookletId;
                    _notyfService.Success("Thay đổi thông tin nhân khẩu thành công");
                    return Redirect(url);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PopulationExists(population.PopulationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["ResidenceBookletId"] = new SelectList(_context.ResidenceBooklets, "ResidenceBookletId", "ResidenceBookletId", population.ResidenceBookletId);
            return View(population);
        }

        [HttpPost]
        [Route("/edit-data-population/{id}")]
        public IActionResult EditPopulation(int id, [Bind("PopulationId,PopulationName,PopulationNickName,Gender,Image,DateOfBirth,BirthPlace,Ethnicity,Religion,WorkPlace,Occupation,alive,CitizenIdentificationCard,DateOfIssue,PlaceOfIssue,CreatedDate,ResidenceBookletId,Relationship")] Population population)
        {
            try
            {
                var populationOld=_context.Populations.FirstOrDefault(p=>p.PopulationId == id);
                var abc = population;
                string url = "/chi-tiet-so-ho-khau/" + populationOld.ResidenceBookletId;
                if (populationOld != null)
                {
                    populationOld.PopulationName = population.PopulationName;
                    populationOld.PopulationNickName = population.PopulationNickName;
                    populationOld.Gender = population.Gender;
                    populationOld.DateOfBirth = population.DateOfBirth;
                    populationOld.BirthPlace = population.BirthPlace;
                    populationOld.Ethnicity = population.Ethnicity;
                    populationOld.Religion = population.Religion;
                    populationOld.WorkPlace = population.WorkPlace;
                    populationOld.Occupation = population.Occupation;
                    populationOld.DateOfIssue = population.DateOfIssue;
                    populationOld.PlaceOfIssue = population.PlaceOfIssue;
                    populationOld.Relationship = population.Relationship;
                    _context.Update(populationOld);
                    _context.SaveChanges();
                    return Json(new { result = true, redirectUrl = url });
                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Route("danh-sach-nhan-khau")]
        public IActionResult ListPopulation()
        {
            return View();
        }

        private bool PopulationExists(int id)
        {
            return _context.Populations.Any(e => e.PopulationId == id);
        }
    }
}
