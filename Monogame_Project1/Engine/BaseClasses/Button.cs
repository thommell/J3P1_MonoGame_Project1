using Monogame_Project1.Engine.Singletons;
using System;

namespace Monogame_Project1.Engine.BaseClasses;

#region Enums

public enum ButtonStatus
{
    Normal,
    Hovered,
    Pressed,
    Clicked,
    Holded
}

#endregion

public class Button : GameObject
{
    #region Variables

    protected Game1 game;
    private ButtonStatus status;
    private readonly SpriteFont font;
    private MouseState currentMouseState;
    private MouseState previousMouseState;
    private bool isBeingHeld;
   
    #endregion
    
    #region Properties
    public string Text { get; set; }
    #endregion

    #region Constructor
    public Button(Texture2D pTexture, string text, bool pIsActive = true) : base(pTexture, pIsActive)
    {
        game = SceneManager.Instance.Game;
        font = game.Content.Load<SpriteFont>("Font");
        status = ButtonStatus.Normal;
        Text = text;
    }
    #endregion

    #region Public Methods

    public override void Update(GameTime pGameTime)
    {
        ButtonStatus previousButtonStatus = status;
        previousMouseState = currentMouseState;
        currentMouseState = Mouse.GetState();
    
        Rectangle mouseRectangle = new(currentMouseState.X, currentMouseState.Y, 1, 1);
    
        if (mouseRectangle.Intersects(BoundingBox) && currentMouseState.LeftButton == ButtonState.Released)
            OnHover();
    
        if (!mouseRectangle.Intersects(BoundingBox))
            OnNormal();
    
        if (mouseRectangle.Intersects(BoundingBox) && currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            OnClick();

        if (currentMouseState.LeftButton == ButtonState.Released)
            isBeingHeld = false;

        if (mouseRectangle.Intersects(BoundingBox) && currentMouseState.LeftButton == ButtonState.Pressed && previousButtonStatus == ButtonStatus.Hovered)
            isBeingHeld = true;

        if (isBeingHeld)
            OnHold();
      
       
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
            case ButtonStatus.Holded:
                color = Color.Green;
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
        Console.WriteLine("Clicked");
        AudioManager.Instance.PlaySound("OnClick");
        status = ButtonStatus.Pressed;
    }
    protected virtual void OnHold()
    {
        status = ButtonStatus.Holded;
    }
    protected virtual void OnHover()
    {
        status = ButtonStatus.Hovered;
    }
    protected void OnNormal()
    {
        status = ButtonStatus.Normal;
    }

    #endregion
}
