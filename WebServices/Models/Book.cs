using System.Runtime.Serialization;

namespace WebServices.Models
{
    [DataContract]
    public class Book
    {
        public Book(long isbn, string title, string author)
        {
            Isbn = isbn;
            Title = title;
            Author = author;
        }

        [DataMember]
        public long Isbn { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Author { get; set; }
    }
}