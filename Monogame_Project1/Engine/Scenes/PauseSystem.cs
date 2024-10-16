using System;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.Scenes;
public class PauseSystem : GameObject
{
    public bool IsPaused { get; set; }
    private readonly SpriteFont _font;
    private readonly Texture2D _pixelTexture;
    private KeyboardState _previousKeyboardState;
    private SwitchSceneButton _menuButton;
    private RestartButton _restartButton;
    private List<GameObject> _pausedObjects = new();
    public PauseSystem(SpriteFont pFont, Texture2D pTexture)
    {
        _font = pFont;
        _pixelTexture = pTexture;
        _previousKeyboardState = Keyboard.GetState();
        IsPaused = false;
    }

    public override void LateLoad()
    {
        _menuButton = new SwitchSceneButton(SceneManagerSingleton.Instance.Game.Content.Load<Texture2D>("UI_Tile_128x64"), "Menu",
            SceneManagerSingleton.Instance.GetScene<MainMenu>(), false)
        {
            Position = new Vector2(SceneManagerSingleton.Instance.Game.GraphicsDevice.Viewport.Width * 0.5f, SceneManagerSingleton.Instance.Game.GraphicsDevice.Viewport.Height * 0.5f - 50)
        };
        _restartButton = new RestartButton(
            SceneManagerSingleton.Instance.Game.Content.Load<Texture2D>("UI_Tile_128x64"), "Restart", false)
        {
            Position = new Vector2(SceneManagerSingleton.Instance.Game.GraphicsDevice.Viewport.Width * 0.5f,
                SceneManagerSingleton.Instance.Game.GraphicsDevice.Viewport.Height * 0.5f + 100)
        };
        SceneManagerSingleton.Instance.CurrentScene.Objects.Add(_menuButton);
        SceneManagerSingleton.Instance.CurrentScene.Objects.Add(_restartButton);
        _pausedObjects.Add(_menuButton);
        _pausedObjects.Add(_restartButton);
        base.LateLoad();
    }

    public void TogglePausedState()
    {
        IsPaused = !IsPaused;
        _pausedObjects.ForEach(pausedObject => pausedObject.IsActive = !pausedObject.IsActive);
    }

    public override void Update(GameTime pGameTime)
    {
        UpdateState();
        if (!IsPaused) return;
        for (int i = 0; i <= _pausedObjects.Count - 1; i++)
            _pausedObjects[i].Update(pGameTime);
        SceneManagerSingleton.Instance.Game.IsMouseVisible = true;
    }
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        if (!IsPaused)
            return;
        _pausedObjects.ForEach(pausedObject => pausedObject.Draw(pSpriteBatch));
        DrawState(pSpriteBatch);
    }
    public void UpdateState()
    {
        KeyboardState currentKeyboardState = Keyboard.GetState();
        if (currentKeyboardState.IsKeyDown(Keys.Escape) && _previousKeyboardState.IsKeyUp(Keys.Escape)) TogglePausedState();
        _previousKeyboardState = currentKeyboardState;
    }

    public void DrawState(SpriteBatch pSpriteBatch)
    {
        pSpriteBatch.Draw(_pixelTexture, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight), Color.Black * 0.5f);
        string pausedText = "Game has been paused.";
        Vector2 textSize = _font.MeasureString(pausedText);
        pSpriteBatch.DrawString(_font, pausedText, new Vector2((Game1.ScreenWidth / 2) - (textSize.X / 2), (Game1.ScreenHeight / 2) - (textSize.Y / 2)), Color.White);
    }
}
