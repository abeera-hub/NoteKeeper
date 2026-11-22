
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteKeeper.Models;
using NoteKeeper.Repositories.Interfaces;
using System.Security.Claims;

namespace NoteKeeper.Controllers;

[Authorize]
public class NotebookController : Controller
{
    private readonly INotebookRepository _notebookRepository;

    public NotebookController(INotebookRepository notebookRepository)
    {
        _notebookRepository = notebookRepository;
    }

    private string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var notebooks = await _notebookRepository.GetAllAsync(GetUserId());

        return Json(notebooks);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] Notebook notebook)
    {
        if (string.IsNullOrWhiteSpace(notebook.Title))
        {
            return BadRequest(new { message = "Notebook title is required." });
        }

        notebook.UserId = GetUserId();

        int id = await _notebookRepository.CreateAsync(notebook);

        return Json(new { success = true, id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update([FromForm] Notebook notebook)
    {
        if (string.IsNullOrWhiteSpace(notebook.Title))
        {
            return BadRequest(new { message = "Notebook title is required." });
        }

        notebook.UserId = GetUserId();

        bool updated = await _notebookRepository.UpdateAsync(notebook);

        if (!updated)
            return NotFound(new { message = "Notebook not found." });

        return Json(new { success = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted = await _notebookRepository.DeleteAsync(id, GetUserId());

        if (!deleted)
            return NotFound(new { message = "Notebook not found." });

        return Json(new { success = true });
    }
}
