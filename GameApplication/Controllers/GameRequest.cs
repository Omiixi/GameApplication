namespace GameApplication.Controllers;

public record GameRequest(string Title, string Developer, string Editor, string Date);

public class GameRequest1
{
    public string Title { get; init; }
    public string Developer { get; init; }
    public string Editor { get; init; }
    public string Date { get; init; }
}