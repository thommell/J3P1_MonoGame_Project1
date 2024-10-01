using System;

namespace Monogame_Project1.Engine;

public class SceneManager { 

    #region Fields

    private GraphicsDeviceManager _graphicsDevice;
    private readonly ContentManager _contentManager;
    private SpriteBatch _spriteBatch;
    private Scene _currentScene;
    private List<Scene> _scenesList = new();
    private readonly Game1 _game;
    // public Player Player { get; private set; }
    // public SceneTransitionButton MenuButton { get; set; }

    #endregion

    #region Properties
    public Scene CurrentScene => _currentScene;

    #endregion
    public SceneManager(GraphicsDeviceManager pGraphicsDevice, ContentManager pContentManager, SpriteBatch pSpriteBatch, Game1 pGame) {
        _graphicsDevice = pGraphicsDevice;
        _contentManager = pContentManager;
        _spriteBatch = pSpriteBatch;
        _game = pGame;
        // Player = new Player(Utility.Utility.PlayerTextures.DefaultKnight, new Vector2(_game.ScreenSize.X * 0.5f, _game.ScreenSize.Y * 0.5f), 250f, this);
    }
    public void Initialize() {
        _scenesList = CreateSceneList();
        // _currentScene = GetScene<MenuScene>(); CURRENTSCENE WILL BE NULL!
        LoadScene();
    }
    public void Update(GameTime pGameTime) {
        _currentScene.Update(pGameTime);
    }
    public void Draw(SpriteBatch pSpriteBatch) {
        _currentScene.Draw(pSpriteBatch);
    }
    public T GetScene<T>() where T : Scene {
        for (int i = 0; i < _scenesList.Count; i++) {
            if (_scenesList[i] is T scene)
                return scene;
        }
        return null;
    }
    public void ChangeScene(Scene pTargetScene) {
        // Check if the target scene has the same type as the current scene, if so skip the check.
        if (_currentScene.GetType() == pTargetScene.GetType())
            return;
        foreach (Scene scene in _scenesList) {
            if (scene.GetType() == pTargetScene.GetType()) {
                _currentScene = pTargetScene;
                LoadScene();
                return;
            }
        }
    }
    private void LoadScene() {
        if (CurrentScene.IsLoaded) return;
        _currentScene.LoadContent(_contentManager);
        CurrentScene.IsLoaded = true;
    }
    private List<Scene> CreateSceneList() {
        List<Scene> scenes = new(); 
        return scenes;
    }
}