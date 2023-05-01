using DemoApp.Data;
using DemoApp.Data.DTO;
using DemoApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DemoApp.Controllers
{
    [Authorize]
    public class ApplicationsController : Controller
    {

        private readonly ApplicationDbContext _context = default!;
        private readonly UserManager<User> _userManager;
        public ApplicationsController(ApplicationDbContext context, UserManager<User> userManager)
        {

            this._context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            try
            {

                var now = DateTime.Now;
                List<IpoInformation> instruments = _context.IpoInformations
                    .ToList();
                ViewBag.Instrument = new SelectList(instruments, "InstrumentId", "InstrumentName");
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var data = _context.ApplicationDetails.Where(x => x.UserId == user.Id).ToList();
                return View(data);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }



        public IActionResult Create()
        {
            var now = DateTime.Now;

            List<IpoInformation> instruments = _context.IpoInformations
                   .Where(i => i.StartDate <= now && i.EndDate >= now)
                   .ToList();
            ViewBag.Instrument = new SelectList(instruments, "InstrumentId", "InstrumentName");


            return View();
        }

        public IActionResult GetInstrumentDetails(int id)
        {
            IpoInformation instrument = _context.IpoInformations.SingleOrDefault(i => i.InstrumentId == id)!;
            if (instrument == null)
            {
                return NotFound();
            }

            return Json(new
            {
                facevalue = instrument.Facevalue,
                premium = instrument.Premium,
                iporate = instrument.IpoRate,
                minimumAmount = instrument.MinimumAmount
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(IPOApplicationDTO dto)
        {

            try
            {
                IpoInformation ipoInformation = _context.IpoInformations.Where(x => x.InstrumentId == dto.InstrumentId).FirstOrDefault()!;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var existingDetails = _context.ApplicationDetails.FirstOrDefault(x => x.InstrumentId == dto.InstrumentId && x.UserId == user.Id);
                if (existingDetails != null)
                {
                    ViewBag.Instrument = new SelectList(_context.IpoInformations.ToList(), "InstrumentId", "InstrumentName");
                    TempData["AlertMessage"] = "You already applied this IPO ";
                    return View();
                }
                if (dto.InstrumentId == 0)
                {

                    TempData["AlertMessage"] = "Please select a company/fund"; ;
                    ViewBag.Instrument = new SelectList(_context.IpoInformations.ToList(), "InstrumentId", "InstrumentName");
                    return View();
                }


                IpoApplicationDetails details = new IpoApplicationDetails()
                {
                    UserId = user.Id,
                    InstrumentId = dto.InstrumentId,
                    ApplicationAmount = ipoInformation.MinimumAmount,
                    CreatedDate = DateTime.Now.Date,
                    IsApproved = false,
                    IsExecuted = false,
                };
                _context.ApplicationDetails.Add(details);
                _context.SaveChanges();


            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred ";
                ViewBag.ErrorMessage = errorMessage;
                ViewBag.Instrument = new SelectList(_context.IpoInformations.ToList(), "InstrumentId", "InstrumentName");
                TempData["AlertMessage"] = "Error Has been Placed!!!!!! ";
                return View();
            }
            ViewBag.Instrument = new SelectList(_context.IpoInformations.ToList(), "InstrumentId", "InstrumentName");
            return RedirectToAction("Index");

        }

        public IActionResult GetDetails(int id)
        {
            try
            {
                IpoInformation ipoInformation = _context.IpoInformations.FirstOrDefault(x => x.InstrumentId == id)!;

                if (ipoInformation == null)
                {
                    return NotFound();
                }

                IPOApplicationDTO dto = new IPOApplicationDTO()
                {
                    InstrumentId = ipoInformation.InstrumentId,
                    InstrumentName = ipoInformation.InstrumentName,
                    MinimumAmount = ipoInformation.MinimumAmount,
                    IpoRate = ipoInformation.IpoRate,
                    Facevalue = ipoInformation.Facevalue,
                    Premium = ipoInformation.Premium,
                    StartDate = ipoInformation.StartDate,
                    EndDate = ipoInformation.EndDate,
                };
                ViewBag.Instrument = new SelectList(_context.IpoInformations, "InstrumentId", "InstrumentName");

                return View(dto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while processing your request.";
                return RedirectToAction("Index");
            }
        }
    }
}
