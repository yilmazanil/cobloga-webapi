using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cobloga.WebApi.Models
{
    public class TagForCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

    }
}
