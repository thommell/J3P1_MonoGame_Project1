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
    public LevelSelectionScene levelSelect;

    #region Components

    private SpawningSystem _currentSpawningSystem;
    private Timer _currentSpawningTimer;

    #endregion
    public void GetData()
    {
        GetSceneData();
        levelSelect = SceneManagerSingleton.Instance.GetScene<LevelSelectionScene>();
        SetResult(SceneManagerSingleton.Instance.GetScene<Level1>(), Result.Win);
    }

    private void GetSceneData()
    {
        foreach (var pair in SceneManagerSingleton.Instance.GetScenes<LevelScene>())
        {
            sceneResults.Add((LevelScene)pair.Value, Result.Undecided);
        }
    }

    public void SetResult(LevelScene pLevel, Result pResult)
    {
        if (sceneResults.ContainsKey(pLevel))
        {
            // Change the Result (value) of the contained key (pLevel) to pResult.
            sceneResults[pLevel] = pResult;
        }
        else
            throw new NullReferenceException($"{pLevel} is not a valid level scene!");
    }
    public void ResetData()
    {
        sceneResults.Clear();
        GetSceneData();
    }

    // private Dictionary<LevelScene, Result> GetNewResults()
    // {
    // return SceneManagerSingleton.Instance.GetScenes<LevelScene>(level => level, _ => Result.Undecided);
    // }
    public void Update(GameTime pGameTime)
    {
        if (SceneManagerSingleton.Instance.CurrentScene is not LevelScene)
            return;
        _currentSpawningSystem = SceneManagerSingleton.Instance.CurrentScene.GetObject<SpawningSystem>();
        _currentSpawningTimer = SceneManagerSingleton.Instance.CurrentScene.GetObject<Timer>();

        if (!_currentSpawningSystem.HasSpawned)
            return;
        if (_currentSpawningTimer.Time <= 0.1f)
            HandleResult((LevelScene)SceneManagerSingleton.Instance.CurrentScene, Result.Lose);
        if (_currentSpawningSystem.currentTargets.Any(a => a is Target && a.IsActive) && _currentSpawningSystem.HasSpawned) return;
        {
            Console.WriteLine("User has finished the level!");
            HandleResult((LevelScene)SceneManagerSingleton.Instance.CurrentScene, Result.Win);
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
        SceneManagerSingleton.Instance.SwapScene(SceneManagerSingleton.Instance.GetScene<WinScene>());
    }
    private void HandleLoseResult()
    {
        SceneManagerSingleton.Instance.SwapScene(SceneManagerSingleton.Instance.GetScene<LoseScene>());
    }
}