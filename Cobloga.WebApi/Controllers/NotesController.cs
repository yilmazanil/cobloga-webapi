using Cobloga.WebApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cobloga.WebApi.Controllers
{
    [Route("api/notes")]
    public class NotesController : Controller
    {
        private ILogger<NotesController> _logger;

        public NotesController(ILogger<NotesController> logger)
        {
            _logger = logger;
            //Constructor Injection or get by request services HttpContext.RequestServices.GetService();
        }

        [HttpGet()]
        public IActionResult GetNotes()
        {
            return Ok(NotesDataSource.Current.Notes);
        }

        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult Get(int id)
        {
            try
            {
                // find city
                var noteToReturn = NotesDataSource.Current.Notes.FirstOrDefault(n => n.Id == id);
                if (noteToReturn == null)
                {
                    _logger.LogInformation($"Note with id {id} was not found");
                    return NotFound();
                }

                return Ok(noteToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting city with id {id}", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] NoteForCreationDto note)
        {
            if (note == null)
            {
                return BadRequest();
            }

            if (string.Equals(note.Body, note.Subject, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Body", "The provided body should be different from the subject.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // demo purposes - to be improved
            var noteId = NotesDataSource.Current.Notes.Max(n => n.Id);

            var tags = new List<TagDto>();


            //Tag source will be seperated, for test purpose only
            var currentTags = NotesDataSource.Current.Notes.SelectMany(
                 n => n.Tags
             );
            var maxTagId = currentTags.Max(t => t.Id);
            foreach (var tag in note.Tags)
            {

                var existingTag = currentTags.FirstOrDefault(t => t.Name == tag.Name);

                if (existingTag != null)
                {
                    tags.Add(existingTag);
                }
                else
                {
                    tags.Add(new TagDto { Id = ++maxTagId, Name = tag.Name });
                }
            }
            //Tags must be improved
            var noteToAdd = new NoteDto
            {
                Id = ++noteId,
                Subject = note.Subject,
                Body = note.Body,
                CreatedDate = DateTime.Now,
                Tags = tags
            };
            NotesDataSource.Current.Notes.Add(noteToAdd);

            return CreatedAtRoute("GetNote", new
            { id = noteToAdd.Id }, noteToAdd);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(int id,
            [FromBody] NoteForUpdateDto note)
        {
            if (note == null)
            {
                return BadRequest();
            }

            if (string.Equals(note.Body, note.Subject, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Body", "The provided body should be different from the subject.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var noteToUpdate = NotesDataSource.Current.Notes.FirstOrDefault(n => n.Id == id);

            if (noteToUpdate == null)
            {
                return NotFound();
            }

            var tags = new List<TagDto>();
            var currentTags = NotesDataSource.Current.Notes.SelectMany(
                 n => n.Tags
             );
            var maxTagId = currentTags.Max(t => t.Id);
            foreach (var tag in note.Tags)
            {

                var existingTag = currentTags.FirstOrDefault(t => t.Name == tag.Name);

                if (existingTag != null)
                {
                    tags.Add(existingTag);
                }
                else
                {
                    tags.Add(new TagDto { Id = ++maxTagId, Name = tag.Name });
                }
            }


            noteToUpdate.Subject = note.Subject;
            noteToUpdate.Body = note.Body;
            noteToUpdate.Tags = tags;
            noteToUpdate.ModifiedDate = DateTime.Now;

            return NoContent();
        }

        //    [HttpPatch("{id}")]
        //    public IActionResult PartiallyUpdatePointOfInterest(int id,
        //[FromBody] JsonPatchDocument<NoteForUpdateDto> patchDoc)
        //    {
        //        if (patchDoc == null)
        //        {
        //            return BadRequest();
        //        }

        //        var note = NotesDataSource.Current.Notes.FirstOrDefault(n => n.Id == id);
        //        if (note == null)
        //        {
        //            return NotFound();
        //        }

        //        var noteToPatch =
        //               new NoteForUpdateDto()
        //               {
        //                   Subject = note.Subject,
        //                   Body = note.Body
        //               };

        //        patchDoc.ApplyTo(noteToPatch, ModelState);

        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        if (string.Equals(noteToPatch.Body, noteToPatch.Subject, StringComparison.OrdinalIgnoreCase))
        //        {
        //            ModelState.AddModelError("Body", "The provided body should be different from the subject.");
        //        }

        //        TryValidateModel(noteToPatch);

        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        note.Subject = note.Subject;
        //        note.Body = note.Body;

        //        return NoContent();
        //    }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            var note = NotesDataSource.Current.Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            NotesDataSource.Current.Notes.Remove(note);
            return NoContent();
        }

    }
}
