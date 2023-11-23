using GameApplication.Entities;
using GameApplication.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameApplication.Controllers;

[Route("games")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly AddDBContext _dbContext;

    public GameController(AddDBContext dbcontext)
    {
        _dbContext = dbcontext;
    }
    
    /*private static readonly List<Game> _list = new()
    {
        Game.Create("Rain World", "Videocult", "Akupara Games", "28-03-2017"),
        Game.Create("Zuma Deluxe", "PopCap Games", "Electronic Arts", "30/8/2006")
    };*/
    
    [HttpGet(Name = "GetAllGames")]
    public ActionResult GetGames()
    {
        var games = _dbContext.Set<Game>().ToList();
        _dbContext.SaveChanges();
        return Ok(games);
    }
    
    [HttpGet( "{Id}")]
    public ActionResult GetGames(string Id)
    {
        //var _list = _dbContext.Set<Game>().ToList();
        //var game = _list.FirstOrDefault(s => s.Id == Id);
        
        var game = _dbContext.Games.Where(games => games.Id == Id).FirstOrDefault();
        if (game is null)
            return NotFound($"Game with id: {Id} does not exist");
        _dbContext.SaveChanges();
        return Ok(game);
    }
    
    [HttpPost]
    public ActionResult CreateGame([FromBody] GameRequest gameRequest)
    {
        Game game = null;
        try
        {
            game = Game.Create(gameRequest.Title, gameRequest.Developer, gameRequest.Editor, gameRequest.Date);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        var _list = _dbContext.Set<Game>().ToList();
        _list.Add(game);
        _dbContext.Add(game);
        _dbContext.SaveChanges();
        return Ok(gameRequest);
    }
    
    [HttpDelete("{Id}")]
    public ActionResult RemoveGame(string Id)
    {
        //var _list = _dbContext.Set<Game>().ToList();
        
        //var game = _list.FirstOrDefault(s => s.Id == Id);
        var game = _dbContext.Games.FirstOrDefault();

        if (game is null)
            return NotFound($"Game with id: {Id} does not exist");

        _dbContext.Remove(game);
        _dbContext.SaveChanges();

        return Ok($"Game with id: {Id} was removed");
    }
    
    [HttpPatch("{Id} Update Game Title")]
    public ActionResult UpdateGameTitle(string Id, [FromBody] string title)
    {
        //var _list = _dbContext.Set<Game>().ToList();
        var game = _dbContext.Games.FirstOrDefault(s => s.Id == Id);

        if (game is null)
            return NotFound($"Game with id: {Id} does not exist");

        try
        {
            game.SetTitleName(title);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        _dbContext.SaveChanges();
        return Ok(game);
    }
    
    [HttpPut("{Id} Update")]
    public ActionResult UpdateGame(string Id, [FromBody]GameRequest gameRequest)
    {
        //var _list = _dbContext.Set<Game>().ToList();
        var game = _dbContext.Games.FirstOrDefault(s => s.Id == Id);

        if (game is null)
            return NotFound($"Game with id: {Id} does not exist");

        try
        {
            game.SetTitleName(gameRequest.Title);
            game.SetDeveloperName(gameRequest.Developer);
            game.SetEditorName(gameRequest.Editor);
            game.SetDate(gameRequest.Date);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        _dbContext.SaveChanges();
        return Ok(game);
    }
    
    [HttpPatch("{Id} Add Positive Review")]
    public ActionResult AddPositiveReview(string Id, [FromBody] string review)
    {
        //var _list = _dbContext.Set<Game>().ToList();
        var game = _dbContext.Games.FirstOrDefault(s => s.Id == Id);

        if (game is null)
            return NotFound($"Game with id: {Id} does not exist");

        try
        {
            game.AddPositiveReview(review);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        _dbContext.SaveChanges();
        return Ok(game);
    }
    
    [HttpPatch("{Id} Add Negative Review")]
    public ActionResult AddNegativeReview(string Id, [FromBody] string review)
    {
        var _list = _dbContext.Set<Game>().ToList();
        var game = _dbContext.Games.FirstOrDefault(s => s.Id == Id);

        if (game is null)
            return NotFound($"Game with id: {Id} does not exist");

        try
        {
            game.AddNegativeReview(review);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        _dbContext.SaveChanges();
        return Ok(game);
    }
    
    [HttpGet("{Id} GetAllReviews")]
    public ActionResult GetReviews(string Id)
    {
        //var _list = _dbContext.Set<Game>().ToList();
        var game = _dbContext.Games.FirstOrDefault(s => s.Id == Id);

        if (game is null)
            return NotFound($"Game with id: {Id} does not exist");
        _dbContext.SaveChanges();
        return Ok("Positive Reviews(" + game.PositiveReviews +  "): " + String.Join(", ", game.PositiveReviewsList) + "\n" + "Negative Reviews(" + game.NegativeReviews + "):" + String.Join(", ", game.NegativeReviewsList) + "\n");
    }
}