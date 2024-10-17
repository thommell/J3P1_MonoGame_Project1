using System;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Scenes;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.BaseClasses;

public class LevelScene : Scene
{
    private PauseSystem _pauseSystem;
    public PauseSystem PauseSystem => _pauseSystem;
    private Rectangle bottomBorder;
    public override void LoadContent(ContentManager pContent)
    {
        objects.Add(new PauseSystem(pContent.Load<SpriteFont>("Font"), pContent.Load<Texture2D>("Pixel")));
        objects.Add(new AnimationsPlayer());
        objects.Add(new TimeSystem(3f, GetObject<SpawningSystem>(), GetObject<Timer>(), font));
        base.LoadContent(pContent);
    }
    public override void LateLoad()
    {
        bottomBorder = new Rectangle(0, 1080 - 167, 1920, 167);
        _pauseSystem = GetObject<PauseSystem>();
        base.LateLoad();
    }
    public override void Update(GameTime pGameTime)
    {
        if (!_pauseSystem.IsPaused) 
            base.Update(pGameTime);
        _pauseSystem.Update(pGameTime);
    }

    public override void Draw(SpriteBatch pSpriteBatch)
    {
        pSpriteBatch.Draw(SceneManager.Instance.Game.Content.Load<Texture2D>("Background"), new Vector2(0, 0), Color.White);
        pSpriteBatch.Draw(SceneManager.Instance.Game.Content.Load<Texture2D>("Pixel"), bottomBorder, Color.Gray * 0f);
        base.Draw(pSpriteBatch);
    }
}