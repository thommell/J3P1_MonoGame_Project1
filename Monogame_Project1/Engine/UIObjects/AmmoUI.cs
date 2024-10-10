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
        pSpriteBatch.DrawString(_spriteFont, _shootingSystem.Ammo.ToString(), new Vector2(position.X + texture.Width, position.Y), Color.White);

        base.Draw(pSpriteBatch);
    }

}