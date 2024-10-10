using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using System.Runtime.CompilerServices;

namespace Monogame_Project1.Engine.UIObjects;

public class AmmoUI : UIObject
{
    private ShootingSystem _shootingSystem;
    private Scene _scene;
    private Game1 _game;

    private SpriteFont _spriteFont;
    
    //private Rectangle _rec;

    private readonly float _recWidth = 100f;
    private readonly float _recHeight = 100f;
    public AmmoUI(Texture2D pTexture, Scene pScene, Game1 pGame, bool pActive = true, Game1 game = null) : base(pTexture, pActive)
    {
        _scene = pScene;
        _game = pGame;
    }

    public override void LoadContent(ContentManager pContent)
    {
        _shootingSystem = _scene.GetObject<ShootingSystem>();
        _spriteFont = pContent.Load<SpriteFont>("UIText");
        //_rec = new Rectangle(50, 50, (int)_recWidth, (int)_recHeight);

        base.LoadContent(pContent);
    }

    public override void Draw(SpriteBatch pSpriteBatch)
    {
        string ammoText = "Ammo: " + _shootingSystem.Ammo.ToString();
        Vector2 textSize = _spriteFont.MeasureString(ammoText);

        float textX = position.X + (texture.Width / 2) - (textSize.X / 2);
        float textY = position.Y + (texture.Height / 2) - (textSize.Y / 2);

        float spacingX = 180f;
        //pSpriteBatch.Draw(texture, _rect, color);
        //pSpriteBatch.DrawString(_font, scoreText, new Vector2(_rect.X + (_rect.Width / 2) - (textSize.X / 2), _rect.Y + (_rect.Height / 2) - (textSize.Y / 2)), Color.White);
        pSpriteBatch.Draw(texture, new Vector2(Position.X, Position.Y), Color.White);
        pSpriteBatch.DrawString(_spriteFont, ammoText, new Vector2(textX + spacingX + 2, textY - 2), Color.Black);
        pSpriteBatch.DrawString(_spriteFont, ammoText, new Vector2(textX + spacingX, textY), Color.White);
    }
}