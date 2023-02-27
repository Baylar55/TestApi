using CICWebApi.Entities;

namespace DataAccess.Services.Abstraction
{
    public interface ICategoryService
    {
        ICollection<Category> GetAll();
        Category GetById(int id);
        ICollection<Request> GetRequestsByCategory(int categoryId);
        bool isExist(int id);
        bool Create(Category category);
        bool Update(Category category);
        bool Delete(Category category);
    }
}
