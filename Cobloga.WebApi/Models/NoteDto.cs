using System;
using System.Collections.Generic;

namespace Cobloga.WebApi.Models
{
    public class NoteDto
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public ICollection<TagDto> Tags { get; set; } = new List<TagDto>();

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
