using System;
using JsonApiDotNetCore.Models;

namespace LibraryApi.Models
{
    public class Book : Identifiable
    {
        [Attr("isbn")] public string Isbn { get; set; }
        [Attr("title")] public string Title { get; set; }
        [Attr("publish-date")] public DateTime PublishDate { get; set; }
        public int AuthorId { get; set; }
        [HasOne("author")] public Author Author { get; set; }
    }
}