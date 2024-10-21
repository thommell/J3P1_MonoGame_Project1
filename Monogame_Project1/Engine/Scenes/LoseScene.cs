using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.Scenes;

public class LoseScene : Scene
{
    private QuitButton _quitButton;
    private RestartButton _restartButton;
    private SwitchSceneButton _levelSelectButton;
    private Vector2 _loseTextBounds;
    private Vector2 _scoreTextBounds;
    private const string LoseText = "You've lost.";
    public override void LoadContent(ContentManager pContent)
    {
        _quitButton = new QuitButton(game.Content.Load<Texture2D>("UI_Tile_128x64"), "Quit")
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.5f, game.GraphicsDevice.Viewport.Height * 0.7f)
        };
        _restartButton = new RestartButton(game.Content.Load<Texture2D>("UI_Tile_128x64"), "Restart")
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.5f, game.GraphicsDevice.Viewport.Height * 0.5f)
        };
        _levelSelectButton = new SwitchSceneButton(game.Content.Load<Texture2D>("UI_Tile_128x64"), "Level Select", SceneManager.Instance.GetScene<LevelSelectionScene>())
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.5f, game.GraphicsDevice.Viewport.Height * 0.3f)
        };        
        objects.Add(_quitButton);
        objects.Add(_restartButton);
        objects.Add(_levelSelectButton);
        
        _loseTextBounds = font.MeasureString(LoseText);
        _scoreTextBounds = font.MeasureString($"Your score was {SceneManager.Instance.ScoringSystem.CurrentScore}");
        
        base.LoadContent(pContent);
    }
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        DrawText(pSpriteBatch);
        base.Draw(pSpriteBatch);
    }
    private void DrawText(SpriteBatch pSpriteBatch)
    {
        pSpriteBatch.DrawString(font, $"Your score was: {SceneManager.Instance.ScoringSystem.CurrentScore.ToString()}", new Vector2(game.GraphicsDevice.Viewport.Width / 2 - _scoreTextBounds.X * 0.5f, game.GraphicsDevice.Viewport.Height / 8), Color.White);
        pSpriteBatch.DrawString(font, "You've lost.", new Vector2(game.GraphicsDevice.Viewport.Width / 2 - _loseTextBounds.X * 0.5f, game.GraphicsDevice.Viewport.Height / 1.25f), Color.White);
    }
}