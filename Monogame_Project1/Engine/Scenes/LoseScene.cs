using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;

namespace Monogame_Project1.Engine.Scenes;

public class LoseScene : Scene
{
    private QuitButton _quitButton;
    private RestartButton _restartButton;
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
        base.LoadContent(pContent);
    }
}