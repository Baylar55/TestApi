using CICWebApi.Entities;
using Data.DataContext;
using Data.Entities;
using DataAccess.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services.Implementation
{
    public class RequestStatusService:IRequestStatusService
    {
        private readonly AppDbContext _context;

        public RequestStatusService(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public ICollection<RequestStatus> GetAll()
        {
            return _context.RequestStatuses.ToList();
        }

        public RequestStatus GetById(int id)
        {
            return _context.RequestStatuses.FirstOrDefault(c => c.Id == id);
        }

        public bool isExist(int id)
        {
            var RequestStatus = _context.RequestStatuses.Any(c => c.Id == id);
            if (RequestStatus == null) return false;
            return true;
        }

        public bool Create(RequestStatus RequestStatus)
        {
            _context.Add(RequestStatus);
            _context.SaveChanges();
            return true;
        }

        public bool Update(RequestStatus RequestStatus)
        {
            _context.Update(RequestStatus);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(RequestStatus RequestStatus)
        {
            _context.Remove(RequestStatus);
            _context.SaveChanges();
            return true;
        }

        public ICollection<Request> GetRequestsByRequestStatus(int RequestStatusId)
        {
            return _context.Requests
                           .Where(r => r.RequestStatusId == RequestStatusId)
                           .Include(r => r.Category)
                           .Include(r => r.Priority)
                           .Include(r => r.RequestType)
                           .Include(r => r.RequestStatus)
                           .Include(r => r.CreatorUser)
                           .Include(r => r.ExecutorUser)
                           .ToList();
        }
    }
}
