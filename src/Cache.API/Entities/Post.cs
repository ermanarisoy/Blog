namespace Cache.API.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Content { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
