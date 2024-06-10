using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demographic_Website.Models;
using SelectPdf;

namespace Demographic_Website.Controllers
{
    public class PopulationsController : Controller
    {
        private readonly DemoGraphicContext _context;

        public PopulationsController(DemoGraphicContext context)
        {
            _context = context;
        }

        // GET: Populations
        public async Task<IActionResult> Index()
        {
            var demoGraphicContext = _context.Populations.Include(p => p.ResidenceBooklet);
            return View(await demoGraphicContext.ToListAsync());
        }

        // GET: Populations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var population = await _context.Populations
                .Include(p => p.ResidenceBooklet)
                .FirstOrDefaultAsync(m => m.PopulationId == id);
            if (population == null)
            {
                return NotFound();
            }

            return View(population);
        }

        // GET: Populations/Create
        public IActionResult Create()
        {
            ViewData["ResidenceBookletId"] = new SelectList(_context.ResidenceBooklets, "ResidenceBookletId", "ResidenceBookletId");
            return View();
        }

        // POST: Populations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PopulationId,PopulationName,PopulationNickName,Gender,Image,DateOfBirth,BirthPlace,Ethnicity,Religion,WorkPlace,Occupation,alive,CitizenIdentificationCard,DateOfIssue,PlaceOfIssue,CreatedDate,ResidenceBookletId,Relationship")] Population population)
        {
            if (ModelState.IsValid)
            {
                _context.Add(population);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResidenceBookletId"] = new SelectList(_context.ResidenceBooklets, "ResidenceBookletId", "ResidenceBookletId", population.ResidenceBookletId);
            return View(population);
        }

        // GET: Populations/Edit/5
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

        // POST: Populations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Update(population);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResidenceBookletId"] = new SelectList(_context.ResidenceBooklets, "ResidenceBookletId", "ResidenceBookletId", population.ResidenceBookletId);
            return View(population);
        }

        // GET: Populations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var population = await _context.Populations
                .Include(p => p.ResidenceBooklet)
                .FirstOrDefaultAsync(m => m.PopulationId == id);
            if (population == null)
            {
                return NotFound();
            }

            return View(population);
        }

        // POST: Populations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var population = await _context.Populations.FindAsync(id);
            if (population != null)
            {
                _context.Populations.Remove(population);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PopulationExists(int id)
        {
            return _context.Populations.Any(e => e.PopulationId == id);
        }
        [HttpPost]
        public IActionResult ExportPdf()
        {
            HtmlToPdf htmlToPdf = new HtmlToPdf();
            htmlToPdf.Options.PdfPageSize = PdfPageSize.A4;
            htmlToPdf.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            htmlToPdf.Options.MarginLeft = 10;
            htmlToPdf.Options.MarginRight = 10;
            htmlToPdf.Options.MarginTop = 20;
            htmlToPdf.Options.MarginBottom = 20;

            PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString("<div>           <div style=\"display: flex;flex-direction: column;justify-content: center; align-items: center;\">               <h4>CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</h4>               <p style=\"font-size: 18px;\">Dộc lập - Tự do - Hạnh phúc</p>               <div style=\"height: 2px;width: 200px;background-color: black;margin: 10px;\"></div>               <p style=\"font-size: 20PX;font-weight: bold;\">ĐƠN ĐĂNG KÝ TẠM VẮNG</p>               <p><strong>Kính gửi:</strong> Công an xã/phường/thị trấn Trực Đại</p>           </div>           <div style=\"margin: 0 30px;\">               <div style=\"display: flex;\">                <p>Họ tên: Phạm Đức Công</p>   </div>               <div style=\"display: flex;\">                   <label>Ngày sinh:</label>                   <p style=\"margin-left: 10px;\">22-03-2002</p>               </div>               <div style=\"display: flex;\">                   <div style=\"display: flex;\">                       <label>Số CCCD:</label>                       <p style=\"margin-left: 10px;\">012345678911</p>                   </div>                   <div style=\"display: flex;margin-left: 50px;\">                       <label>Ngày cấp:</label>                       <p style=\"margin-left: 10px;\">22-03-2002</p>                   </div>                   <div style=\"display: flex;margin-left: 50px;\">                       <label>Nơi cấp:</label>                       <p style=\"margin-left: 10px;\">Cục cảnh sát</p>                   </div>               </div>               <div style=\"display: flex;\">                   <label>Địa chỉ thường chú tại:</label>                   <p style=\"margin-left: 10px;\">Xóm 5 - Xã Trực Đại - Huyện Trực Ninh - Tỉnh Nam Định</p>               </div>               <div>                   <p>Nay tôi làm đơn này xin Công an xã/phường/thị trấn Trực Đại</p>                   <p>cho tôi xin tạm vắng tại Xóm 5 - Xã Trực Đại - Huyện Trực Ninh - Tỉnh Nam Định</p>                   <p>từ 22-03-2002</p>               </div>               <div style=\"display: flex;\">                   <label>Lý do xin tạm vắng:</label>                   <p style=\"margin-left: 10px;\">Đi làm xa</p>               </div>               <p>Trong thời gian tạm vắng tôi xin hứa thực hiện tốt các nội quy, quy định về an ninh trật tự tại địa phương. Nếu tôi vi phạm tôi xin hoàn toàn chịu trách nhiệm</p>               <p>Tôi xin chân thành cảm ơn!</p>               <div style=\"display: flex;justify-content: space-between;\">                   <p><strong>Xác nhận của Công an xã/phường/thị trấn</strong></p>                   <p>Trực Đại,ngày 22 tháng 03 năm 2024</p>               </div>               <div style=\"display: flex;justify-content: flex-end;\">                   <div style=\"display: flex;justify-content: center;align-items: center;flex-direction: column; margin-right: 80px;\">                       <p><strong>Người làm đơn</strong></p>                       <p>(Ký rõ họ tên)</p>                   </div>               </div>           </div>       </div>");

            string fileName = string.Format("{0}.pdf", DateTime.Now.Ticks);
            string pathFile = string.Format("~wwwroot/images/{0}", fileName);

            pdfDocument.Save(pathFile);
            pdfDocument.Close();

            return Json(1);
        }
    }
}
