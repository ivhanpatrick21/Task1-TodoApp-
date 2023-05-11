using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task1.Migrations;
using Task1.Models;
using Task1.Repository;
using Task1.ViewModel;

namespace TodoApp.Controllers
{
   // [Authorize(Roles = "Administrator, User" )]
    public class TodoController : Controller
    {
        // inmemory
        // database
        // RDBMS
        // NoSQL
        // Files

        ITodoRepo _repo;

        // tightly coupled object 
        //ITodoRepository _repo = new InMemoryRepository();
        //ITodoRepository _repo1 = new DBRepository();

        // lossely coupled implementation
        public TodoController(ITodoRepo repo)
        {
            this._repo = repo;
        }
        // [AllowAnonymous]
        public IActionResult GetAllTodos(string searchString)
        {
            var todolists = from todoss in _repo.GetAllTodos()
                           select todoss;
           
        if (!String.IsNullOrEmpty(searchString))
       {
           todolists = todolists.Where(s => s.Title.ToLower().Contains(searchString.Trim().ToLower()));
            return View(todolists.ToList());
       }
            var todolist = _repo.GetAllTodos();
            return View(todolist);
        }
        public IActionResult Details(int todoId)
        {
            var todo = _repo.GetTodoById(todoId);
            return View(todo);
        }
        public async Task <IActionResult> Delete(int todoId)
        {
            Todo todo = await _repo.GetTodoById(todoId);
            if (todo == null)
            {
                return NotFound();
            }
            // Delete the room
            await _repo.DeleteTodo(id);
            return RedirectToAction("GetAllTodos");
            //var todolist = _repo.DeleteTodo(todoId);
            //return RedirectToAction(controllerName: "Todo", actionName: "GetAllTodos"); // reload the getall page it self
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Todo newTodo) // model binded this where the views data is accepted 
        {
            if (ModelState.IsValid){
                var todo = _repo.AddTodo(newTodo);
                return RedirectToAction(controllerName: "Todo", actionName: "GetAllTodos");
            }
            ViewData["Message"] = "Data is not valid to create the Todo";
            return View();
        }

        [HttpGet]
        public IActionResult Update(int todoId)
        {
            var oldTodo = _repo.GetTodoById(todoId);
            return View(oldTodo);
        }
        [HttpPost]
        public IActionResult Update(Todo newTodo)
        {
            var todo = _repo.UpdateTodo(newTodo.Id, newTodo);
            return RedirectToAction(controllerName: "Todo", actionName: "GetAllTodos");
        }

       
    }
}
