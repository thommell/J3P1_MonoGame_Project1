using System;
using System.Linq;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Enums;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Scenes;

namespace Monogame_Project1.Engine.Singletons;

public class ResultHandler
{
    private static ResultHandler _instance;
    public static ResultHandler Instance => _instance ??= new ResultHandler();

    public Dictionary<LevelScene, Results> sceneResults = new();
    public List<LevelScene> levelScenes = new();
    public LevelSelectionScene levelSelect;
    private List<SelectionScreenButton> _buttons;

    #region Components

    private SpawningSystem _currentSpawningSystem;
    private Timer _currentSpawningTimer;

    #endregion
    public void GetData()
    {
        GetSceneData();
        levelSelect = SceneManager.Instance.GetScene<LevelSelectionScene>();
        _buttons = levelSelect.GetObjects<SelectionScreenButton>();
        SetResult(SceneManager.Instance.GetScene<Level1>(), Results.Win);
    }

    private void GetSceneData()
    {
        foreach (var pair in SceneManager.Instance.GetScenes<LevelScene>())
        {
            sceneResults.Add((LevelScene)pair.Value, Results.Undecided);
        }
    }

    public void SetResult(LevelScene pLevel, Results pResult)
    {
        if (sceneResults.ContainsKey(pLevel))
        {
            // Change the Result (value) of the contained key (pLevel) to pResult.
            sceneResults[pLevel] = pResult;
            UpdateLocks();
        }
        else
            throw new NullReferenceException($"{pLevel} is not a valid level scene!");
    }

    private void UpdateLocks()
    {
        var scenes = SceneManager.Instance.GetScenes<LevelScene>(); 

        int i = 0; 
        
        foreach (var entry in scenes)
        {
            var scene = entry.Value;
            if (scene is not LevelScene currentScene) continue;
            if (!sceneResults.TryGetValue(SceneManager.Instance.pastLevelScene = currentScene, out Results result) || 
                result != Results.Win) 
            {
                continue;
            }
            if (i < _buttons.Count)
            {
                _buttons[i].Unlock();
                if (i + 1 < _buttons.Count)
                {
                    Console.WriteLine($"Button: {_buttons[i + 1].SceneToSwitchTo} is locked: {_buttons[i + 1].IsLocked}");
                }
            }
            i++; 
        } 
    }

    public void ResetData()
    {
        sceneResults.Clear();
        GetSceneData();
    }
    public void Update(GameTime pGameTime)
    {
        if (SceneManager.Instance.CurrentScene is not LevelScene)
            return;
        _currentSpawningSystem = SceneManager.Instance.CurrentScene.GetObject<SpawningSystem>();
        _currentSpawningTimer = SceneManager.Instance.CurrentScene.GetObject<Timer>();

        if (!_currentSpawningSystem.HasSpawned)
            return;
        if (_currentSpawningTimer.Time <= 0.1f)
            HandleResult((LevelScene)SceneManager.Instance.CurrentScene, Results.Lose);
        // if (_currentSpawningSystem.currentTargets.Any(a => a is Target && a.IsActive) && _currentSpawningSystem.HasSpawned) return;
        // {
        //     Console.WriteLine("User has finished the level!");
        //     HandleResult((LevelScene)SceneManager.Instance.CurrentScene, Results.Win);
        // }
    }
    public void HandleResult(LevelScene pScene, Results pResult)
    {
        SetResult(pScene, pResult);
        switch (pResult)
        {
            case Results.Win:
                HandleWinResult();
                break;
            case Results.Lose:
                HandleLoseResult();
                break;
            default:
                return;
        }
    }
    private void HandleWinResult()
    {
        _currentSpawningSystem.HasSpawned = false;
        SceneManager.Instance.SwapScene(SceneManager.Instance.GetScene<WinScene>());
    }
    private void HandleLoseResult()
    {
        SceneManager.Instance.SwapScene(SceneManager.Instance.GetScene<LoseScene>());
    }
}