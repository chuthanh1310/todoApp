using ADO.NETtodoApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ADO.NETtodoApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly ApplicationDBContext _context;
        public TodoController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var todos = _context.Todos.OrderByDescending(t => t.CreatedAt).ToList();
            var countUnfinished = todos.Count(t => !t.IsCompleted);
            ViewBag.CountUnfinished = countUnfinished;
            return View("/Views/Home/Index.cshtml", todos);
        }
        [HttpPost]
        public IActionResult EditTitle(int Id, string NewTitle)
        {
            var todo = _context.Todos.Find(Id);
            if (todo != null && !string.IsNullOrWhiteSpace(NewTitle))
            {
                todo.Title = NewTitle.Trim();
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteComplete(int id)
        {
           var completeTodos=_context.Todos.Where(t => t.IsCompleted).ToList();
            if (completeTodos.Any())
            {
                _context.Todos.RemoveRange(completeTodos);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult CreateInline(string Title)
        {
            if (!string.IsNullOrWhiteSpace(Title))
            {
                var todo = new Todo
                {
                    Title = Title,
                    IsCompleted = false,
                    CreatedAt = DateTime.Now
                };

                _context.Todos.Add(todo);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ToggleComplete(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo != null)
            {
                todo.IsCompleted = !todo.IsCompleted;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                _context.Todos.Add(todo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        // POST: Todo/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
