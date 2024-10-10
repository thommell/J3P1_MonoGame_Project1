using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;

public class TimeSystem : GameObject
{
    private float _elapsedTime;
    private readonly float _originalTime;
    private SpawningSystem _spawningSystem;
    private Timer _timer;
    private bool _isWaiting;
    public TimeSystem(float pTime, SpawningSystem pSpawningSystem, Timer timer)
    {
        _elapsedTime = pTime;
        _originalTime = pTime;
        _spawningSystem = pSpawningSystem;
        _timer = timer;
    }
    public override void Update(GameTime pGameTime)
    {
        UpdateTimer(pGameTime);
    }
    private void UpdateTimer(GameTime pGameTime)
    {
        if (_isWaiting) return;
        _elapsedTime -= (float)pGameTime.ElapsedGameTime.TotalSeconds;
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
}