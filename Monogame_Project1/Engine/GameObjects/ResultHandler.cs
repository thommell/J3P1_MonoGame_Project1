using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Scenes;

namespace Monogame_Project1.Engine.GameObjects;


public enum Result{
    Win,
    Lose,
    Undecided
}
public class ResultHandler : GameObject
{
    private SpawningSystem _spawningSystem;
    private SceneManager _sceneManager;
    private Timer _timer;
    private Result _result = Result.Undecided;
    public ResultHandler(SpawningSystem spawningSystem, SceneManager pSceneManager, Timer timer)
    {
        _spawningSystem = spawningSystem;
        _sceneManager = pSceneManager;
        _timer = timer;
    }
    public override void Update(GameTime pGameTime)
    {
        if (!_spawningSystem.HasSpawned) return;
        
        if (_timer.Time <= 0.1f)
            HandleResult(Result.Lose);
        if (_spawningSystem.CurrentTargets.Any(a => a is Target && a.IsActive) && _spawningSystem.HasSpawned) return;
        {
            Console.WriteLine("User has finished the level!");
            HandleResult(Result.Win);
        }
    }
    public void HandleResult(Result pResult)
    {
        switch (pResult)
        {
            case Result.Win:
                HandleWinResult();
                break;
            case Result.Lose:
                HandleLoseResult();
                break;
            case Result.Undecided:
                return;
        }
        
    }
    private void HandleWinResult()
    {
        _spawningSystem.HasSpawned = false;
        _sceneManager.ChangeScene(_sceneManager.GetScene<WinScene>());
    }

    private void HandleLoseResult()
    {
        _sceneManager.ChangeScene(_sceneManager.GetScene<LoseScene>());
    }

}