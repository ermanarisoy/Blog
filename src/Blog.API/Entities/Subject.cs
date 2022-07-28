namespace Blog.API.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
