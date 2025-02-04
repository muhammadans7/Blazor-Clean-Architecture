using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain.Models;
using WebApplication1.Application.Interfaces;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace WebApplication1.Presentation.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly IComplaintRepository _complaintRepository;

        // Constructor with Dependency Injection
        public ComplaintsController(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        // GET: Complaints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Complaints/Create
        [HttpPost]
        public IActionResult Create(Complaint complaint)
        {

             if (ModelState.IsValid)
            {
                _complaintRepository.AddComplaint(complaint);
                // Return a JSON response with a success message
                return Json(new { message = "Thanks! We are glad to assist you." });
            }

            return Json(new { message = "There was an issue with your submission." });
            // if (ModelState.IsValid)
            // {
            //     // Store the complaint in the database
            //     _complaintRepository.AddComplaint(complaint);
            //     return Json(new { success = true });
            // }
            // return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }
    }
}
