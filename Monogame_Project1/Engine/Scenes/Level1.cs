using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.UIObjects;

namespace Monogame_Project1.Engine.Scenes;

public class Level1 : LevelScene
{
    public Level1(Game1 pGame, SceneManager pManager) : base(pGame, pManager)
    { }

    public override void LoadContent(ContentManager pContent)
    {
        objects.Add(new SpawningSystem(this, game, manager, 5, 5));
        objects.Add(new ShootingSystem(this));
        objects.Add(manager.ScoringSystem);
        objects.Add(new AmmoSystem(3));
        objects.Add(new Timer(game, manager, 10f));
        UIObject scoreUi = new ScoreUI(pContent.Load<Texture2D>("BrokenTarget"), game, this)
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width - 370, 30)           
        };

        UIObject ammoUi = new AmmoUI(pContent.Load<Texture2D>("Bullet"), pContent.Load<Texture2D>("UsedBullet"), this, game)
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width - 380, game.GraphicsDevice.Viewport.Height - 150),
        };

        uiObjects.Add(new CrosshairUI(pContent.Load<Texture2D>("FixedCrosshair"), game, Color.Black));

        uiObjects.Add(ammoUi);
        uiObjects.Add(scoreUi);
        base.LoadContent(pContent);
    }
}