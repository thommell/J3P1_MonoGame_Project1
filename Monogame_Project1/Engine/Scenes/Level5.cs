using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Singletons;
using Monogame_Project1.Engine.UIObjects;

namespace Monogame_Project1.Engine.Scenes;

public class Level5 : LevelScene
{
    public override void LoadContent(ContentManager pContent)
    {
        objects.Add(new SpawningSystem(this, 30, 25));
        objects.Add(new ShootingSystem(this));
        objects.Add(SceneManager.Instance.ScoringSystem);
        objects.Add(new AmmoSystem(3));
        objects.Add(new Timer(10f));
        UIObject scoreUi = new ScoreUI(pContent.Load<Texture2D>("BrokenTarget"), game, this)
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width - 370, 30)           
        };

        UIObject ammoUi = new AmmoUI(pContent.Load<Texture2D>("Bullet"), pContent.Load<Texture2D>("UsedBullet"), this, game)
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width - 380, game.GraphicsDevice.Viewport.Height - 150),
        };
        uiObjects.Add(ammoUi);
        uiObjects.Add(scoreUi);
        base.LoadContent(pContent);
    }
}