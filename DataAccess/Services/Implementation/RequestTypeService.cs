using CICWebApi.Entities;
using Data.DataContext;
using DataAccess.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services.Implementation
{
    public class RequestTypeService : IRequestTypeService
    {
        private readonly AppDbContext _context;

        public RequestTypeService(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public ICollection<RequestType> GetAll()
        {
            return _context.RequestTypes.ToList();
        }

        public RequestType GetById(int id)
        {
            return _context.RequestTypes.FirstOrDefault(c => c.Id == id);
        }

        public bool isExist(int id)
        {
            var RequestType = _context.RequestTypes.Any(c => c.Id == id);
            if (RequestType == null) return false;
            return true;
        }

        public bool Create(RequestType RequestType)
        {
            _context.Add(RequestType);
            _context.SaveChanges();
            return true;
        }

        public bool Update(RequestType RequestType)
        {
            _context.Update(RequestType);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(RequestType RequestType)
        {
            _context.Remove(RequestType);
            _context.SaveChanges();
            return true;
        }

        public ICollection<Request> GetRequestsByRequestType(int RequestTypeId)
        {
            return _context.Requests
                           .Where(r => r.RequestTypeId == RequestTypeId)
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
