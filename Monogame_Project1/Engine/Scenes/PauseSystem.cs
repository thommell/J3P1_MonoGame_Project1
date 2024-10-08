using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;

namespace Monogame_Project1.Engine.Scenes;
public class PauseSystem : GameObject
{
    public bool IsPaused { get; set; }
    private readonly SpriteFont _font;
    private readonly Texture2D _pixelTexture;
    private KeyboardState _previousKeyboardState;
    private SwitchSceneButton _menuButton;
    private SceneManager _sceneManager;
    public PauseSystem(SpriteFont pFont, Texture2D pTexture, SceneManager pManager)
    {
        _font = pFont;
        _pixelTexture = pTexture;
        _previousKeyboardState = Keyboard.GetState();
        _sceneManager = pManager;
        IsPaused = false;
    }

    public override void LateLoad()
    {
        _menuButton = new SwitchSceneButton(_sceneManager.Game, _sceneManager, _sceneManager.Game.Content.Load<Texture2D>("UI_Tile_128x64"), "Menu",
            _sceneManager.GetScene<MainMenu>(), false)
        {
            Position = new Vector2(_sceneManager.Game.GraphicsDevice.Viewport.Width * 0.5f, _sceneManager.Game.GraphicsDevice.Viewport.Height * 0.5f - 50),
        };
        _sceneManager.CurrentScene.Objects.Add(_menuButton);
        base.LateLoad();
    }

    public void TogglePausedState()
    {
        IsPaused = !IsPaused;
        _menuButton.IsActive = !_menuButton.IsActive;
    }

    public override void Update(GameTime pGameTime)
    {
        UpdateState();
        if (IsPaused)
            _menuButton.Update(pGameTime);
    }
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        if (!IsPaused)
            return;
        _menuButton.Draw(pSpriteBatch);
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
