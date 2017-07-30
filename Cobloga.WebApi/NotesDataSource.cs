using Cobloga.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cobloga.WebApi
{
    public class NotesDataSource
    {
        public static NotesDataSource Current { get; } = new NotesDataSource();

        public List<NoteDto> Notes { get; set; }

        public NotesDataSource()
        {
            Notes = new List<NoteDto> {

                new NoteDto {
                    Id = 1,
                    Body = "",
                    CreatedDate = DateTime.Now,
                    Subject = "Post 1 Subject",
                    Tags = new List<TagDto> {
                        new TagDto() {
                            Id= 1,
                            Name = "c#"
                        },
                        new TagDto() {
                            Id= 2,
                            Name = "dotnet"
                        }
                    }
                },
                new NoteDto {
                    Id = 2,
                    Body = "",
                    CreatedDate = DateTime.Now,
                    Subject = "Post 2 Subject",
                    Tags = new List<TagDto> {
                        new TagDto() {
                            Id= 3,
                            Name = "programming"
                        },
                        new TagDto() {
                            Id= 4,
                            Name = "webapi"
                        }
                    }
                },
                new NoteDto {
                    Id = 3,
                    Body = "",
                    CreatedDate = DateTime.Now,
                    Subject = "Post 3 Subject",
                    Tags = new List<TagDto> {
                        new TagDto() {
                            Id= 1,
                            Name = "c#"
                        },
                        new TagDto() {
                            Id= 4,
                            Name = "webapi"
                        }
                    }
                }

            };
        }
    }
}
