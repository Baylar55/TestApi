using CICWebApi.Entities;
using Data.DataContext;
using DataAccess.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services.Implementation
{
    public class PriorityService : IPriorityService
    {

        private readonly AppDbContext _context;

        public PriorityService(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public ICollection<Priority> GetAll()
        {
            return _context.Priorities.ToList();
        }

        public Priority GetById(int id)
        {
            return _context.Priorities.FirstOrDefault(c => c.Id == id);
        }

        public bool isExist(int id)
        {
            var Priority = _context.Priorities.Any(c => c.Id == id);
            if (Priority == null) return false;
            return true;
        }

        public bool Create(Priority Priority)
        {
            _context.Add(Priority);
            _context.SaveChanges();
            return true;
        }

        public bool Update(Priority Priority)
        {
            _context.Update(Priority);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Priority Priority)
        {
            _context.Remove(Priority);
            _context.SaveChanges();
            return true;
        }

        public ICollection<Request> GetRequestsByPriority(int PriorityId)
        {
            return _context.Requests
                           .Where(r => r.PriorityId == PriorityId)
                           .Include(r=>r.Category)
                           .Include(r=>r.Priority)
                           .Include(r=>r.RequestType)
                           .Include(r=>r.RequestStatus)
                           .Include(r=>r.CreatorUser)
                           .Include(r=>r.ExecutorUser)
                           .ToList();
        }
    }
}
