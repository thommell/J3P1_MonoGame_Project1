using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using System;
using System.Runtime.CompilerServices;

namespace Monogame_Project1.Engine.UIObjects;

public class AmmoUI : UIObject
{
    private AmmoSystem _ammoSystem;
    private Scene _scene;
    private Game1 _game;

    private SpriteFont _spriteFont;

    Texture2D _bullet;
    Texture2D _usedBullet;
    
    //private Rectangle _rec;

    private readonly float _recWidth = 100f;
    private readonly float _recHeight = 100f;
    public AmmoUI(Texture2D pBullet, Texture2D pUsedBullet, Scene pScene, Game1 pGame, bool pActive = true, Game1 game = null) : base(pActive)
    {
        _scene = pScene;
        _game = pGame;

        _bullet = pBullet;
        _usedBullet = pUsedBullet;
    }

    public override void LoadContent(ContentManager pContent)
    {
        _ammoSystem = _scene.GetObject<AmmoSystem>();
        _spriteFont = pContent.Load<SpriteFont>("UIText");

        //_rec = new Rectangle(50, 50, (int)_recWidth, (int)_recHeight);

        base.LoadContent(pContent);
    }

    public override void Draw(SpriteBatch pSpriteBatch)
    {      
        float spacing = 10f;
        
        for (int i = 0; i < _ammoSystem.StartAmmo;  i++)
        {          
            Texture2D textureToUse = GetTexture(i);

            float posX = _game.GraphicsDevice.Viewport.Width - ((_ammoSystem.StartAmmo - i) * textureToUse.Width) - (i * spacing);
            float posY = _game.GraphicsDevice.Viewport.Height - textureToUse.Height;

            pSpriteBatch.Draw(textureToUse, new Vector2(posX, posY), Color.White);
        }
    }
    private Texture2D GetTexture(int pBullet)
    {
        if (pBullet +1 <= _ammoSystem.Ammo) return _bullet;

        return _usedBullet;
    }

    /*string ammoText = "Ammo: " + _ammoSystem.Ammo.ToString();
        Vector2 textSize = _spriteFont.MeasureString(ammoText);

        float textX = position.X + (texture.Width / 2) - (textSize.X / 2);
        float textY = position.Y + (texture.Height / 2) - (textSize.Y / 2);

        float spacingX = 180f;
        //pSpriteBatch.Draw(texture, _rect, color);
        //pSpriteBatch.DrawString(_font, scoreText, new Vector2(_rect.X + (_rect.Width / 2) - (textSize.X / 2), _rect.Y + (_rect.Height / 2) - (textSize.Y / 2)), Color.White);
        pSpriteBatch.Draw(texture, new Vector2(Position.X, Position.Y), Color.White);
        pSpriteBatch.DrawString(_spriteFont, ammoText, new Vector2(textX + spacingX + 2, textY - 2), Color.Black);
        pSpriteBatch.DrawString(_spriteFont, ammoText, new Vector2(textX + spacingX, textY), Color.White);*/

}