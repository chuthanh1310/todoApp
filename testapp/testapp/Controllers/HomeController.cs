using System.Collections.Generic;
using System.Web.Mvc;
using testapp;
using testapp.Controllers;
using testapp.Models;
using System.Linq;
public class HomeController : Controller
{
    private readonly ApplicationDBContext db = new ApplicationDBContext();

    public ActionResult Index()
    {
        var todos = db.Todos.ToList();
        var vm = MvcApplication.Mapper.Map<List<TodoViewModel>>(todos); // Dùng Mapper tĩnh
        return View(vm);
    }
    [HttpPost]
    public ActionResult ToggleStatus(int id, bool isCompleted)
    {
        var todo = db.Todos.Find(id);
        if (todo != null)
        {
            todo.IsCompleted = isCompleted;
            db.SaveChanges();
        }
        return new HttpStatusCodeResult(200);
    }

    [HttpPost]
    public ActionResult Delete(int id)
    {
        var todo = db.Todos.Find(id);
        if (todo != null)
        {
            db.Todos.Remove(todo);
            db.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult ClearCompleted()
    {
        var completedTodos = db.Todos.Where(t => t.IsCompleted).ToList();
        foreach (var item in completedTodos)
        {
            db.Todos.Remove(item);
        }
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult Create(TodoViewModel model)
    {
        if (ModelState.IsValid)
        {
            var todo = MvcApplication.Mapper.Map<Todo>(model); // Dùng Mapper tĩnh
            db.Todos.Add(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("Index");
    }

    public ActionResult About() => View();
    public ActionResult Contact() => View();
}
