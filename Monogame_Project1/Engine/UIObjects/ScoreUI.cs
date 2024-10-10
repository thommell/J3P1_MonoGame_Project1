using Monogame_Project1.Engine.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.GameObjects;

public class ScoreUI : UIObject
{
    private Game1 _game;
    private Scene _scene;
    private ScoringSystem _scoringSystem;

    private SpriteFont _font;
    private Rectangle _rect;

    private readonly float _rectWidth = 200f;
    private readonly float _rectHeight = 60f;

    public ScoreUI(Texture2D pTexture, Game1 pGame, Scene pScene, bool pActive = true) : base(pTexture, pActive)
    {
        _game = pGame;
       _scene = pScene; 
    }
    public override void LoadContent(ContentManager pContent)
    {
        _font = pContent.Load<SpriteFont>("UIText");

        _scoringSystem = _scene.GetObject<ScoringSystem>();

        _rect = new Rectangle(_game.GraphicsDevice.Viewport.Width - (int)_rectWidth - 20, 30, (int)_rectWidth, (int)_rectHeight);  
        base.LoadContent(pContent);
    }
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        string scoreText = "Score: " + _scoringSystem.CurrentScore.ToString();
        Vector2 textSize = _font.MeasureString(scoreText);

        pSpriteBatch.Draw(texture, _rect, color);
        pSpriteBatch.DrawString(_font, scoreText, new Vector2(_rect.X + (_rect.Width / 2) - (textSize.X / 2), _rect.Y + (_rect.Height / 2) - (textSize.Y / 2)), Color.White);
    }

}
