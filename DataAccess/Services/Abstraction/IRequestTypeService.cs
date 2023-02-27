using CICWebApi.Entities;

namespace DataAccess.Services.Abstraction
{
    public interface IRequestTypeService
    {
        ICollection<RequestType> GetAll();
        RequestType GetById(int id);
        ICollection<Request> GetRequestsByRequestType(int RequestTypeId);
        bool isExist(int id);
        bool Create(RequestType RequestType);
        bool Update(RequestType RequestType);
        bool Delete(RequestType RequestType);
    }
}
