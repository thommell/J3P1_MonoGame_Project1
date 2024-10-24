using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.UIObjects;

public abstract class SliderUI : GameObject
{
    private Game1 _game;
    private SceneManager _manager;
    private SliderButton _slider;

    protected Rectangle recForeGround;
    protected Rectangle recBackGround;
    protected Rectangle outline;

    private readonly float _barHeight = 20f;
    private readonly float _barWidth = 400f;
    private readonly float _outlineWidth = 10f;

    protected float currentValue;

    public SliderUI(Texture2D pTexture, bool pActive = true) : base(pTexture, pActive)
    { 
        _game = SceneManager.Instance.Game;
    }

    public override void LoadContent(ContentManager pContent)
    {
        recForeGround = new Rectangle((int)position.X, (int)position.Y, (int)_barWidth, (int)_barHeight);
        recBackGround = new Rectangle((int)position.X, (int)position.Y, (int)_barWidth, (int)_barHeight);

        outline = new Rectangle(recForeGround.Location.X - (int)_outlineWidth / 2, recForeGround.Location.Y - (int)_outlineWidth / 2, (int)_barWidth + (int)_outlineWidth, (int)_barHeight + (int)_outlineWidth);

        _slider = new SliderButton(pContent.Load<Texture2D>("SliderButton"), position.X, position.X + recForeGround.Width)
        {
            Position = new Vector2(position.X + (currentValue * recForeGround.Width), position.Y + (recForeGround.Height / 2))
        };

        base.LoadContent(pContent);
    }
    public override void Update(GameTime pGameTime)
    {
        _slider.Update(pGameTime);

        //gets the right value between 0 and 1 
        currentValue = (_slider.Position.X - recForeGround.Left) / recBackGround.Width;

        OnValue(currentValue);

        //calculates the width of the progress bar
        float width = recBackGround.Width * currentValue; 
        recForeGround.Width = (int)MathF.Round(width);

        base.Update(pGameTime);
    }
    public virtual void OnValue(float pValue) { }    
   
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        pSpriteBatch.Draw(texture, outline, Color.Black);
        pSpriteBatch.Draw(texture, recBackGround, Color.Gray);
        pSpriteBatch.Draw(texture, recForeGround, Color.Green);

        _slider.Draw(pSpriteBatch); 
    }
}
