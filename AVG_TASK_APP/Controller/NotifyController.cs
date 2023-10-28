using Microsoft.AspNetCore.Mvc;
using AVG_TASK_APP.DataAccess; // Import the namespace where NotifyRepository is defined

namespace AVG_TASK_APP.Controllers
{
    public class NotifyController : Controller
    {
        private readonly NotifyRepository _notifyRepository;

        public NotifyController(NotifyRepository notifyRepository)
        {
            _notifyRepository = notifyRepository;
        }

        public IActionResult Index()
        {
            var notifyViewModels = _notifyRepository.GetNotifies();
            return View(notifyViewModels);
        }
    }
}