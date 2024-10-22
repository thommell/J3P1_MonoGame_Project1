using System;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Scenes;
using Monogame_Project1.Engine.Singletons;
using Monogame_Project1.Engine.UIObjects;

namespace Monogame_Project1.Engine.BaseClasses;

public class LevelScene : Scene
{
    private PauseSystem _pauseSystem;
    public PauseSystem PauseSystem => _pauseSystem;
    private Rectangle bottomBorder;
    private CrosshairUI _crosshairUI;
    public override void LoadContent(ContentManager pContent)
    {
        _crosshairUI = new CrosshairUI(pContent.Load<Texture2D>("FixedCrosshair"), game, Color.Red);
        objects.Add(new PauseSystem(pContent.Load<SpriteFont>("Font"), pContent.Load<Texture2D>("Pixel")));
        objects.Add(new AnimationsPlayer());
        objects.Add(new TimeSystem(3f, GetObject<SpawningSystem>(), GetObject<Timer>(), font));
        AudioManager.Instance.PlayMusic("TestMusic", true);
        uiObjects.Add(_crosshairUI);
        WaveManager.Instance.Initialize(SceneManager.Instance.CurrentScene);
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
        WaveManager.Instance.Update();
        if (!_pauseSystem.IsPaused) 
            base.Update(pGameTime);
        _pauseSystem.Update(pGameTime);
    }

    public override void Draw(SpriteBatch pSpriteBatch)
    {
        var timer = GetObject<TimeSystem>();
        if (_pauseSystem.IsPaused)
        {
            timer.IsActive = false;
            _crosshairUI.IsActive = false;
        }
        else
        {
            timer.IsActive = true;
            _crosshairUI.IsActive = true;
        }
        pSpriteBatch.Draw(SceneManager.Instance.Game.Content.Load<Texture2D>("Background"), new Vector2(0, 0), Color.PapayaWhip);
        pSpriteBatch.Draw(SceneManager.Instance.Game.Content.Load<Texture2D>("Pixel"), bottomBorder, Color.Gray * 0f);
        WaveManager.Instance.Draw(pSpriteBatch);
        base.Draw(pSpriteBatch);
    }
}