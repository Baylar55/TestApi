namespace CICWebApi.Entities
{
    public class RequestType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}
