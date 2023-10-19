namespace Pri.Games.Api.DTOs.Response.Games
{
    public class GamesGetAllDto
    {
        //a list of games
        public IEnumerable<GamesGetDto> Games { get; set; }
    }
}
