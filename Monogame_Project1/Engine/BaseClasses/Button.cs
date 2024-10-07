namespace Monogame_Project1.Engine.BaseClasses;

#region Enums

public enum ButtonStatus
{
    Normal,
    Hovered,
    Pressed,
    Clicked
}

#endregion

public class Button : GameObject
{
    #region Variables

    protected Game1 game;
    protected SceneManager manager;
    private ButtonStatus status;
    private readonly SpriteFont font;
    private MouseState currentMouseState;
    private MouseState previousMouseState;
   
    #endregion
    
    #region Properties
    public string Text { get; set; }
    #endregion

    #region Constructor
    public Button(Game1 pGame, SceneManager pManager, Texture2D pTexture, string text) : base(pTexture)
    {
        manager = pManager;
        font = pGame.Content.Load<SpriteFont>("Font");
        game = pGame;
        status = ButtonStatus.Normal;
        Text = text;
    }
    #endregion

    #region Public Methods

    public override void Update(GameTime pGameTime)
    {
        previousMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();
    
        Rectangle _mouseRectangle = new(currentMouseState.X, currentMouseState.Y, 1, 1);
    
        if (_mouseRectangle.Intersects(BoundingBox))
            OnHover();
    
        if (!_mouseRectangle.Intersects(BoundingBox))
            OnNormal();
    
        if (_mouseRectangle.Intersects(BoundingBox) && currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            OnClick();
    
        base.Update(pGameTime);
    }

    public override void Draw(SpriteBatch pSpriteBatch)
    {
        switch (status)
        {
            case ButtonStatus.Normal:
                color = Color.White;
                break;
            case ButtonStatus.Hovered:
                color = Color.Gray;
                break;
            case ButtonStatus.Pressed:
                color = Color.Red;
                break;
        }
    
        base.Draw(pSpriteBatch);
    
        if (!string.IsNullOrEmpty(Text))
        {
            float x = (BoundingBox.X + (BoundingBox.Width / 2)) - (font.MeasureString(Text).X / 2);
            float y = (BoundingBox.Y + (BoundingBox.Height / 2)) - (font.MeasureString(Text).Y / 2);
    
            pSpriteBatch.DrawString(font, Text, new Vector2(x, y), color, Rotation, new Vector2(0, 0), 1f, SpriteEffects.None, Layer + 0.01f);
        }
    }
    #endregion

    #region Protected Methods

    protected virtual void OnClick()
    {
        status = ButtonStatus.Pressed;
    }
    protected void OnHover()
    {
        status = ButtonStatus.Hovered;
    }
    protected void OnNormal()
    {
        status = ButtonStatus.Normal;
    }

    #endregion
}
