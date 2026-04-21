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
    public class ContractsController : Controller
    {
        private readonly IContractService _contractService;
        private readonly DataContext _context;

        public ContractsController(IContractService contractService, DataContext context)
        {
            _contractService = contractService;
            _context = context;
        }

        // GET: Contracts
        public async Task<IActionResult> Index(string status, DateTime? startDate, DateTime? endDate)
        {

            var contracts = await _contractService.GetAllContractsAsync(status, startDate, endDate);
            return View(contracts);
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _contractService.GetContractDetailsAsync(id.Value);

            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContractFormViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                //re-populates the client id dropdown, so user can reselect it 
                ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", viewmodel.ClientId);

                return View(viewmodel);
            }

            try
            {
                await _contractService.CreateContractAsync(viewmodel);
                TempData["NotificationMessage"] = "Contract created successfully. SMS and WhatsApp notifications sent.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", viewmodel.ClientId);
                return View(viewmodel);
            }
        }



        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _contractService.GetContractByIdAsync(id.Value);

            if (contract == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", contract.ClientId);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractId,ContractStartDate,ContractEndDate,ContractStatus,ContractServiceLevel,ClientId")] Contract contract)
        {
            if (id != contract.ContractId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", contract.ClientId);
                return View(contract);
            }

            try
            {
                await _contractService.UpdateContractAsync(contract);
                TempData["NotificationMessage"] = $"Contract #{contract.ContractId} successfully updated. SMS and WhatsApp notifications sent.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Contracts.Any(e => e.ContractId == contract.ContractId))
                {
                    return NotFound();
                }

                throw;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", contract.ClientId);
                return View(contract);
            }
        }




        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.ContractId == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _contractService.DeleteContractAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
