using Microsoft.AspNetCore.Mvc;
using ModelUserRole = ST10062630_PROG6212_POE.Models.UserRole; // Alias for Models.UserRole
using ST10062630_PROG6212_POE.Data;
using ST10062630_PROG6212_POE.Models;
using System.Data;
using System.Linq;

public class MainController : Controller
{
    private readonly ApplicationDbContext _context;

    public MainController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        try
        {
            var roles = _context.Roles.ToList();
            if (roles == null || !roles.Any())
            {
                TempData["ErrorMessage"] = "No roles available. Please add roles to the system.";
            }
            ViewBag.Roles = roles;

            var claims = _context.Claims.ToList();
            return View(claims);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            ViewBag.Roles = new List<ModelUserRole>(); // Set an empty list to avoid null reference in the view
            return View(new List<Claim>());
        }
    }

    public IActionResult SubmitClaim(Claim model, string roleName)
    {
        if (ModelState.IsValid)
        {
            model.Amount = model.HoursWorked * model.HourlyRate;
            model.Status = "Pending"; // Default status
            model.Month = DateTime.Now.ToString("MMMM"); // Current month

            // Use RoleName to find the correct role
            var selectedRole = _context.Roles.FirstOrDefault(r => r.RoleName == roleName);
            if (selectedRole != null)
            {
                model.Role = selectedRole.RoleName; // Assign role name to the claim
                _context.Claims.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Claim submitted successfully!";
            }
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to submit the claim. Check the input.";
        }

        return RedirectToAction("Index");
    }
}
