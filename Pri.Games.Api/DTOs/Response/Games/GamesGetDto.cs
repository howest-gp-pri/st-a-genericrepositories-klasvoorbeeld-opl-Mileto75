namespace Pri.Games.Api.DTOs.Response.Games
{
    public class GamesGetDto : BaseDto
    {
        //models the info of one game
        public string Description { get; set; }
        public BaseDto Publisher { get; set; }
        public IEnumerable<BaseDto> Genres { get; set; }
        public string ImageUrl { get; set; }
    }
}
