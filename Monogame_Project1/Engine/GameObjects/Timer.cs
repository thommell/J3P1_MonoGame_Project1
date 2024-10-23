using Microsoft.Xna.Framework;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.GameObjects;

public class Timer : GameObject
{
    #region Fields
    //timer
    private Game1 _game;
    private float _time;
    private bool _isRunning;
    private readonly float _startTime;

    //UI
    private Texture2D _barTexture;
    private Texture2D _clock;

    private Rectangle _recForeGround;
    private Rectangle _recBackGround;
    private Rectangle _outline;

    private readonly float _barHeight = 20f;
    private readonly float _barWidth = 400f;
    private readonly float _outlineWidth = 10f;
    #endregion
    
    #region Properties

    public bool IsRunning
    {
        get => _isRunning;
        set => _isRunning = value;
    }

    public float Time
    {
        get => _time;
        set => _time = value;
    }
    
    public float StartTime
    {
        get => _startTime;
    }
    #endregion

    #region Constructors
    public Timer(float pTime)
    {
        _game = SceneManager.Instance.Game;
        _time = pTime;
        _startTime = _time;
    }
    public Timer(float pTime, float pBarHeight, float pBarWidth, float pOutlineWidth = 10f) : this(pTime)
    {
        _barHeight = pBarHeight;
        _barWidth = pBarWidth;
        _outlineWidth = pOutlineWidth;
    }
    #endregion

    #region public methods
    public override void LoadContent(ContentManager pContent)
    {
        _barTexture = pContent.Load<Texture2D>("TestSprite");
        _clock = pContent.Load<Texture2D>("Clock");

        _recForeGround = new Rectangle((_game.GraphicsDevice.Viewport.Width - (int)_barWidth) / 2, 1000, (int)_barWidth, (int)_barHeight);
        _recBackGround = new Rectangle((_game.GraphicsDevice.Viewport.Width - (int)_barWidth) / 2, 1000, (int)_barWidth, (int)_barHeight);

        _outline = new Rectangle(_recForeGround.Location.X - (int)_outlineWidth / 2, _recForeGround.Location.Y - (int)_outlineWidth / 2, (int)_barWidth + (int)_outlineWidth, (int)_barHeight + (int)_outlineWidth);

        base.LoadContent(pContent);
    }

    public override void Update(GameTime pGameTime)
    {
        if (_time >= 0 && _isRunning) OnTimerActive(pGameTime);
            
        else ResetTimer();
    }
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        // SpriteFont font = _game.Content.Load<SpriteFont>("Font");

        base.Draw(pSpriteBatch);

        //pSpriteBatch.DrawString(font, "Time Left: " + Math.Abs(Math.Ceiling(_time)).ToString(), new Vector2(50, 100), Color.White);

        pSpriteBatch.Draw(_barTexture, _outline, Color.Black);
        pSpriteBatch.Draw(_barTexture, _recBackGround, Color.Gray);
        pSpriteBatch.Draw(_barTexture, _recForeGround, GetColor());
        pSpriteBatch.Draw(_clock, new Vector2(_recForeGround.Left - _clock.Width + 5, _recForeGround.Top + (_recForeGround.Height / 2) - (_clock.Height / 2)), Color.White);
    }

    public void ResetTimer() => _time = _startTime;

    public void ToggleTimer() => _isRunning = !_isRunning;

    #endregion

    #region Private Methods
    private void OnTimerActive(GameTime pGameTime)
    {
        _time -= (float)pGameTime.ElapsedGameTime.TotalSeconds;

        //Changes width of the bar depending on the time
        _recForeGround.Width = (int)(_barWidth * (_time / _startTime)); 
    }

    private Color GetColor()
    {
        if (_time <= _startTime * 0.25) return Color.Red;

        if (_time <= _startTime * 0.50) return Color.Orange;

        if (_time <= _startTime * 0.75) return Color.Yellow;

        return Color.Green;
    }
    #endregion
}
