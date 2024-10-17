using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Singletons;
using Monogame_Project1.Engine.UIObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.Scenes;

public class Settings : Scene
{
    public override void LoadContent(ContentManager pContent)
    {
        objects.Add(new SoundSliderUI(pContent.Load<Texture2D>("TestSprite"), "SFX")
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.4f, game.GraphicsDevice.Viewport.Height * 0.3f)
        });
        objects.Add(new MusicSliderUI(pContent.Load<Texture2D>("TestSprite"), "Music")
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.4f, game.GraphicsDevice.Viewport.Height * 0.5f)
        });
        objects.Add(new SwitchSceneButton(game.Content.Load<Texture2D>("UI_Tile_128x64"), "Back", SceneManager.Instance.GetScene<MainMenu>())
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width * 0.5f, game.GraphicsDevice.Viewport.Height * 0.8f)
        });

        base.LoadContent(pContent);
    }
}
