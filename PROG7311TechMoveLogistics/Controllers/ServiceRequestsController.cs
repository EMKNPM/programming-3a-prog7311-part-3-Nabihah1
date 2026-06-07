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
using PROG7311TechMoveLogistics.APIServices;

namespace PROG7311TechMoveLogistics.Controllers
{
    public class ServiceRequestsController : Controller
    {
        //private readonly DataContext _context;
        private readonly ICurrencyService _currencyService;
        private readonly IServiceRequestApiService _serviceRequestApiService;
        private readonly IContractApiService _contractApiService;


        public ServiceRequestsController(IContractApiService contractApiService, ICurrencyService currencyService, IServiceRequestApiService serviceRequestApiService)
        {
            _contractApiService = contractApiService;
            _currencyService = currencyService;
            _serviceRequestApiService = serviceRequestApiService;
        }

        // GET: ServiceRequests
        public async Task<IActionResult> Index()
        {
            var requests =  await _serviceRequestApiService.GetAllAsync();

            return View(requests);
        }

        // GET: ServiceRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceRequest =   await _serviceRequestApiService.GetByIdAsync(id.Value);

            if (serviceRequest == null)
            {
                return NotFound();
            }

            return View(serviceRequest);
        }

        // GET: ServiceRequests/Create
        public async Task<IActionResult> Create()
        {
            var contracts =  await _contractApiService.GetAllContractsAsync();

            ViewData["ContractId"] = new SelectList( contracts, "ContractId","ContractId");

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
                var contracts = await _contractApiService.GetAllContractsAsync();

                ViewData["ContractId"] = new SelectList(  contracts, "ContractId", "ContractId");
                return View(serviceRequest);
            }

            try
            {
                await _serviceRequestApiService.CreateAsync(serviceRequest);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;

                var contracts =  await _contractApiService.GetAllContractsAsync();

                ViewData["ContractId"] = new SelectList( contracts, "ContractId",  "ContractId");
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

            var serviceRequest =  await _serviceRequestApiService.GetByIdAsync(id.Value);

            if (serviceRequest == null)
            {
                return NotFound();
            }
            var contracts =await _contractApiService.GetAllContractsAsync();

            ViewData["ContractId"] =  new SelectList(  contracts,  "ContractId",  "ContractId",  serviceRequest.ContractId);

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
                var contracts =
     await _contractApiService.GetAllContractsAsync();

                ViewData["ContractId"] =
                    new SelectList(
                        contracts,
                        "ContractId",
                        "ContractId",
                        serviceRequest.ContractId);
                return View(serviceRequest);
            }


            try
            {
                await _serviceRequestApiService.UpdateAsync(serviceRequest);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                var contracts =  await _contractApiService.GetAllContractsAsync();

                ViewData["ContractId"] =
                    new SelectList(
                        contracts,
                        "ContractId",
                        "ContractId",
                        serviceRequest.ContractId);
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

            var serviceRequest =  await _serviceRequestApiService.GetByIdAsync(id.Value);

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
            await _serviceRequestApiService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        //private bool ServiceRequestExists(int id)
        //{
        //    return _context.ServiceRequests.Any(e => e.ServiceRequestId == id);
        //}




    }
}