using System;
using System.Linq;
using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;
public class ShootingSystem : GameObject
{
    private bool _hasShot;
    private bool _allowedToKill = true;
    private readonly Scene _scene;
    private ScoringSystem _scoringSystem;
    private SpawningSystem _spawningSystem;
    private AmmoSystem _ammoSystem;
    private Point _mousePoint;

    public ShootingSystem(Scene pScene) 
    {
        _scene = pScene;       
    }
    public override void LateLoad()
    {
        _scoringSystem = _scene.GetObject<ScoringSystem>();
        _spawningSystem = _scene.GetObject<SpawningSystem>();
        _ammoSystem = _scene.GetObject<AmmoSystem>();
    }
    public override void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();
        _mousePoint = new Point(mouseState.X, mouseState.Y);
        switch (mouseState.LeftButton)
        {
            case ButtonState.Pressed when !_hasShot && _ammoSystem.Ammo > 0:
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
        List<GameObject> targets = _spawningSystem.currentTargets;
        GameObject gameObjectHit = targets.FirstOrDefault(target => target.BoundingBox.Contains(_mousePoint) && _hasShot && target.IsActive);
        // Reverse casting to prevent C# type safety issue.
        BaseTarget targetHit = (BaseTarget)gameObjectHit;
        if (targetHit != null)
            OnHit(targetHit);
        else
            OnMiss();
    }
    public void OnHit(BaseTarget pTarget)
    {
        Console.WriteLine("You hit a target!");
        _allowedToKill = false;
        DeactivateObject(pTarget);
        pTarget.OnHit();
    }
    public void OnMiss()
    {
        if (!_hasShot) return;
        _allowedToKill = false;
        Console.WriteLine("You missed!");
        _scoringSystem.RemoveScore(2);
        _ammoSystem.SubtractAmmo(1);
    }
}
