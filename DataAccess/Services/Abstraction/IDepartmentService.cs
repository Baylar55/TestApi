using Data.Entities;

namespace DataAccess.Services.Abstraction
{
    public interface IDepartmentService
    {
        ICollection<Department> GetAll();
        Department GetById(int id);
        ICollection<User> GetUsersByDepartment(int DepartmentId);
        bool isExist(int id);
        bool Create(Department Department);
        bool Update(Department Department);
        bool Delete(Department Department);
    }
}
