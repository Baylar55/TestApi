using CICWebApi.Entities;
using Data.DataContext;
using Data.Entities;

namespace CICWebApi
{
    public class Seed
    {
        private readonly AppDbContext context;
        public Seed(AppDbContext context)
        {
            this.context = context;
        }
        public void SeedAppDbContext()
        {
            if (!context.Categories.Any())
            {
                var categories = new Category[]
                {
                new Category { Name = "Category 1", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
                new Category { Name = "Category 2", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
                new Category { Name = "Category 3", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
                new Category { Name = "Category 4", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.Departments.Any())
            {
                var departments = new Department[]
                {
                new Department { Name = "Department 1" },
                new Department { Name = "Department 2" },
                new Department { Name = "Department 3" },
                new Department { Name = "Department 4" }
                };

                context.Departments.AddRange(departments);
                context.SaveChanges();
            }

            if (!context.Priorities.Any())
            {
                var priorities = new Priority[]
                {
                new Priority { Type = "Low", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
                new Priority { Type = "Medium", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
                new Priority { Type = "High", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
                new Priority { Type = "Urgent", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now }
                };

                context.Priorities.AddRange(priorities);
                context.SaveChanges();
            }

            if (!context.RequestStatuses.Any())
            {
                var requestStatuses = new RequestStatus[]
                {
                new RequestStatus { Name = "Open", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
                new RequestStatus { Name = "In Progress", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
                new RequestStatus { Name = "Completed", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
                new RequestStatus { Name = "Closed", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now }
                };

                context.RequestStatuses.AddRange(requestStatuses);
                context.SaveChanges();
            }

            if (!context.RequestTypes.Any())
            {
                var requestTypes = new RequestType[]
                {
                new RequestType { Name = "Type 1", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
                new RequestType { Name = "Type 2", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
                new RequestType { Name = "Type 3", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
                new RequestType { Name = "Type 4", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now }
                };

                context.RequestTypes.AddRange(requestTypes);
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var users = new User[]
                {
                new User { Name = "John111", Surname = "Doe111", Email = "john.doe1111@example.com", Password = "password111", InternalNumber = "1111", ContactNumber = "555-5555", Position = "Position 1", AllowNotification = true, DepartmentId = 1 },
                new User { Name = "John222", Surname = "Doe222", Email = "john.doe2222@example.com", Password = "password222", InternalNumber = "2222", ContactNumber = "666-6666", Position = "Position 2", AllowNotification = true, DepartmentId = 2 },
                new User { Name = "John333", Surname = "Doe333", Email = "john.doe3333@example.com", Password = "password333", InternalNumber = "3333", ContactNumber = "777-7777", Position = "Position 3", AllowNotification = true, DepartmentId = 3 },
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }

            if (!context.Requests.Any())
            {
                var requests = new Request[]
                {
                new Request { Title = "Broken Printer111", Description = "The printer in the IT department is not working1111", FileData = "aaa", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now, CategoryId=1, CreatorUserId=1, ExecutorUserId=1, PriorityId=1, RequestStatusId=1, RequestTypeId=1 },
                new Request { Title = "Broken Printer222", Description = "The printer in the IT department is not working2222", FileData = "bbb", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now, CategoryId=2, CreatorUserId=1, ExecutorUserId=1, PriorityId=2, RequestStatusId=2, RequestTypeId=1 },
                new Request { Title = "Broken Printer333", Description = "The printer in the IT department is not working3333", FileData = "ccc", CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now, CategoryId=3, CreatorUserId=1, ExecutorUserId=1, PriorityId=3, RequestStatusId=3, RequestTypeId=1 },
                };
                context.Requests.AddRange();
                context.SaveChanges();
            }
        }
    }
}
