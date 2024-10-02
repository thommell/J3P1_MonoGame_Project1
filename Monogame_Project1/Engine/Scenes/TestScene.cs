using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.Scenes;

internal class TestScene : Scene
{
    public TestScene(Game1 pGame, SceneManager pManager) : base(pGame, pManager)
    {
    }

    public override void LoadContent(ContentManager pContent)
    {
     
    }
    public override void Update(GameTime pGameTime)
    {
        base.Update(pGameTime);
    }
}

