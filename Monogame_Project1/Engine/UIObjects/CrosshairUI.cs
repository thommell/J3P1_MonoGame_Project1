using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.UIObjects;

public class CrosshairUI : UIObject
{
    private Game1 _game;
    public CrosshairUI(Texture2D pTexture, Game1 pGame, Color pColor) : base(pTexture) 
    { 
        _game = pGame;
        color = pColor;
    }
    public override void LoadContent(ContentManager pContent)
    {
        _game.IsMouseVisible = false;

        base.LoadContent(pContent);
    }
    public override void Update(GameTime pGameTime)
    {
        MouseState mouseState = Mouse.GetState();

        position = new Vector2(mouseState.X, mouseState.Y);

        base.Update(pGameTime);
    }
}