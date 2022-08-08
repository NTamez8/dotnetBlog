using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace backend.Models
{
    public class BlogPost
    {

        public int Id {get; set;}
        public string Title {get; set;}

        public string ShortText {get; set;}

        [Column(TypeName="Date")]
        public DateTime DateCreated {get; set;}

        [MaxLength]
        [Column(TypeName ="ntext")]
        public string Body{get; set;}

    }

    public class BlogPostDto
    {
        public string Title {get; set;}

        public string ShortText {get; set;}

        public string Body{get; set;}
    }
}