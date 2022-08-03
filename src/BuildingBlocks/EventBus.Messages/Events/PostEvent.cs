using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class PostEvent
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
