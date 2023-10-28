using AVG_TASK_APP.Migration;
using AVG_TASK_APP.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AVG_TASK_APP.DataAccess
{
    public class NotifyRepository
    {
        private readonly AppDbContext _context;

        public NotifyRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<NotifyViewModel> GetNotifies()
        {
            var notifies = _context.Notifies.Include(n => n.User).ToList();

            var notifyViewModels = notifies.Select(notify => new NotifyViewModel
            {
                UserName = notify.User.Name,
                NotifyContent = notify.Content,
                NotifyCreatedAt = notify.Created_At
                // Add other properties if needed
            }).ToList();

            return notifyViewModels;
        }
    }
}
