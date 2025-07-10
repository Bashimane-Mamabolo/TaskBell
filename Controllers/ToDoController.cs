using Microsoft.AspNetCore.Mvc;
using TaskBell.Models;
using TaskBell.Repositories;

namespace TaskBell.Controllers
{
    [Route("Todo")]
    public class ToDoController : Controller
    {

        private readonly ILogger<ToDoController> _logger;
        private readonly IToDoItemRepository _repository;

        public ToDoController(IToDoItemRepository repository, ILogger<ToDoController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index() 
        { 
            _logger.LogInformation("Navigating to ToDoItem index page.");
            return View();
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            _logger.LogInformation("Navigating to ToDoItem creation page.");
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ToDoItem item)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for ToDoItem creation.");
                return View(item);
            }
            try
            {
                var createdItem = await _repository.AddAsync(item);
                _logger.LogInformation($"ToDoItem with ID {createdItem.Id} created successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating ToDoItem.");
                ModelState.AddModelError("", "An error occurred while creating the ToDoItem.");
                return ErrorResult("An error occurred while creating the ToDoItem.");
            }
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var item = await _repository.GetByIdAsync(id);
                if (item == null)
                {
                    _logger.LogWarning($"ToDoItem with ID {id} not found for editing.");
                    return ErrorResult($"ToDoItem with ID {id} not found.");
                }
                return View(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching ToDoItem for editing.");
                return ErrorResult("An error occurred while fetching ToDoItem for editing.");
            }
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit(ToDoItem item)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for ToDoItem editing.");
                return View(item);
            }
            try
            {
                var updatedItem = await _repository.UpdateAsync(item);
                if (updatedItem == null)
                {
                    _logger.LogWarning($"ToDoItem with ID {item.Id} not found for update.");
                    return ErrorResult($"ToDoItem with ID {item.Id} not found.");
                }
                _logger.LogInformation($"ToDoItem with ID {updatedItem.Id} updated successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating ToDoItem.");
                ModelState.AddModelError("", "An error occurred while updating the ToDoItem.");
                return ErrorResult("An error occurred while updating the ToDoItem.");
            }
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item = await _repository.GetByIdAsync(id);
                if (item == null)
                {
                    _logger.LogWarning($"ToDoItem with ID {id} not found for deletion.");
                    return ErrorResult($"ToDoItem with ID {id} not found.");
                }
                return View(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching ToDoItem for deletion.");
                return ErrorResult("An error occurred while fetching ToDoItem for deletion.");
            }
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var deleted = await _repository.DeleteAsync(id);
                if (!deleted)
                {
                    _logger.LogWarning($"ToDoItem with ID {id} not found for deletion.");
                    return ErrorResult($"ToDoItem with ID {id} not found.");
                }
                _logger.LogInformation($"ToDoItem with ID {id} deleted successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting ToDoItem.");
                ModelState.AddModelError("", "An error occurred while deleting the ToDoItem.");
                return ErrorResult("An error occurred while deleting the ToDoItem.");
            }
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var item = await _repository.GetByIdAsync(id);
                if (item == null)
                {
                    _logger.LogWarning($"ToDoItem with ID {id} not found.");
                    return ErrorResult($"ToDoItem with ID {id} not found.");
                }
                return View(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching ToDo items.");
                return ErrorResult("An error occurred while fetching ToDo items.");
            }
        }

        private IActionResult ErrorResult(string message)
        {
            var errorModel = new ErrorViewModel
            {
                Message = message,
                RequestId = HttpContext.TraceIdentifier
            };
            return View("Error", errorModel);
        }
    }
}
