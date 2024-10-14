using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;

namespace Monogame_Project1.Engine.Scenes;

public class LoseScene : Scene
{
    private QuitButton _quitButton;
    private RestartButton _restartButton;
    private Vector2 _loseTextBounds;
    private Vector2 _scoreTextBounds;
    private const string LoseText = "You've lost.";
    public LoseScene(Game1 pGame, SceneManager pManager) : base(pGame, pManager)
    { }

    public override void LoadContent(ContentManager pContent)
    {
        _quitButton = new QuitButton(game, manager, game.Content.Load<Texture2D>("UI_Tile_128x64"), "Quit")
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2)
        };
        _restartButton = new RestartButton(game, manager, game.Content.Load<Texture2D>("UI_Tile_128x64"), "Restart")
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 3)
        };
        
        objects.Add(_quitButton);
        objects.Add(_restartButton);
        
        _loseTextBounds = font.MeasureString(LoseText);
        _scoreTextBounds = font.MeasureString($"Your score was {manager.ScoringSystem.CurrentScore}");
        
        base.LoadContent(pContent);
    }
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        DrawText(pSpriteBatch);
        base.Draw(pSpriteBatch);
     
    }
    private void DrawText(SpriteBatch pSpriteBatch)
    {
        pSpriteBatch.DrawString(font, $"Your score was: {manager.ScoringSystem.CurrentScore.ToString()}", new Vector2(game.GraphicsDevice.Viewport.Width / 2 - _scoreTextBounds.X * 0.5f, game.GraphicsDevice.Viewport.Height / 8), Color.White);
        pSpriteBatch.DrawString(font, "You've lost.", new Vector2(game.GraphicsDevice.Viewport.Width / 2 - _loseTextBounds.X * 0.5f, game.GraphicsDevice.Viewport.Height / 1.25f), Color.White);
    }
}