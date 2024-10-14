using System;
using System.Linq;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Scenes;

namespace Monogame_Project1.Engine.Singletons;

public class ResultHandlerSingleton
{
    private static ResultHandlerSingleton _instance;
    public static ResultHandlerSingleton Instance => _instance ??= new ResultHandlerSingleton();

    public Dictionary<LevelScene, Result> sceneResults = new();
    public List<LevelScene> levelScenes = new();
    public SceneManager sceneManager;
    public LevelSelectionScene levelSelect;

    #region Components

    private SpawningSystem _currentSpawningSystem;
    private Timer _currentSpawningTimer;

    #endregion
    public void GetData()
    {
        sceneResults = GetNewResults();
        levelScenes = sceneManager.GetScenes<LevelScene>();
        levelSelect = sceneManager.GetScene<LevelSelectionScene>();
        SetResult(sceneManager.GetScene<Level1>(), Result.Win);
    }
    public void SetResult(LevelScene pLevel, Result pResult)
    {
        if (sceneResults.ContainsKey(pLevel))
        {
            // Change the Result (value) of the contained key (pLevel) to pResult.
            sceneResults[pLevel] = pResult;
        }
        else
        {
            Console.WriteLine($"{pLevel} doesn't exit?");
        }

        if (pLevel == sceneManager.GetScene<Level2>())
        {
            
        }
    }
    public void ResetData()
    {
        sceneResults = GetNewResults();
    }

    private Dictionary<LevelScene, Result> GetNewResults()
    {
        return sceneManager.GetScenes<LevelScene>().ToDictionary(level => level, _ => Result.Undecided);
    }
    public void Update(GameTime pGameTime)
    {
        if (sceneManager.CurrentScene is not LevelScene)
            return;
        _currentSpawningSystem = sceneManager.CurrentScene.GetObject<SpawningSystem>();
        _currentSpawningTimer = sceneManager.CurrentScene.GetObject<Timer>();

        if (!_currentSpawningSystem.HasSpawned)
            return;
        if (_currentSpawningTimer.Time <= 0.1f)
            HandleResult((LevelScene)sceneManager.CurrentScene, Result.Lose);
        if (_currentSpawningSystem.CurrentTargets.Any(a => a is Target && a.IsActive) && _currentSpawningSystem.HasSpawned) return;
        {
            Console.WriteLine("User has finished the level!");
            HandleResult((LevelScene)sceneManager.CurrentScene, Result.Win);
        }
    }
    public void HandleResult(LevelScene pScene ,Result pResult)
    {
        SetResult(pScene, pResult);
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
        _currentSpawningSystem.HasSpawned = false;
        sceneManager.ChangeScene(sceneManager.GetScene<WinScene>());
    }
    private void HandleLoseResult()
    {
        sceneManager.ChangeScene(sceneManager.GetScene<LoseScene>());
    }
}