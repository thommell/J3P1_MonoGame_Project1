using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Scenes;
using System;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine;

public class SceneManager 
{ 
    #region Fields

    private GraphicsDeviceManager _graphicsDevice;
    private readonly ContentManager _contentManager;
    private SpriteBatch _spriteBatch;
    private Scene _currentScene;
    private List<Scene> _scenesList = new();
    private readonly Game1 _game;
    public LevelScene PastLevelScene;
    protected ScoringSystem scoringSystem;
    #endregion

    #region Properties
    public Scene CurrentScene => _currentScene;
    public Game1 Game => _game;
    public List<Scene> ScenesList => _scenesList;

    public ScoringSystem ScoringSystem => scoringSystem;
    
    #endregion

    #region Constructor

    public SceneManager(GraphicsDeviceManager pGraphicsDevice, ContentManager pContentManager, SpriteBatch pSpriteBatch, Game1 pGame) 
    {
        _graphicsDevice = pGraphicsDevice;
        _contentManager = pContentManager;
        _spriteBatch = pSpriteBatch;
        _game = pGame;
    }

    #endregion

    #region Public Methods

    public void Awake()
    {
        _scenesList = CreateSceneList();
        _currentScene = GetScene<MainMenu>();
        scoringSystem = new ScoringSystem(CurrentScene);
        // LoadScene();
        LoadAllScenes();
       // ResultHandlerSingleton.Instance.GetData();
    }

    // public void RestartInitialize() 
    // {
    //     _scenesList = CreateSceneList();
    //     _currentScene = GetScene<LevelScene>();
    //     // LoadScene();
    //     // _currentScene.LoadContent(_contentManager);
    //     RestartGame();
    // }
    public void Update(GameTime pGameTime) 
    {
        _currentScene.Update(pGameTime);
    }
    public void Draw(SpriteBatch pSpriteBatch) 
    {
        _currentScene.Draw(pSpriteBatch);
    }
    public T GetScene<T>() where T : Scene 
    {
        for (int i = 0; i < _scenesList.Count; i++) 
        {
            if (_scenesList[i] is T scene)
                return scene;
        }
        return null;
    }

    public List<T> GetScenes<T>() where T : Scene
    {
        List<T> levelScenes = new();
        for (int i = 0; i < _scenesList.Count; i++) 
        {
            if (_scenesList[i] is T scene)
                levelScenes.Add(scene);
        }
        return levelScenes;
    }
    public void ChangeScene(Scene pTargetScene) 
    {
        // Check if the target scene has the same type as the current scene, if so skip the check.
        if (_currentScene.GetType() == pTargetScene.GetType())
            return;
        foreach (Scene scene in _scenesList) 
        {
            if (scene.GetType() == pTargetScene.GetType()) 
            {
                if (_currentScene is LevelScene levelScene)
                    PastLevelScene = levelScene;
                _currentScene = pTargetScene;
                // LoadScene();
                if (pTargetScene is LevelScene level && level.PauseSystem.IsPaused)
                    level.PauseSystem.TogglePausedState();
                return;
            }
        }
    }

    public void RestartGame()
    {
        _currentScene = GetScene<MainMenu>();
        _scenesList.Clear();
        Awake();
    }

    public void RestartLevel()
    {
        if (_currentScene is LevelScene)
        {
            scoringSystem.ResetScore();
            int currentSceneIndex = _scenesList.FindIndex(scene => scene.GetType() == _currentScene.GetType());

            if (currentSceneIndex != -1)
            {
                Scene newSceneInstance = (Scene)Activator.CreateInstance(_currentScene.GetType(), _game, this);
                _scenesList[currentSceneIndex] = newSceneInstance;
                _currentScene = newSceneInstance;
                _scenesList.Clear();
                // RestartInitialize();
            }
        }
        else
        {
            scoringSystem.ResetScore();
            _currentScene = PastLevelScene;
            _scenesList.Clear();
            // RestartInitialize();
        }
    }

    #endregion

    #region Private Methods

    private void LoadAllScenes()
    {
        foreach (var s in ScenesList)
        {
            s.LoadContent(_contentManager);
            s.LateLoad();
            _game.IsMouseVisible = s is LevelScene;
        }
    }
    private void LoadScene() 
    {
        if (CurrentScene.IsLoaded) return;
        _currentScene.LoadContent(_contentManager);
        _currentScene.LateLoad();
        CurrentScene.IsLoaded = true;

        if (CurrentScene is LevelScene)
        {
            _game.IsMouseVisible = false;
        }
        else
        {
            _game.IsMouseVisible = true;
        }

        Console.WriteLine(_game.IsMouseVisible);
    }
    private List<Scene> CreateSceneList()
    {
        // List<Scene> scenes = new List<Scene>
        // {
        //     new MainMenu(),
        //     new LevelSelectionScene(),
        //     new WinScene(),
        //     new LoseScene(),
        //     new Level1(),
        //     new Level2(),
        //     new Level3(),
        //     new Level4(),
        //     new Level5()
        // };
        return null;
    }

    #endregion
}