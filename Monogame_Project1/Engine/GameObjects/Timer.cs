using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.GameObjects;

public class Timer : GameObject
{
    private Game1 _game;
    private SceneManager _sceneManager;
    private float _time;
    private float _startTime;
    public Timer(Game1 pGame, SceneManager pSceneManager, float pTime)
    {
        _game = pGame;
        _time = pTime;
        _sceneManager = pSceneManager;

        _startTime = _time;
    }

    public override void Update(GameTime pGameTime)
    {
        if (_time >= 0)
            _time -= (float)pGameTime.ElapsedGameTime.TotalSeconds;
        else OnTimesUp();

        base.Update(pGameTime);
    }
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        SpriteFont font = _game.Content.Load<SpriteFont>("Font");

        base.Draw(pSpriteBatch);

        pSpriteBatch.DrawString(font, "Time Left: " + Math.Abs(Math.Ceiling(_time)).ToString(), new Vector2(50, 100), Color.White);
    }
    private void OnTimesUp()
    {
        _time = _startTime;
        _sceneManager.ChangeScene(_sceneManager.GetScene<MainMenu>());
    }
}
