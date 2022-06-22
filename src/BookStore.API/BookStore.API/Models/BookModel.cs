using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please add title property.")]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}