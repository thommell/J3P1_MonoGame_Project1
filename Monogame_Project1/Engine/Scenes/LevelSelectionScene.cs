using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Monogame_Project1.Engine.Scenes;

public class LevelSelectionScene : Scene
{
    public LevelSelectionScene(Game1 pGame, SceneManager pManager) : base(pGame, pManager)
    {
    }
    public override void LoadContent(ContentManager pContent)
    {
        objects.Add(new SelectionScreenButton(game, manager, pContent.Load<Texture2D>("UI_Tile_64x64"), "Level 1", manager.GetScene<Level1>(), pContent.Load<Texture2D>("Lock"))
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.1f, game.GraphicsDevice.Viewport.Height * 0.4f)
        });
        objects.Add(new SelectionScreenButton(game, manager, pContent.Load<Texture2D>("UI_Tile_64x64"), "Level 2", manager.GetScene<Level2>(), pContent.Load<Texture2D>("Lock"), true)
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.3f, game.GraphicsDevice.Viewport.Height * 0.4f)
        });
        objects.Add(new SelectionScreenButton(game, manager, pContent.Load<Texture2D>("UI_Tile_64x64"), "Level 3", manager.GetScene<Level3>(), pContent.Load<Texture2D>("Lock"), true)
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.5f, game.GraphicsDevice.Viewport.Height * 0.4f)
        });
        objects.Add(new SelectionScreenButton(game, manager, pContent.Load<Texture2D>("UI_Tile_64x64"), "Level 4", manager.GetScene<Level4>(), pContent.Load<Texture2D>("Lock"), true)
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.7f, game.GraphicsDevice.Viewport.Height * 0.4f)
        });
        objects.Add(new SelectionScreenButton(game, manager, pContent.Load<Texture2D>("UI_Tile_64x64"), "Level 5", manager.GetScene<Level5>(), pContent.Load<Texture2D>("Lock"), true)
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.9f, game.GraphicsDevice.Viewport.Height * 0.4f)
        });

        //Main Menu Button
        objects.Add(new SwitchSceneButton(game, manager, pContent.Load<Texture2D>("UI_Tile_64x64"), "Menu", manager.GetScene<MainMenu>())
        {
            Position = new Vector2(50, 50)
        });
    }
}