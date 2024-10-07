using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.Scenes;
public class PauseMenu : GameObject
{
    public bool IsPaused { get; set; }
    private readonly SpriteFont _font;
    private readonly Texture2D _pixelTexture;
    private KeyboardState _previousKeyboardState;
    public PauseMenu(SpriteFont pFont, Texture2D pTexture)
    {
        _font = pFont;
        _pixelTexture = pTexture;
        _previousKeyboardState = Keyboard.GetState();
    }

    public void TogglePausedState()
    {
        IsPaused = !IsPaused;
    }

    public override void Update(GameTime pGameTime)
    {
        KeyboardState _currentKeyboardState = Keyboard.GetState();
        if (_currentKeyboardState.IsKeyDown(Keys.Escape) && _previousKeyboardState.IsKeyUp(Keys.Escape)) TogglePausedState();

        _previousKeyboardState = _currentKeyboardState;
    }

    public override void Draw(SpriteBatch pSpriteBatch)
    {
        if (IsPaused)
        {
            pSpriteBatch.Draw(_pixelTexture, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight), Color.Black * 0.5f);

            string pausedText = "Game has been paused.";
            Vector2 textSize = _font.MeasureString(pausedText);
            pSpriteBatch.DrawString(_font, pausedText, new Vector2((Game1.ScreenWidth / 2) - (textSize.X / 2), (Game1.ScreenHeight / 2) - (textSize.Y / 2)), Color.White);
        }
    }
}
