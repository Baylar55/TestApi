using Data.DataContext;
using Data.Entities;
using DataAccess.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;

        public DepartmentService(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public ICollection<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return _context.Departments.FirstOrDefault(c => c.Id == id);
        }

        public bool isExist(int id)
        {
            var Department = _context.Departments.Any(c => c.Id == id);
            if (Department == null) return false;
            return true;
        }

        public bool Create(Department Department)
        {
            _context.Add(Department);
            _context.SaveChanges();
            return true;
        }

        public bool Update(Department Department)
        {
            _context.Update(Department);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Department Department)
        {
            _context.Remove(Department);
            _context.SaveChanges();
            return true;
        }

        public ICollection<User> GetUsersByDepartment(int departmentId)
        {
            return _context.Users
                           .Where(u => u.DepartmentId == departmentId)
                           .Include(u => u.Department)
                           .ToList();
        }
    }
}
