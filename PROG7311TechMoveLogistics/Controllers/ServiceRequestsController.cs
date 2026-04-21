using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG7311TechMoveLogistics.Data;
using PROG7311TechMoveLogistics.Models;
using PROG7311TechMoveLogistics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROG7311TechMoveLogistics.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICurrencyService _currencyService;
        private readonly IServiceRequestService _serviceRequestService;

        public ServiceRequestsController(DataContext context, ICurrencyService currencyService, IServiceRequestService serviceRequestService)
        {
            _context = context;
            _currencyService = currencyService;
            _serviceRequestService = serviceRequestService;
        }

        // GET: ServiceRequests
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.ServiceRequests.Include(s => s.Contract);
            return View(await dataContext.ToListAsync());
        }

        // GET: ServiceRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceRequest = await _context.ServiceRequests
                .Include(s => s.Contract)
                .FirstOrDefaultAsync(m => m.ServiceRequestId == id);
            if (serviceRequest == null)
            {
                return NotFound();
            }

            return View(serviceRequest);
        }

        // GET: ServiceRequests/Create
        public IActionResult Create()
        {
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId");
            return View();
        }

        // POST: ServiceRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceRequestId,SrDescription,CostForeign,SrStatus,ContractId")] ServiceRequest serviceRequest)
        {

            if (!ModelState.IsValid)
            {
                ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId");
                return View(serviceRequest);
            }

            try
            {
                await _serviceRequestService.CreateServiceRequest(serviceRequest);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
                ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId");
                return View(serviceRequest);
            }


        }


        // GET: ServiceRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceRequest = await _context.ServiceRequests.FindAsync(id);
            if (serviceRequest == null)
            {
                return NotFound();
            }
            ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", serviceRequest.ContractId);
            return View(serviceRequest);
        }

        // POST: ServiceRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceRequestId,SrDescription,CostForeign,SrStatus,ContractId")] ServiceRequest serviceRequest)
        {
            if (id != serviceRequest.ServiceRequestId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", serviceRequest.ContractId);
                return View(serviceRequest);
            }


            try
            {
                await _serviceRequestService.EditServiceRequest(serviceRequest);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ViewData["ContractId"] = new SelectList(_context.Contracts, "ContractId", "ContractId", serviceRequest.ContractId);
                return View(serviceRequest);
            }
        }

        // GET: ServiceRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceRequest = await _context.ServiceRequests
                .Include(s => s.Contract)
                .FirstOrDefaultAsync(m => m.ServiceRequestId == id);
            if (serviceRequest == null)
            {
                return NotFound();
            }

            return View(serviceRequest);
        }

        // POST: ServiceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceRequest = await _context.ServiceRequests.FindAsync(id);
            if (serviceRequest != null)
            {
                _context.ServiceRequests.Remove(serviceRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceRequestExists(int id)
        {
            return _context.ServiceRequests.Any(e => e.ServiceRequestId == id);
        }




    }
}