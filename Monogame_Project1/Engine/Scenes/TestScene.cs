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
    private ShootingSystem _shootingSystem = new ShootingSystem();
    public TestScene(Game1 pGame, SceneManager pManager) : base(pGame, pManager)
    {
    }

    public override void LoadContent(ContentManager pContent)
    {
        objects.Add(new Target(pContent.Load<Texture2D>("TestSprite"), new Vector2(100,100), _shootingSystem));
    }
}

