using Godot;

public partial class GameManager : Node
{
    [Export]
    private CanvasLayer gameOverCanvasLayer;

    [Export]
    private CanvasLayer youWonCanvasLayer;

    public void GameOver()
    {
        gameOverCanvasLayer.Visible = true;
    }

    public void YouWon()
    {
        youWonCanvasLayer.Visible = true;
    }
}
