using System;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.GameObjects;

public class TimeSystem : GameObject
{
    private float _elapsedTime;
    private readonly float _originalTime;
    private SpawningSystem _spawningSystem;
    private Timer _timer;
    private bool _isWaiting;
    private SpriteFont _font;
    private float _displayedElapsedTime;
    public TimeSystem(float pTime, SpawningSystem pSpawningSystem, Timer timer, SpriteFont font)
    {
        _elapsedTime = pTime;
        _originalTime = pTime;
        _spawningSystem = pSpawningSystem;
        _timer = timer;
        _font = font;
    }
    public override void Update(GameTime pGameTime)
    {
        UpdateTimer(pGameTime);
    }
    private void UpdateTimer(GameTime pGameTime)
    {
        if (_isWaiting) return;
        var rawElapsedTime = _elapsedTime -= (float)pGameTime.ElapsedGameTime.TotalSeconds;
        _displayedElapsedTime = (float)Math.Round(rawElapsedTime);
        CheckTimer();
    }

    private void CheckTimer()
    {
        if (_elapsedTime >= 0.1f) return;
        ResetTimer();
        _timer.IsRunning = true;
        _spawningSystem.StartSpawner();
    }

    private void ResetTimer()
    {
        _isWaiting = true;
    }

    public override void Draw(SpriteBatch pSpriteBatch)
    {
        DrawTimer(pSpriteBatch);
        base.Draw(pSpriteBatch);
    }
    private void DrawTimer(SpriteBatch pSpriteBatch)
    {
        if (!_isWaiting)
            pSpriteBatch.DrawString(_font, _displayedElapsedTime.ToString(), new Vector2(SceneManager.Instance.Game.GraphicsDevice.Viewport.Width / 2, SceneManager.Instance.Game.GraphicsDevice.Viewport.Height / 2), Color.White);
    }
}