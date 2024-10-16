using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Monogame_Project1.Engine.UIObjects;

public class SoundSliderUI : GameObject
{
    private Game1 _game;
    private SceneManager _manager;
    private SliderButton _slider;

    private Rectangle _recForeGround;
    private Rectangle _recBackGround;
    private Rectangle _outline;

    private readonly float _barHeight = 20f;
    private readonly float _barWidth = 400f;
    private readonly float _outlineWidth = 10f;

    private float _currentValue;

    public SoundSliderUI(Texture2D pTexture, SceneManager pManager, Game1 pGame) : base(pTexture)
    {
        _game = pGame;
        _manager = pManager;
    }

    public override void LoadContent(ContentManager pContent)
    {
        _currentValue = AudioManager.Instance.Volume;

        _recForeGround = new Rectangle((int)position.X, (int)position.Y, (int)_barWidth, (int)_barHeight);
        _recBackGround = new Rectangle((int)position.X, (int)position.Y, (int)_barWidth, (int)_barHeight);

        _outline = new Rectangle(_recForeGround.Location.X - (int)_outlineWidth / 2, _recForeGround.Location.Y - (int)_outlineWidth / 2, (int)_barWidth + (int)_outlineWidth, (int)_barHeight + (int)_outlineWidth);

        _slider = new SliderButton(_game, _manager, pContent.Load<Texture2D>("UI_Tile_64x64"), position.X, position.X + _recForeGround.Width)
        {
            Position = new Vector2(position.X + (_currentValue * _recForeGround.Width), position.Y + (_recForeGround.Height / 2))
        };

        base.LoadContent(pContent);
    }
    public override void Update(GameTime pGameTime)
    {
        _slider.Update(pGameTime);

        //gets the right value between 0 and 1 
        _currentValue = (_slider.Position.X - _recForeGround.Left) / _recBackGround.Width;

        AudioManager.Instance.SetVolume(_currentValue);

        //calculates the width of the progress bar
        float width = _recBackGround.Width * _currentValue; 
        _recForeGround.Width = (int)MathF.Round(width);

        base.Update(pGameTime);
    }
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        pSpriteBatch.Draw(texture, _outline, Color.Black);
        pSpriteBatch.Draw(texture, _recBackGround, Color.Gray);
        pSpriteBatch.Draw(texture, _recForeGround, Color.Green);

        _slider.Draw(pSpriteBatch); 
    }
}
