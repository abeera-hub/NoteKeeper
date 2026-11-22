using Microsoft.AspNetCore.Mvc;

namespace NoteKeeper.Repositories
{
    public class NotePageRepository : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
