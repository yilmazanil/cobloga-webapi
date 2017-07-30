using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cobloga.WebApi.Models
{
    public class NoteForUpdateDto
    {
        [Required(ErrorMessage = "Subject is not provided")]
        [MaxLength(100)]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Note Body is not provided")]
        public string Body { get; set; }

        public ICollection<TagDto> Tags { get; set; }
    }
}
