using CICWebApi.Entities;
using Data.Entities;

namespace DataAccess.Services.Abstraction
{
    public interface IRequestStatusService
    {
        ICollection<RequestStatus> GetAll();
        RequestStatus GetById(int id);
        ICollection<Request> GetRequestsByRequestStatus(int RequestStatusId);
        bool isExist(int id);
        bool Create(RequestStatus RequestStatus);
        bool Update(RequestStatus RequestStatus);
        bool Delete(RequestStatus RequestStatus);
    }
}
