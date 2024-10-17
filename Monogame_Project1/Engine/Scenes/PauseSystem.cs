using System;
using System.Xml.Linq;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Singletons;
using Monogame_Project1.Engine.UIObjects;

namespace Monogame_Project1.Engine.Scenes;
public class PauseSystem : GameObject
{
    public bool IsPaused { get; set; }
    private readonly SpriteFont _font;
    private readonly Texture2D _pixelTexture;
    private KeyboardState _previousKeyboardState;
    private SwitchSceneButton _menuButton;
    private SoundSliderUI _soundSliderUI;
    private MusicSliderUI _musicSliderUI;
    private RestartButton _restartButton;
    private List<GameObject> _pausedObjects = new();
    public PauseSystem(SpriteFont pFont, Texture2D pTexture)
    {
        _font = pFont;
        _pixelTexture = pTexture;
        _previousKeyboardState = Keyboard.GetState();
        IsPaused = false;
    }
    public override void LoadContent(ContentManager pContent)
    {
        _soundSliderUI = (new SoundSliderUI(SceneManager.Instance.Game.Content.Load<Texture2D>("TestSprite"), "SFX", false)
        {
            Position = new Vector2(SceneManager.Instance.Game.GraphicsDevice.Viewport.Width * 0.2f, SceneManager.Instance.Game.GraphicsDevice.Viewport.Height * 0.8f)
        });
        _musicSliderUI = (new MusicSliderUI(SceneManager.Instance.Game.Content.Load<Texture2D>("TestSprite"), "Music", false)
        {
            Position = new Vector2(SceneManager.Instance.Game.GraphicsDevice.Viewport.Width * 0.6f, SceneManager.Instance.Game.GraphicsDevice.Viewport.Height * 0.8f)
        });
        
        _soundSliderUI.LoadContent(pContent);
        _musicSliderUI.LoadContent(pContent);

        base.LoadContent(pContent);
    }
    public override void LateLoad()
    {
        _menuButton = new SwitchSceneButton(SceneManager.Instance.Game.Content.Load<Texture2D>("UI_Tile_128x64"), "Menu",
            SceneManager.Instance.GetScene<MainMenu>(), false)
        {
            Position = new Vector2(SceneManager.Instance.Game.GraphicsDevice.Viewport.Width * 0.5f, SceneManager.Instance.Game.GraphicsDevice.Viewport.Height * 0.5f - 50)
        };
        _restartButton = new RestartButton(
            SceneManager.Instance.Game.Content.Load<Texture2D>("UI_Tile_128x64"), "Restart", false)
        {
            Position = new Vector2(SceneManager.Instance.Game.GraphicsDevice.Viewport.Width * 0.5f,
                SceneManager.Instance.Game.GraphicsDevice.Viewport.Height * 0.5f + 100)
        };

        SceneManager.Instance.CurrentScene.Objects.Add(_menuButton);
        SceneManager.Instance.CurrentScene.Objects.Add(_restartButton);
        SceneManager.Instance.CurrentScene.Objects.Add(_soundSliderUI);
        SceneManager.Instance.CurrentScene.Objects.Add(_musicSliderUI);
        _pausedObjects.Add(_menuButton);
        _pausedObjects.Add(_restartButton);
        _pausedObjects.Add(_soundSliderUI);
        _pausedObjects.Add(_musicSliderUI);
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
        SceneManager.Instance.Game.IsMouseVisible = true;
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
