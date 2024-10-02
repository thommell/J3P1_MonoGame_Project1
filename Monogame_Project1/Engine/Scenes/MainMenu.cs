using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.Scenes;

public class MainMenu : Scene
{
    public MainMenu(Game1 pGame, SceneManager pManager) : base(pGame, pManager)
    { }

    public override void LoadContent(ContentManager pContent)
    {
        objects.Add(new Button(game, manager, game.Content.Load<Texture2D>("UI_Tile_128x64"), "Quit"));
    }
}