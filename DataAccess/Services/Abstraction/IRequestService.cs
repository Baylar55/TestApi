using CICWebApi.Entities;

namespace DataAccess.Services.Abstraction
{
    public interface IRequestService
    {
        ICollection<Request> GetAll();
        Request GetById(int id);
        //ICollection<Request> GetRequestsOfCategory(int categoryId);
        //ICollection<Request> GetRequestsOfPriority(int priorityId);
        //ICollection<Request> GetRequestsOfType(int requestTypeId);
        //ICollection<Request> GetRequestsOfStatus(int requestStatusId);
        bool isExist(int id);
        bool Create(Request Request);
        bool Update(Request Request);
        bool Delete(Request Request);
        bool DeleteRequests(List<Request> Requests);
    }
}
