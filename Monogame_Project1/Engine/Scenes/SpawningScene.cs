using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.UIObjects;

namespace Monogame_Project1.Engine.Scenes;

public class SpawningScene : LevelScene
{
    private ScoringSystem _scoreSystem;
    public SpawningScene(Game1 pGame, SceneManager pManager) : base(pGame, pManager) {}
    public override void LoadContent(ContentManager pContent)
    {
        objects.Add(new SpawningSystem(this, game, manager, 5, 5));
        objects.Add(new ShootingSystem(this));
        objects.Add(manager.ScoringSystem);
        objects.Add(new AmmoSystem(3));
        objects.Add(new Timer(game, manager, 10f));
        objects.Add(new AnimationsPlayer());
        UIObject scoreUI = new ScoreUI(pContent.Load<Texture2D>("BrokenTarget"), game, this)
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width - 370, 30)           
        };

        UIObject ammoUI = new AmmoUI(pContent.Load<Texture2D>("Bullet"), pContent.Load<Texture2D>("UsedBullet"), this, game)
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width - 380, game.GraphicsDevice.Viewport.Height - 150),
        };

        uiObjects.Add(new CrosshairUI(pContent.Load<Texture2D>("FixedCrosshair"), game, Color.Black));

        uiObjects.Add(ammoUI);
        uiObjects.Add(scoreUI);
        base.LoadContent(pContent);
    }
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        base.Draw(pSpriteBatch);
        pSpriteBatch.DrawString(
            game.Content.Load<SpriteFont>("Font"),
            "Press space to spawn objects!",
            new Vector2(game.GraphicsDevice.Viewport.Height * 0.5f + 300f,
                game.GraphicsDevice.Viewport.Height * 0.9f),
            Color.White);
        pSpriteBatch.DrawString(
            game.Content.Load<SpriteFont>("Font"),
            manager.ScoringSystem.CurrentScore.ToString(),
            new Vector2(64, 64), Color.White);
    }
}