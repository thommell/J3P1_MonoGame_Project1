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
    private Point _mousePoint;
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
        _mousePoint = new Point(mouseState.X, mouseState.Y);
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
        List<GameObject> targets = _spawningSystem.CurrentTargets;
        GameObject gameObjectHit = targets.FirstOrDefault(a => a.BoundingBox.Contains(_mousePoint) && _hasShot && IsActive);
        // Reverse casting to prevent C# type safety issue.
        Target targetHit = (Target)gameObjectHit;
        if (targetHit != null)
            OnHit(targetHit);
        else
            OnMiss();
    }
    public void OnHit(Target pTarget)
    {
        Console.WriteLine("You hit the target!");
        _allowedToKill = false;
        // pTarget.IsActive = false;
        DeactivateObject(pTarget);
        _scoringSystem.AddScore(pTarget.ScoreAmount);
    }
    public void OnMiss()
    {
        if (!_hasShot) return;
        _allowedToKill = false;
        Console.WriteLine("You missed!");
        _scoringSystem.RemoveScore(2);
    }
}
