using System.Diagnostics;
using System.Runtime.InteropServices;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Enums;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Singletons;
using Monogame_Project1.Engine.UIObjects;

namespace Monogame_Project1.Engine.Scenes;

public class WinScene : Scene
{
    private SwitchSceneButton _menuButton;
    private SwitchSceneButton _levelSelectButton;
    private QuitButton _quitButton;
    private string _winText;
    private Vector2 _winTextBounds;
    private ScoringSystem _scoreSystem;
    public override void LoadContent(ContentManager pContent)
    {
        _quitButton = new QuitButton(game.Content.Load<Texture2D>("UI_Tile_128x64"), "Quit")
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.5f, game.GraphicsDevice.Viewport.Height * 0.6f)
        };
        _levelSelectButton = new SwitchSceneButton(game.Content.Load<Texture2D>("UI_Tile_128x64"), "Level Select", SceneManager.Instance.GetScene<LevelSelectionScene>())
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.5f, game.GraphicsDevice.Viewport.Height * 0.4f)
        };
        objects.Add(_quitButton);
        objects.Add(_levelSelectButton);
        _winText = "Congratulations, you've beaten the level!";
        font = game.Content.Load<SpriteFont>("UIText");
        _winTextBounds = font.MeasureString(_winText);
        if (SceneManager.Instance.pastLevelScene != null)
            ResultHandler.Instance.SetResult(SceneManager.Instance.pastLevelScene, Results.Win);
        base.LoadContent(pContent);
    }

    public override void Draw(SpriteBatch pSpriteBatch)
    {
        DrawText(pSpriteBatch);
        base.Draw(pSpriteBatch);
    }
    private void DrawText(SpriteBatch pSpriteBatch)
    {
        pSpriteBatch.DrawString(font, $"Your score is: {SceneManager.Instance.ScoringSystem.CurrentScore.ToString()}", new Vector2(game.GraphicsDevice.Viewport.Width * 0.5f - _winTextBounds.X * 0.25f + 50, game.GraphicsDevice.Viewport.Height * 0.2f), Color.White);
        pSpriteBatch.DrawString(font, _winText, new Vector2(game.GraphicsDevice.Viewport.Width / 2 - _winTextBounds.X * 0.5f, game.GraphicsDevice.Viewport.Height / 1.5f), Color.White);
    }
}