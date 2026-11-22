using Microsoft.AspNetCore.Mvc;

namespace NoteKeeper.Repositories.Interfaces
{
    public class INotePageRepository : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
