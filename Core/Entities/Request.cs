using Data.Entities;

namespace CICWebApi.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileData { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public User CreatorUser { get; set; }
        public int CreatorUserId { get; set; }

        public User ExecutorUser { get; set; }
        public int ExecutorUserId { get; set; }

        public Priority Priority { get; set; }
        public int PriorityId { get; set; }
        
        //public Department Department { get; set; }
        //public int DepartmentId { get; set; }

        public RequestStatus RequestStatus { get; set; }
        public int RequestStatusId { get; set; }

        public RequestType RequestType { get; set; }
        public int RequestTypeId { get; set; }
    }
}
