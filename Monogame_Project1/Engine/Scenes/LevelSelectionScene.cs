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
        objects.Add(new SelectionScreenButton(game, manager, pContent.Load<Texture2D>("UI_Tile_64x64"), "Level 1", manager.GetScene<TestScene>()));
    }
}
