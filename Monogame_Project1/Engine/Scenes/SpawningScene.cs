using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;

namespace Monogame_Project1.Engine.Scenes;

public class SpawningScene : Scene
{
    public SpawningScene(Game1 pGame, SceneManager pManager) : base(pGame, pManager) {}
    public override void LoadContent(ContentManager pContent)
    {
        objects.Add(new SpawningSystem(this, game));
        objects.Add(new ShootingSystem(this, game, 999));
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
    }
}