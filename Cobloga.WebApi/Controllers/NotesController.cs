using Cobloga.WebApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cobloga.WebApi.Controllers
{
    [Route("api/notes")]
    public class NotesController : Controller
    {
        [HttpGet()]
        public IActionResult GetNotes()
        {
            return Ok(NotesDataSource.Current.Notes);
        }

        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult Get(int id)
        {
            // find city
            var noteToReturn = NotesDataSource.Current.Notes.FirstOrDefault(n => n.Id == id);
            if (noteToReturn == null)
            {
                return NotFound();
            }

            return Ok(noteToReturn);
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

            //Tags must be improved
            var noteToAdd = new NoteDto
            {
                Id = ++noteId,
                Subject = note.Subject,
                Body = note.Body,
                CreatedDate = DateTime.Now,
                Tags = note.Tags
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

            noteToUpdate.Subject = note.Subject;
            noteToUpdate.Body = note.Body;
            noteToUpdate.Tags = note.Tags;
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
