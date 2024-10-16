using System.Runtime.InteropServices;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.Scenes;

public class WinScene : Scene
{
    private SwitchSceneButton _nextSceneButton;
    private SwitchSceneButton _menuButton;
    private QuitButton _quitButton;
    private string _winText;
    private Vector2 _winTextBounds;
    private ScoringSystem _scoreSystem;
    public override void LoadContent(ContentManager pContent)
    {
        _nextSceneButton = new SwitchSceneButton(game.Content.Load<Texture2D>("UI_Tile_128x64"),
            "Next Level", SceneManagerSingleton.Instance.GetScene<LevelSelectionScene>())
            {
                Position = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 3),
            };
        _quitButton = new QuitButton(game.Content.Load<Texture2D>("UI_Tile_128x64"), "Quit")
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2)
        };
        objects.Add(_nextSceneButton);
        objects.Add(_quitButton);
        objects.Add(new SoundSliderUI(pContent.Load<Texture2D>("TestSprite"), manager, game)
        {
            Position = new Vector2(100, 100)
        });
        _winText = "Congratulations, you've beaten the level!";
        font = game.Content.Load<SpriteFont>("UIText");
        _winTextBounds = font.MeasureString(_winText);
        if (SceneManagerSingleton.Instance.pastLevelScene != null)
            ResultHandlerSingleton.Instance.SetResult(SceneManagerSingleton.Instance.pastLevelScene, Result.Win);
        base.LoadContent(pContent);
    }

    public override void Draw(SpriteBatch pSpriteBatch)
    {
        DrawText(pSpriteBatch);
        base.Draw(pSpriteBatch);

    }
    private void DrawText(SpriteBatch pSpriteBatch)
    {
        pSpriteBatch.DrawString(font, $"Your score is: {SceneManagerSingleton.Instance.ScoringSystem.CurrentScore.ToString()}", new Vector2(game.GraphicsDevice.Viewport.Width / 2 - _winTextBounds.X * 0.5f, game.GraphicsDevice.Viewport.Height / 3), Color.White);
        pSpriteBatch.DrawString(font, _winText, new Vector2(game.GraphicsDevice.Viewport.Width / 2 - _winTextBounds.X * 0.5f, game.GraphicsDevice.Viewport.Height / 1.5f), Color.White);
    }
}