using System;
using Monogame_Project1.Engine;
namespace J3P1_CSharp_Advanced.Opdracht4.GameObjects;

public enum ButtonStatus {
    Normal,
    Hovered,
    Pressed,
    Clicked
}

public class Button : GameObject {
    private ButtonStatus _currentButtonState = ButtonStatus.Normal;
    private MouseState _mouse;
    private Vector2 _mousePosition;
    private MouseState _prevMouseClick;
    private readonly string _buttonText;
    private Vector2 _buttonTextSize;
    private Vector2 _buttonTextPosition;
    protected Scene targetScene;
    protected SceneManager sceneManager;
    public ButtonStatus CurrentButtonState {
        get => _currentButtonState;
        set => _currentButtonState = value;
    }
    public Button(Texture2D pTexture, string pButtonText) : base(pTexture) {
        _buttonText = pButtonText;
    }
    public Button(Texture2D pTexture, string pButtonText, Scene pTargetScene, SceneManager pSceneManager) : base(pTexture) {
        _buttonText = pButtonText;
        targetScene = pTargetScene;
        sceneManager = pSceneManager;
    }
    public override void LoadContent(ContentManager pContent) {
        NormalState();
        SetButtonTextBounds();
    }
    public override void Update(GameTime pGameTime) {
        CreateMouseState();
        ExecuteButtonState();
    }
    public override void Draw(SpriteBatch pSpriteBatch) {
        base.Draw(pSpriteBatch);
        pSpriteBatch.DrawString(Utility.Font, _buttonText, _buttonTextPosition, Color.White);
    }
   
    protected virtual void OnClick() {}

    private void CreateMouseState() {
        _mouse = Mouse.GetState();
        _mousePosition = new Vector2(_mouse.X, _mouse.Y);
    }
    public virtual void NormalState() {
        CurrentButtonState = ButtonStatus.Normal;
        Color = Color.White;
        if (HitBox.Contains(_mousePosition)) {
            CurrentButtonState = ButtonStatus.Hovered;
        }
    }
    public virtual void HoveredState() {
        CurrentButtonState = ButtonStatus.Hovered;
        Color = Color.LightGray;
        if (_mouse.LeftButton == ButtonState.Pressed) {
            CurrentButtonState = ButtonStatus.Pressed;
        }
        if (!HitBox.Contains(_mousePosition)) 
            CurrentButtonState = ButtonStatus.Normal;
    }
    public virtual void PressedState() {
        Color = Color.Red;
        if (!HitBox.Contains(_mousePosition))
            CurrentButtonState = ButtonStatus.Normal;
        if (_prevMouseClick.LeftButton == ButtonState.Pressed && _mouse.LeftButton == ButtonState.Released) {
            CurrentButtonState = ButtonStatus.Clicked;
        }
    }
    private void ExecuteButtonState(){
        switch (CurrentButtonState) {
            case ButtonStatus.Normal:
                NormalState();
                break;
            case ButtonStatus.Hovered:
                HoveredState();
                break;
            case ButtonStatus.Pressed:
                PressedState();
                break;
            case ButtonStatus.Clicked:
                OnClick();
                NormalState();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        _prevMouseClick = _mouse;
    }
    private void SetButtonTextBounds() {
        _buttonTextSize = Utility.Font.MeasureString(_buttonText);
        _buttonTextPosition = new Vector2(Position.X - _buttonTextSize.X * 0.5f, Position.Y - _buttonTextSize.Y * 0.5f);
    }
}