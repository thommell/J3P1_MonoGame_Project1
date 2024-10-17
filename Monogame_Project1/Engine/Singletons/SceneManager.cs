using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Scenes;

namespace Monogame_Project1.Engine.Singletons;

public sealed class SceneManager
{
    private static SceneManager _instance;
    public static SceneManager Instance => _instance ??= new SceneManager();
    
    private Scene _currentScene;
    private Dictionary<string, Scene> _scenesDictionary = new(); // Dictionary where u can do string-based lookups
    private Game1 _game;
    
    public LevelScene pastLevelScene;
    public ScoringSystem scoringSystem;
    public Scene CurrentScene => _currentScene;

    public Game1 Game
    {
        get => _game;
        set => _game = value;
    }
    public ScoringSystem ScoringSystem => scoringSystem;
    public void Awake()
    {
        CreateScenes(ref _scenesDictionary);
        _currentScene = GetScene<MainMenu>();
        scoringSystem = new ScoringSystem(CurrentScene);
        LoadScene();
        ResultHandler.Instance.GetData();
        pastLevelScene = GetScene<Level1>();
    }
    public void LoadScene()
    {
        Console.WriteLine($"Loading {_currentScene.SceneName}");
        _currentScene.LoadContent(Game.Content);
        _currentScene.LateLoad();
    }
    public void Update(GameTime pGameTime) => _currentScene.Update(pGameTime);
    public void Draw(SpriteBatch pSpriteBatch) => _currentScene.Draw(pSpriteBatch);
    public void SwapScene(Scene pScene)
    {
        pastLevelScene ??= (LevelScene)pScene;
        if (pScene is null)
            throw new NullReferenceException("The given scene is null? Check the dictionary!\nCan't swap scenes!");
        if (pScene.Equals(_currentScene))
            RestartLevel((LevelScene)pScene);
        if (GetScene(pScene.SceneName) != null)
            SetScene(pScene);
        else
            throw new ArgumentNullException($"{pScene} has no name?\nCan't swap scenes!");
    }
    private void SetScene(Scene pScene)
    {
        _currentScene = pScene;
        pScene.UnloadScene();
        LoadScene();
        UpdateCrosshairVisibility();
    }
    public void RestartLevel(LevelScene pScene)
    {
        pScene.UnloadScene();
        SetScene(pastLevelScene);
    }
    public T GetScene<T>() where T : Scene
    {
        foreach (var pair in _scenesDictionary)
        {
            if (pair.Value is T scene)
                return scene;
        }
        throw new NullReferenceException($"The type ({typeof(T)}) you're searching for doesn't exist!");
    }
    public Scene GetScene(string pScene) =>
        (from kvpPair in _scenesDictionary where kvpPair.Key.ToLower() == pScene select kvpPair.Value).FirstOrDefault();
    public Dictionary<string, Scene> GetScenes<T>() where T : Scene
    {
        var dict = new Dictionary<string, Scene>();
        foreach (var scenePair in _scenesDictionary)
        {
            if (scenePair.Value is T scene)
                dict.Add(scenePair.Key, scene);
        }
        if (dict.Count == 0)
            throw new Exception($"No scenes of type ({typeof(T)}) found!");
        return dict;
    }
    private void CreateScenes(ref Dictionary<string, Scene> pScenesDictionary)
    {
        pScenesDictionary.Add("MainMenu", new MainMenu());
        pScenesDictionary.Add("Level1", new Level1());
        pScenesDictionary.Add("Level2", new Level2());
        pScenesDictionary.Add("Level3", new Level3());
        pScenesDictionary.Add("Level4", new Level4());
        pScenesDictionary.Add("Level5", new Level5());
        pScenesDictionary.Add("WinScene", new WinScene());
        pScenesDictionary.Add("LoseScene", new LoseScene());
        pScenesDictionary.Add("LevelSelectScene", new LevelSelectionScene());
        pScenesDictionary.Add("Settings", new Settings());
        AssignSceneNames();
        return;

        void AssignSceneNames()
        {
            foreach (var pair in _scenesDictionary)
            {
                pair.Value.SceneName = pair.Key.ToLower();
            }
        }
    }
    private void UpdateCrosshairVisibility()
    {
        if (_currentScene is LevelScene)
            _game.IsMouseVisible = false;
        else
            _game.IsMouseVisible = true;
    }

    public void ChangeCrosshairVisibility() => _game.IsMouseVisible = !_game.IsMouseVisible;

}