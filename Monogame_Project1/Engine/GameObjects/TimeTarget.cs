using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.GameObjects;

public class TimeTarget : BaseTarget
{
    private Timer _timer;
    private float _timeToAdd;
    public TimeTarget(Texture2D pTarget, float pTimeToAdd = 2f) : base(pTarget) 
    {
        _timer = SceneManager.Instance.CurrentScene.GetObject<Timer>(); 
        _timeToAdd = pTimeToAdd;
    }
    public override void OnHit()
    {
        _timer.AddTime(_timeToAdd);
        base.OnHit();
    }
}
