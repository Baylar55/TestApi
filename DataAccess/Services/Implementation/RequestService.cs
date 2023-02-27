using CICWebApi.Entities;
using Data.DataContext;
using DataAccess.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services.Implementation
{
    public class RequestService : IRequestService
    {
        private readonly AppDbContext _context;

        public RequestService(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public ICollection<Request> GetAll()
        {
            return _context.Requests
                                    .Include(r => r.Category)
                                    .Include(r => r.Priority)
                                    .Include(r => r.RequestType)
                                    .Include(r => r.RequestStatus)
                                    .ToList();
        }

        public Request GetById(int id)
        {
            return _context.Requests
                                    .Include(r => r.Category)
                                    .Include(r => r.Priority)
                                    .Include(r => r.RequestType)
                                    .Include(r => r.RequestStatus)
                                    .FirstOrDefault(c => c.Id == id);
        }

        public bool isExist(int id)
        {
            var Request = _context.Requests.Any(c => c.Id == id);
            if (Request == null) return false;
            return true;
        }

        public bool Create(Request Request)
        {
            _context.Add(Request);
            _context.SaveChanges();
            return true;
        }

        public bool Update(Request Request)
        {
            _context.Update(Request);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Request Request)
        {
            _context.Remove(Request);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteRequests(List<Request> Requests)
        {
            _context.RemoveRange(Requests);
            _context.SaveChanges();
            return true;
        }


        #region Get requests for his relation


        //public ICollection<Request> GetRequestsOfCategory(int categoryId)
        //{
        //    return _context.Requests.Where(r => r.Category.Id == categoryId).ToList();
        //}

        //public ICollection<Request> GetRequestsOfPriority(int priorityId)
        //{
        //    return _context.Requests.Where(r => r.Priority.Id == priorityId).ToList();
        //}

        //public ICollection<Request> GetRequestsOfType(int requestTypeId)
        //{
        //    return _context.Requests.Where(r => r.RequestType.Id == requestTypeId).ToList();
        //}

        //public ICollection<Request> GetRequestsOfStatus(int requestStatusId)
        //{
        //    return _context.Requests.Where(r => r.RequestStatus.Id == requestStatusId).ToList();
        //}


        #endregion
    }
}
