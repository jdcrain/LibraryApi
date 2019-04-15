using System;
using System.Collections.Generic;
using JsonApiDotNetCore.Models;

namespace LibraryApi.Models
{
    public class Book : BelongsToUser
    {
        [Attr("isbn")] public string Isbn { get; set; }
        [Attr("title")] public string Title { get; set; }
        [Attr("publish-date")] public DateTime PublishDate { get; set; }
        public int AuthorId { get; set; }
        [HasOne("author")] public Author Author { get; set; }
        [HasMany("reviews")] public List<Review> Reviews { get; set; }
    }
}