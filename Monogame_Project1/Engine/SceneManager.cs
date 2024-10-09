﻿using System.Linq;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Scenes;

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

    #endregion

    #region Properties
    public Scene CurrentScene => _currentScene;
    public Game1 Game => _game;

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

    public void Initialize() 
    {
        _scenesList = CreateSceneList();
        _currentScene = GetScene<UIScene>();
        LoadScene();
    }
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
    public void ChangeScene(Scene pTargetScene) 
    {
        // Check if the target scene has the same type as the current scene, if so skip the check.
        if (_currentScene.GetType() == pTargetScene.GetType())
            return;
        foreach (Scene scene in _scenesList) 
        {
            if (scene.GetType() == pTargetScene.GetType()) 
            {
                _currentScene = pTargetScene;
                LoadScene();
                if (pTargetScene is LevelScene level && level.PauseSystem.IsPaused)
                    level.PauseSystem.TogglePausedState();
                return;
            }
        }
    }

    #endregion

    #region Private Methods

    private void LoadScene() 
    {
        if (CurrentScene.IsLoaded) return;
        _currentScene.LoadContent(_contentManager);
        _currentScene.LateLoad();
        CurrentScene.IsLoaded = true;
    }
    private List<Scene> CreateSceneList()
    {
        List<Scene> scenes = new List<Scene>
        {
            new MainMenu(_game, this),
            new TestScene(_game, this),
            new SpawningScene(_game, this),
            new LevelSelectionScene(_game, this),
            new UIScene(_game, this)
        };
        return scenes;
    }

    #endregion
}