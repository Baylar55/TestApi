using CICWebApi.Entities;
using Data.Entities;

namespace CICWebApi.DTOs
{
    public class RequestDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileData { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public string Category { get; set; }
        //public int CategoryId { get; set; }

        public string CreatorUser { get; set; }
        //public int CreatorUserId { get; set; }

        public string ExecutorUser { get; set; }
        //public int ExecutorUserId { get; set; }

        public string Priority { get; set; }
        //public int PriorityId { get; set; }

        public string RequestStatus { get; set; }
        //public int RequestStatusId { get; set; }

        public string RequestType { get; set; }
        //public int RequestTypeId { get; set; }
    }
}
