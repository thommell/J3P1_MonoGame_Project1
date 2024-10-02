using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;

namespace Monogame_Project1.Engine.Scenes;

public class MainMenu : Scene
{
    private QuitButton _quitButton;
    public MainMenu(Game1 pGame, SceneManager pManager) : base(pGame, pManager) { }
    public override void LoadContent(ContentManager pContent)
    {
        objects.Add(_quitButton = new(game, manager, game.Content.Load<Texture2D>("UI_Tile_128x64"), "Quit")
        {
          Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.5f, game.GraphicsDevice.Viewport.Height * 0.5f + 64)
        });
        objects.Add(new PlayButton(game, manager, game.Content.Load<Texture2D>("UI_Tile_128x64"), "Play")
        {
            Position = new Vector2(_quitButton.Position.X, _quitButton.Position.Y - _quitButton.Texture.Height * 1.5f)
        });
    }
}