using System.ComponentModel.DataAnnotations;

namespace Pri.Games.Api.DTOs.Request.Games
{
    public class GamesCreateDto
    {
        [Required(ErrorMessage = "Please provide a title!")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Please provide a publisher")]
        public int PublisherId { get; set; }
        [Required(ErrorMessage = "Please provide a genre!")]
        public IEnumerable<int> Genres { get; set; }
        public IFormFile Image { get; set; }
    }
}
