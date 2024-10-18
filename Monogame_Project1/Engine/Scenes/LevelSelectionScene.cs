using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Monogame_Project1.Engine.Singletons;


namespace Monogame_Project1.Engine.Scenes;

public class LevelSelectionScene : Scene
{
    public override void LoadContent(ContentManager pContent)
    {
        objects.Add(new SelectionScreenButton(pContent.Load<Texture2D>("UI_Tile_64x64"), "Level 1", SceneManager.Instance.GetScene<Level1>(), pContent.Load<Texture2D>("Lock"))
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.1f, game.GraphicsDevice.Viewport.Height * 0.4f)
        });
        objects.Add(new SelectionScreenButton(pContent.Load<Texture2D>("UI_Tile_64x64"), "Level 2", SceneManager.Instance.GetScene<Level2>(), pContent.Load<Texture2D>("Lock"), true)
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.3f, game.GraphicsDevice.Viewport.Height * 0.4f)
        });
        objects.Add(new SelectionScreenButton(pContent.Load<Texture2D>("UI_Tile_64x64"), "Level 3", SceneManager.Instance.GetScene<Level3>(), pContent.Load<Texture2D>("Lock"), true)
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.5f, game.GraphicsDevice.Viewport.Height * 0.4f)
        });
        objects.Add(new SelectionScreenButton(pContent.Load<Texture2D>("UI_Tile_64x64"), "Level 4", SceneManager.Instance.GetScene<Level4>(), pContent.Load<Texture2D>("Lock"), true)
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.7f, game.GraphicsDevice.Viewport.Height * 0.4f)
        });
        objects.Add(new SelectionScreenButton(pContent.Load<Texture2D>("UI_Tile_64x64"), "Level 5", SceneManager.Instance.GetScene<Level5>(), pContent.Load<Texture2D>("Lock"), true)
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.9f, game.GraphicsDevice.Viewport.Height * 0.4f)
        });
        objects.Add(new SwitchSceneButton(pContent.Load<Texture2D>("UI_Tile_64x64"), "Menu", SceneManager.Instance.GetScene<MainMenu>())
        {
            Position = new Vector2(50, 50)
        });
    }
}