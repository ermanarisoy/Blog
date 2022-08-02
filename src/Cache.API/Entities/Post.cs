namespace Cache.API.Entities
{
    public class Post
    {
        public string Id { get; set; }
        public string SubjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
