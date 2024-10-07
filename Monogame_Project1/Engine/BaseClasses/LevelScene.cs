using System;
using Monogame_Project1.Engine.Scenes;

namespace Monogame_Project1.Engine.BaseClasses;

public class LevelScene : Scene
{
    private PauseSystem _pauseSystem;
    public LevelScene(Game1 pGame, SceneManager pManager) : base(pGame, pManager) {}
    public override void LoadContent(ContentManager pContent)
    {
        objects.Add(new PauseSystem(pContent.Load<SpriteFont>("Font"), pContent.Load<Texture2D>("Pixel"), manager));
        base.LoadContent(pContent);
    }
    public override void LateLoad()
    {
        _pauseSystem = GetObject<PauseSystem>();
        base.LateLoad();
    }
    public override void Update(GameTime pGameTime)
    {
        if (!_pauseSystem.IsPaused) base.Update(pGameTime);
        _pauseSystem.UpdateState();
    }
}