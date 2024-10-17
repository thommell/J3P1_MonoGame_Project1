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
        
        base.LoadContent(pContent);
    }
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        string scoreText = "Score: " + _scoringSystem.CurrentScore.ToString();
        Vector2 textSize = _font.MeasureString(scoreText);

        float textX = 0 + origin.X / 2 - 30 + (texture.Width / 2) - (textSize.X / 2);
        float textY = Game1.ScreenHeight - origin.Y * 2 - 10 + (texture.Height / 2) - (textSize.Y / 2);

        float spacingX = 180f;
        //pSpriteBatch.Draw(texture, _rect, color);
        //pSpriteBatch.DrawString(_font, scoreText, new Vector2(_rect.X + (_rect.Width / 2) - (textSize.X / 2), _rect.Y + (_rect.Height / 2) - (textSize.Y / 2)), Color.White);
        pSpriteBatch.Draw(texture, new Vector2(0 + origin.X / 2 - 30, Game1.ScreenHeight - origin.Y * 2 - 10), Color.White);
        //pSpriteBatch.Draw(texture, new Vector2(0 + origin.X / 2, Game1.ScreenHeight - origin.Y * 2 - 10), null, Color.White, rotation, origin, 1f, SpriteEffects.None, layer);

        pSpriteBatch.DrawString(_font, scoreText, new Vector2(textX + spacingX + 2, textY - 2), Color.Black);
        pSpriteBatch.DrawString(_font, scoreText, new Vector2(textX + spacingX, textY), Color.White);
       // base.Draw(pSpriteBatch);
    }

}
