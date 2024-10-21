using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Singletons;
using Monogame_Project1.Engine.UIObjects;

namespace Monogame_Project1.Engine.Scenes;

public class MainMenu : Scene
{
    private const string TitleText = "Sky Strike";
    private Vector2 _titleBounds = new();
    private QuitButton _quitButton;
    public override void LoadContent(ContentManager pContent)
    {
        _titleBounds = game.Content.Load<SpriteFont>("TitleFont").MeasureString(TitleText);
        AudioManager.Instance.PlayMusic("Menu", true);

        objects.Add(_quitButton = new(game.Content.Load<Texture2D>("UI_Tile_128x64"), "Quit")
        {
          Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.5f, game.GraphicsDevice.Viewport.Height * 0.65f)
        });
        objects.Add(new SwitchSceneButton(game.Content.Load<Texture2D>("UI_Tile_128x64"), "Level Select",
            SceneManager.Instance.GetScene<LevelSelectionScene>())
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.5f, game.GraphicsDevice.Viewport.Height * 0.35f)
        });
        // objects.Add(new SwitchSceneButton(game.Content.Load<Texture2D>("UI_Tile_128x64"), "Play", SceneManager.Instance.AssignPlayButton())
        // {
        //     Position = new Vector2(_quitButton.Position.X, game.GraphicsDevice.Viewport.Height * 0.3f)
        // });
        objects.Add(new SwitchSceneButton(game.Content.Load<Texture2D>("UI_Tile_128x64"), "Settings", SceneManager.Instance.GetScene<Settings>())
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.5f, game.GraphicsDevice.Viewport.Height * 0.5f)
        });
        base.LoadContent(pContent);
    }

    public override void Draw(SpriteBatch pSpriteBatch)
    {
        pSpriteBatch.DrawString(game.Content.Load<SpriteFont>("TitleFont"), TitleText, new Vector2(game.GraphicsDevice.Viewport.Width * 0.5f - _titleBounds.X * 0.5f, game.GraphicsDevice.Viewport.Height * 0.25f - _titleBounds.Y * 0.5f), Color.White);
        base.Draw(pSpriteBatch);
    }
}