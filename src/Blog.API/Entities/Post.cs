using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.API.Entities
{
    public class Post
    {
        [Key]
        public string Id { get; set; }
        public string SubjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
