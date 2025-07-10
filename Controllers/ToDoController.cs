using Microsoft.AspNetCore.Mvc;
using TaskBell.Repositories;

namespace TaskBell.Controllers
{
    public class ToDoController : Controller
    {

        private readonly ILogger<ToDoController> _logger;
        private readonly IToDoItemRepository _repository;

        public ToDoController(IToDoItemRepository repository, ILogger<ToDoController> logger) 
        { 
            _logger = logger;
            _repository = repository;
        }


        
    }
}
