using System.Diagnostics;
using AutoMapper;
using AutoMapperMVC.Models;
using AutoMapperMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AutoMapperMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,TodoRepository repo,IMapper mapper)
        {
            _logger = logger;
            _todoRepository = repo;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var todos = _todoRepository.GetAll();
            var vm =_mapper.Map<List<TodoViewModels>>(todos);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
             _todoRepository.Add(new Todo { Title = title, IsCompleted = false });
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _todoRepository.DeleteCompleted(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteEach(int id)
        {
            _todoRepository.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Toggle(int id)
        {
            _todoRepository.Toggle(id);
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult Edit(TodoViewModels vm)
        {
            if (!string.IsNullOrWhiteSpace(vm.Title))
            {
                var todo = _mapper.Map<Todo>(vm);
                _todoRepository.Update(todo);
            }
            return RedirectToAction("Index");
        }
    }
}
