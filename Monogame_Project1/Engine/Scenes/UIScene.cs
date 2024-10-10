using System;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.UIObjects;

namespace Monogame_Project1.Engine.Scenes;

public class UIScene : Scene
{
    public UIScene(Game1 pGame, SceneManager pManager) : base(pGame, pManager) {}
    public override void LoadContent(ContentManager pContent)
    {
        UIObject ammoCounter = new AmmoUI(game.Content.Load<Texture2D>("TNT"))
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2),
        };
        uiObjects.Add(ammoCounter);
    
        base.LoadContent(pContent);
    }
}