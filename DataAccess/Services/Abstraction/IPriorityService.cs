using CICWebApi.Entities;

namespace DataAccess.Services.Abstraction
{
    public interface IPriorityService
    {
        ICollection<Priority> GetAll();
        Priority GetById(int id);
        ICollection<Request> GetRequestsByPriority(int PriorityId);
        bool isExist(int id);
        bool Create(Priority Priority);
        bool Update(Priority Priority);
        bool Delete(Priority Priority);
    }
}
