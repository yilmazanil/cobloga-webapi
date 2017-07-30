using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cobloga.WebApi.Controllers
{
    [Route("api/notes")]
    public class TagsController : Controller
    {
        [HttpGet("{noteId}/tags")]
        public IActionResult GetTags(int noteId)
        {
            var note = NotesDataSource.Current.Notes.FirstOrDefault(n => n.Id == noteId);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note.Tags);
        }


        //[HttpGet("{noteId}/tags/{id}", Name = "GetTag")]
        //public IActionResult GetTag(int noteId, int id)
        //{
        //    var note = NotesDataSource.Current.Notes.FirstOrDefault(n => n.Id == noteId);
        //    if(note == null)
        //    {
        //        return NotFound();
        //    }

        //    var tag = note.Tags.FirstOrDefault(t => t.Id == id);

        //    if (tag == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tag);
        //}


    }
}
