using System;
using System.Linq;
using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;
public class ShootingSystem : GameObject
{
    private int _ammo;
    private bool _hasShot;
    private bool _allowedToKill = true;
    private readonly Scene _scene;
    private ScoringSystem _scoringSystem;
    private SpawningSystem _spawningSystem;
    public ShootingSystem(Scene pScene, int pAmmo) 
    {
        _scene = pScene;
        _ammo = pAmmo;
    }
    public override void LateLoad()
    {
        _scoringSystem = _scene.GetObject<ScoringSystem>();
        _spawningSystem = _scene.GetObject<SpawningSystem>();
    }
    public override void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();
        switch (mouseState.LeftButton)
        {
            case ButtonState.Pressed when !_hasShot && _ammo > 0:
                _ammo--;
                _hasShot = true;
                Console.WriteLine("Shot");
                break;
            case ButtonState.Released when _hasShot:
                _hasShot = false;
                _allowedToKill = true;
                Console.WriteLine("Let go!");
                break;
        }
    }
    public void CheckCollision()
    {
        if (!_allowedToKill) return;
        var mouseState = Mouse.GetState(); 
        var mousePoint = new Point(mouseState.X, mouseState.Y);
        var targets = _spawningSystem.CurrentTargets;
        var targetHit = targets.FirstOrDefault(a => a.Rectangle.Contains(mousePoint) && _hasShot);
        if (targetHit != null)
        {
            Console.WriteLine("You hit the target!");
            _allowedToKill = false;
            targetHit.Destroy();
            _scoringSystem.AddScore(targetHit.ScoreAmount);
            return;
        }
        if (!_hasShot) return;
        _allowedToKill = false;
        Console.WriteLine("You missed!");
        _scoringSystem.RemoveScore(2);
    }
}
