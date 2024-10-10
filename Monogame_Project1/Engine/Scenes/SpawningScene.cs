using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;

namespace Monogame_Project1.Engine.Scenes;

public class SpawningScene : LevelScene
{
    private ScoringSystem _scoreSystem;
    public SpawningScene(Game1 pGame, SceneManager pManager) : base(pGame, pManager) {}
    public override void LoadContent(ContentManager pContent)
    {
        objects.Add(new SpawningSystem(this, game, manager, 5, 5));
        objects.Add(new ShootingSystem(this, 999));
        objects.Add(new ScoringSystem(this));
        objects.Add(new Timer(game, manager, 10f));

        UIObject scoreUI = new ScoreUI(pContent.Load<Texture2D>("BrokenTarget"), game, this)
        {
            Position = new Vector2(game.GraphicsDevice.Viewport.Width - 370, 30)           
        };
        uiObjects.Add(scoreUI);


        base.LoadContent(pContent);
    }
    public override void LateLoad()
    {
        _scoreSystem = GetObject<ScoringSystem>();
        base.LateLoad();
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
            _scoreSystem.CurrentScore.ToString(),
            new Vector2(64, 64), Color.White);
    }
}