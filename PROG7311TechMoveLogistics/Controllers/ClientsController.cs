using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROG7311TechMoveLogistics.Data;
using PROG7311TechMoveLogistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PROG7311TechMoveLogistics.APIServices;

namespace PROG7311TechMoveLogistics.Controllers
{
    public class ClientsController : Controller
    {
        //PART 3: wants us to NOT use DataContext, so imma comment all the code out 
        // we suppoed to use APIs 

        //private readonly DataContext _context;

        //public ClientsController(DataContext context)
        //{
        //    _context = context;
        //}

        private readonly IClientAPIService _clientApiService;

        public ClientsController(IClientAPIService clientApiService)
        {
            _clientApiService = clientApiService;
        }


        // GET: Clients
        public async Task<IActionResult> Index()
        {
            try
            {
                var clients = await _clientApiService.GetAllClientsAsync();
                return View(clients);
            }
            catch (Exception ex)
            {
                return Content($"API error: {ex.Message}");
            }
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientApiService.GetClientByIdAsync(id.Value);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,ClientName,ContactDetails,Region")] Client client)
        {
            if (ModelState.IsValid)
            {
                await _clientApiService.CreateClientAsync(client);

                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientApiService.GetClientByIdAsync(id.Value);

            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,ClientName,ContactDetails,Region")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _clientApiService.UpdateClientAsync(client);
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return View(client);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientApiService.GetClientByIdAsync(id.Value);
           
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clientApiService.DeleteClientAsync(id);
           
            return RedirectToAction(nameof(Index));
        }

        //private bool ClientExists(int id)
        //{
        //    return _context.Clients.Any(e => e.ClientId == id);
        //}

    }
}
