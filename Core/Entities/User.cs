using CICWebApi.Entities;

namespace Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string InternalNumber { get; set; }
        public string ContactNumber { get; set; }
        public string Position { get; set; }
        public string PhotoName { get; set; }
        public bool AllowNotification { get; set; }

        public ICollection<Request> CreatedRequests { get; set; }
        public ICollection<Request> ExecutedRequests { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }

    }
}
