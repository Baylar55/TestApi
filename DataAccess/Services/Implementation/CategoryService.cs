using CICWebApi.Entities;
using Data.DataContext;
using DataAccess.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public ICollection<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public bool isExist(int id)
        {
            var category = _context.Categories.Any(c => c.Id == id);
            if (category == null) return false;
            return true;
        }

        public bool Create(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
            return true;
        }

        public bool Update(Category category)
        {
            _context.Update(category);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Category category)
        {
            _context.Remove(category);
            _context.SaveChanges();
            return true;
        }

        public ICollection<Request> GetRequestsByCategory(int categoryId)
        {
            return _context.Requests
                           .Where(r => r.CategoryId == categoryId)
                           .Include(r=>r.Category)
                           .Include(r=>r.Priority)
                           .Include(r=>r.RequestType)
                           .Include(r => r.RequestStatus)
                           .Include(r=>r.CreatorUser)
                           .Include(r=>r.ExecutorUser)
                           .ToList();
        }

    }
}
