namespace CICWebApi.Entities
{
    public class Priority
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}
