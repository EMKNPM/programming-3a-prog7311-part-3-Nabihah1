using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROG7311TechMoveLogistics.APIServices;
using TechMoveLogisticsAPI.Models;
using TechMoveLogisticsAPI.DTOs;
using PROG7311TechMoveLogistics.Models;
using PROG7311TechMoveLogistics.Services;

namespace PROG7311TechMoveLogistics.Controllers
{
    public class ContractsController : Controller
    {
        private readonly IContractApiService _contractApiService;
        private readonly IClientAPIService _clientApiService;

        public ContractsController(
            IContractApiService contractApiService,
            IClientAPIService clientApiService)
        {
            _contractApiService = contractApiService;
            _clientApiService = clientApiService;
        }

        // GET: Contracts
        public async Task<IActionResult> Index(string status, DateTime? startDate, DateTime? endDate)
        {
            var contracts = await _contractApiService
                .GetAllContractsAsync(status, startDate, endDate);

            return View(contracts);
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var contract = await _contractApiService.GetContractByIdAsync(id.Value);

            if (contract == null) return NotFound();

            return View(contract);
        }

        // GET: Contracts/Create
        public async Task<IActionResult> Create()
        {
            var clients = await _clientApiService.GetAllClientsAsync();

            ViewData["ClientId"] =
                new SelectList(clients, "ClientId", "ClientName");

            return View();
        }

        // POST: Contracts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContractFormViewModel viewmodel)
        {
            var clients = await _clientApiService.GetAllClientsAsync();

            ViewData["ClientId"] =
                new SelectList(clients, "ClientId", "ClientName", viewmodel.ClientId);

            // manual validation 
            if (!viewmodel.ContractStartDate.HasValue)
                ModelState.AddModelError("ContractStartDate", "Start date is required.");

            if (!viewmodel.ContractEndDate.HasValue)
                ModelState.AddModelError("ContractEndDate", "End date is required.");

            if (viewmodel.ContractStartDate.HasValue &&
                viewmodel.ContractEndDate.HasValue &&
                viewmodel.ContractEndDate <= viewmodel.ContractStartDate)
            {
                ModelState.AddModelError("ContractEndDate", "End date must be after start date.");
            }

            // stop here if invalid
            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }

            //map to DTO
            Console.WriteLine($"MVC ClientId = {viewmodel.ClientId}");
            var dto = ContractFactory.Create(viewmodel);
            Console.WriteLine($"DTO ClientId = {dto.ClientId}");

            // send to API
            var result = await _contractApiService.CreateContractAsync(dto);

            // API error handling
            if (!string.IsNullOrEmpty(result) &&
                (result.Contains("error", StringComparison.OrdinalIgnoreCase)
                 || result.Contains("required")
                 || result.Contains("End date")
                 || result.Contains("Service")))
            {
                ModelState.AddModelError("", result);
                return View(viewmodel);
            }

            TempData["NotificationMessage"] = "Contract created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var contract = await _contractApiService.GetContractByIdAsync(id.Value);

            if (contract == null) return NotFound();

            ViewData["ClientId"] =
                new SelectList(await _clientApiService.GetAllClientsAsync(),
                "ClientId", "ClientName", contract.ClientId);

            return View(contract);
        }

        // POST: Contracts/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContractDto contract)
        {
            if (id != contract.ContractId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["ClientId"] =
                    new SelectList(await _clientApiService.GetAllClientsAsync(),
                    "ClientId", "ClientName", contract.ClientId);

                return View(contract);
            }

            try
            {
                await _contractApiService.UpdateContractAsync(contract);

                TempData["NotificationMessage"] =
                    $"Contract #{contract.ContractId} updated successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;

                ViewData["ClientId"] =
                    new SelectList(await _clientApiService.GetAllClientsAsync(),
                    "ClientId", "ClientName", contract.ClientId);

                return View(contract);
            }
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var contract = await _contractApiService.GetContractByIdAsync(id.Value);

            if (contract == null) return NotFound();

            return View(contract);
        }

        // POST: Contracts/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _contractApiService.DeleteContractAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}