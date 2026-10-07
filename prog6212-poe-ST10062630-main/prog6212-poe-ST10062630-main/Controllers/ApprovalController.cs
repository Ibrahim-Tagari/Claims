using Microsoft.AspNetCore.Mvc;
using ST10062630_PROG6212_POE.Data;
using ST10062630_PROG6212_POE.Models;
using System.Linq;

public class ApprovalController : Controller
{
    private readonly ApplicationDbContext _context;

    public ApprovalController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        try
        {
            // Fetch all claims
            var claims = _context.Claims.ToList();
            if (claims == null || !claims.Any())
            {
                TempData["ErrorMessage"] = "No claims found!";
                return View(new List<Claim>());
            }

            return View(claims);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error: {ex.Message}";
            return View(new List<Claim>());
        }
    }

    [HttpPost]
    public IActionResult UpdateStatus(int claimId, string action)
    {
        try
        {
            var claim = _context.Claims.Find(claimId);
            if (claim != null)
            {
                claim.Status = action == "Approve" ? "Approved" : "Rejected";
                _context.SaveChanges();

                TempData["SuccessMessage"] = $"Claim {claimId} has been {claim.Status.ToLower()}!";
            }
            else
            {
                TempData["ErrorMessage"] = "Claim not found!";
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error: {ex.Message}";
        }

        return RedirectToAction("Index");
    }
}
