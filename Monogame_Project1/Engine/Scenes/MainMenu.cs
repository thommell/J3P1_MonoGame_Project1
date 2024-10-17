using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Singletons;
using Monogame_Project1.Engine.UIObjects;

namespace Monogame_Project1.Engine.Scenes;

public class MainMenu : Scene
{
    private QuitButton _quitButton;
    public override void LoadContent(ContentManager pContent)
    {
        objects.Add(_quitButton = new(game.Content.Load<Texture2D>("UI_Tile_128x64"), "Quit")
        {
          Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.5f, game.GraphicsDevice.Viewport.Height * 0.7f)
        });
        objects.Add(new SwitchSceneButton(game.Content.Load<Texture2D>("UI_Tile_128x64"), "Level Select",
            SceneManager.Instance.GetScene<LevelSelectionScene>())
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.5f, game.GraphicsDevice.Viewport.Height * 0.5f)
        });
        objects.Add(new SwitchSceneButton(game.Content.Load<Texture2D>("UI_Tile_128x64"), "Play", SceneManager.Instance.GetScene<Level1>())
        {
            Position = new Vector2(_quitButton.Position.X, game.GraphicsDevice.Viewport.Height * 0.3f)
        });

        objects.Add(new SoundSliderUI(pContent.Load<Texture2D>("TestSprite"))
        {
            Position = new Vector2(100, 100)
        });

        base.LoadContent(pContent);
    }
}